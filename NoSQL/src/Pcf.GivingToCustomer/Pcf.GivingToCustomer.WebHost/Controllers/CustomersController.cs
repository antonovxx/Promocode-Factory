using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.DataAccess.Services;
using Pcf.GivingToCustomer.WebHost.Mappers;
using Pcf.GivingToCustomer.WebHost.Models;
using Pcf.GivingToCustomer.WebHost.Services;

namespace Pcf.GivingToCustomer.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController(
        IRepository<Customer> customerRepository,
        PreferenceService preferenceService,
        IPreferenceSyncService preferenceSyncService)
        : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository = customerRepository;
        private readonly PreferenceService _preferenceService = preferenceService;
        private readonly IPreferenceSyncService _preferenceSyncService = preferenceSyncService;

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CustomerShortResponse>>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            var response = customers.Select(x => new CustomerShortResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id">Id клиента, например <example>a6c8c6b1-4349-45b0-ab31-244740aaf0f0</example></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            var response = new CustomerResponse(customer);

            return Ok(response);
        }

        /// <summary>
        /// Создать нового клиента
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var preferencesFromDictionary = await _preferenceService.GetPreferencesAsync();
            
            await _preferenceSyncService.SyncPreferencesAsync(preferencesFromDictionary);
    
            var validPreferenceIds = preferencesFromDictionary
                .Where(p => request.PreferenceIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            if (validPreferenceIds.Count != request.PreferenceIds.Count)
            {
                return BadRequest("Некоторые из указанных предпочтений не существуют");
            }

            var customer = CustomerMapper.MapFromModel(request, validPreferenceIds);
    
            await _customerRepository.AddAsync(customer);
    
            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer.Id);
        }

        /// <summary>
        /// Обновить клиента
        /// </summary>
        /// <param name="id">Id клиента, например <example>a6c8c6b1-4349-45b0-ab31-244740aaf0f0</example></param>
        /// <param name="request">Данные запроса></param>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            var preferencesFromDictionary = await _preferenceService.GetPreferencesAsync();
    
            var validPreferenceIds = preferencesFromDictionary
                .Where(p => request.PreferenceIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            if (validPreferenceIds.Count != request.PreferenceIds.Count)
            {
                return BadRequest("Некоторые из указанных предпочтений не существуют");
            }

            CustomerMapper.MapFromModel(request, validPreferenceIds, customer);

            await _customerRepository.UpdateAsync(customer);

            return NoContent();
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id">Id клиента, например <example>a6c8c6b1-4349-45b0-ab31-244740aaf0f0</example></param>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            await _customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
}
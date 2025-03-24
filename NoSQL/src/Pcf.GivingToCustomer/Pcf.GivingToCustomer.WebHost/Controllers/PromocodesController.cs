using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.DataAccess.Services;
using Pcf.GivingToCustomer.WebHost.Mappers;
using Pcf.GivingToCustomer.WebHost.Models;

namespace Pcf.GivingToCustomer.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController(
        IRepository<Customer> customersRepository,
        PreferenceService preferenceService,
        IRepository<PromoCode> promoCodesRepository)
        : ControllerBase
    {
        private readonly IRepository<PromoCode> _promoCodesRepository = promoCodesRepository;
        private readonly IRepository<Customer> _customersRepository = customersRepository;
        private readonly PreferenceService _preferenceService = preferenceService;

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promocodes = await _promoCodesRepository.GetAllAsync();

            var response = promocodes.Select(x => new PromoCodeShortResponse()
            {
                Id = x.Id,
                Code = x.Code,
                BeginDate = x.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                PartnerId = x.PartnerId,
                ServiceInfo = x.ServiceInfo
            }).ToList();

            return Ok(response);
        }
        
        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            //Получаем предпочтение по имени
            var preferencesFromDictionary = await _preferenceService.GetPreferencesAsync();
            
            var preferenceExists = preferencesFromDictionary.Any(p => p.Id == request.PreferenceId);

            if (!preferenceExists)
            {
                return BadRequest("Указанное предпочтение не существует");
            }

            var customers = await _customersRepository
                .GetWhere(d => d.Preferences.Any(x => x.PreferenceId == request.PreferenceId));

            var promoCode = PromoCodeMapper.MapFromModel(request, customers);

            await _promoCodesRepository.AddAsync(promoCode);

            return CreatedAtAction(nameof(GetPromocodesAsync), new { }, null);
        }
    }
}
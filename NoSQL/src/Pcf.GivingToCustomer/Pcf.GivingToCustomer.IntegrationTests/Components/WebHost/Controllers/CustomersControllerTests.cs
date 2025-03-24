using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.DataAccess.Repositories;
using Pcf.GivingToCustomer.DataAccess.Services;
using Pcf.GivingToCustomer.Integration.Clients;
using Pcf.GivingToCustomer.WebHost.Controllers;
using Pcf.GivingToCustomer.WebHost.Models;
using Pcf.GivingToCustomer.WebHost.Services;
using Pcf.GivingToCustomer.WebHost.Services.Impl;
using Xunit;

namespace Pcf.GivingToCustomer.IntegrationTests.Components.WebHost.Controllers
{
    [Collection(EfDatabaseCollection.DbCollection)]
    public class CustomersControllerTests: IClassFixture<EfDatabaseFixture>
    {
        private readonly CustomersController _customersController;
        private readonly EfRepository<Customer> _customerRepository;
        private readonly PreferenceService _preferenceService;
        private readonly IPreferenceSyncService _preferenceSyncService;
        
        public CustomersControllerTests(EfDatabaseFixture efDatabaseFixture)
        {
            _customerRepository = new EfRepository<Customer>(efDatabaseFixture.DbContext);
            
            var dictionaryClientMock = new Mock<IDictionaryClient>();
            
            dictionaryClientMock.Setup(client => client.GetPreferencesAsync())
                .ReturnsAsync([new Preference() { Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"), Name = "Театр" }]);
            
            var memoryCacheMock = new Mock<IMemoryCache>();
            memoryCacheMock.Setup(cache => cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
                .Returns(false);
            
            _preferenceService = new PreferenceService(dictionaryClientMock.Object, memoryCacheMock.Object);
            _preferenceSyncService = new PreferenceSyncService(efDatabaseFixture.DbContext);
            
            _customersController = new CustomersController(
                _customerRepository, 
                _preferenceService,
                _preferenceSyncService);
        }
        
        [Fact]
        public async Task CreateCustomerAsync_CanCreateCustomer_ShouldCreateExpectedCustomer()
        {
            //Arrange 
            var preferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c");
            var request = new CreateOrEditCustomerRequest
            {
                Email = "some@mail.ru",
                FirstName = "Иван",
                LastName = "Петров",
                PreferenceIds = new List<Guid>()
                {
                    preferenceId
                }
            };

            //Act
            var result = await _customersController.CreateCustomerAsync(request);
            var actionResult = result.Result as CreatedAtActionResult;
            var id = (Guid)actionResult.Value;
            
            //Assert
            var actual = await _customerRepository.GetByIdAsync(id);
            
            actual.Email.Should().Be(request.Email);
            actual.FirstName.Should().Be(request.FirstName);
            actual.LastName.Should().Be(request.LastName);
            actual.Preferences.Should()
                .ContainSingle()
                .And
                .Contain(x => x.PreferenceId == preferenceId);
        }
    }
}
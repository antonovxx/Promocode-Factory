using System;
using System.Collections.Generic;
using System.Linq;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.WebHost.Models;

namespace Pcf.GivingToCustomer.WebHost.Mappers
{
    public class CustomerMapper
    {

        public static Customer MapFromModel(CreateOrEditCustomerRequest model, IEnumerable<Guid> preferenceIds, Customer customer = null)
        {
            customer ??= new Customer { Id = Guid.NewGuid() };
            
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;

            customer.Preferences = preferenceIds.Select(preferenceId => new CustomerPreference
            {
                CustomerId = customer.Id,
                PreferenceId = preferenceId
            }).ToList();
        
            return customer;
        }
    }
}

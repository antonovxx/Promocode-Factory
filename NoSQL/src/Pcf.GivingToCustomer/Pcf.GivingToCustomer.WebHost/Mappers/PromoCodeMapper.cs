using System;
using System.Collections.Generic;
using System.Linq;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.WebHost.Models;

namespace Pcf.GivingToCustomer.WebHost.Mappers
{
    public class PromoCodeMapper
    {
        public static PromoCode MapFromModel(GivePromoCodeRequest request, IEnumerable<Customer> customers) {

            return new PromoCode
            {
                Id = Guid.NewGuid(),
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Parse(request.BeginDate),
                EndDate = DateTime.Parse(request.EndDate),
                PartnerId = request.PartnerId,
                PreferenceId = request.PreferenceId,
                Customers = customers.Select(c => new PromoCodeCustomer
                {
                    CustomerId = c.Id
                }).ToList()
            };
        }
    }
}

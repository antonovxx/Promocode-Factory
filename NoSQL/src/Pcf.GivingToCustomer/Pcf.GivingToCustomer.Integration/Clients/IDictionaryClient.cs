using System.Collections.Generic;
using System.Threading.Tasks;
using Pcf.GivingToCustomer.Core.Domain;

namespace Pcf.GivingToCustomer.Integration.Clients;

public interface IDictionaryClient
{
    Task<List<Preference>> GetPreferencesAsync();
}
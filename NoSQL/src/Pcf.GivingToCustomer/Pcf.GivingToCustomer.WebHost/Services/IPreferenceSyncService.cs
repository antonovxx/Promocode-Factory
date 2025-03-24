using System.Collections.Generic;
using System.Threading.Tasks;
using Pcf.GivingToCustomer.Core.Domain;

namespace Pcf.GivingToCustomer.WebHost.Services;

public interface IPreferenceSyncService
{
    Task SyncPreferencesAsync(IEnumerable<Preference> preferencesFromDictionary);
}
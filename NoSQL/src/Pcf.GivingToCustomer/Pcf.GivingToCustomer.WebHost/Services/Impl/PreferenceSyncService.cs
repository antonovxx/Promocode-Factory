using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.DataAccess;

namespace Pcf.GivingToCustomer.WebHost.Services.Impl;

public class PreferenceSyncService(DataContext dataContext) : IPreferenceSyncService
{
    private readonly DataContext _dataContext = dataContext;

    public async Task SyncPreferencesAsync(IEnumerable<Preference> preferencesFromDictionary)
    {
        var existingPreferenceIds = await _dataContext.Preferences
            .Select(p => p.Id)
            .ToListAsync();

        var missingPreferences = preferencesFromDictionary
            .Where(p => !existingPreferenceIds.Contains(p.Id))
            .ToList();

        if (missingPreferences.Count != 0)
        {
            await _dataContext.Preferences.AddRangeAsync(missingPreferences);
            await _dataContext.SaveChangesAsync();
        }
    }
}
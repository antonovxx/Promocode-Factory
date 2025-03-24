using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Integration.Clients;

namespace Pcf.GivingToCustomer.DataAccess.Services;

public class PreferenceService(IDictionaryClient dictionaryClient, IMemoryCache cache)
{
    private const string CacheKey = "preferences";
    private const int RetentionTime = 1;
    
    private readonly IDictionaryClient _dictionaryClient = dictionaryClient;
    private readonly IMemoryCache _cache = cache;

    public async Task<List<Preference>> GetPreferencesAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out List<Preference> preferences))
        {
            preferences = await _dictionaryClient.GetPreferencesAsync();
            _cache.Set(CacheKey, preferences, TimeSpan.FromHours(RetentionTime));
        }
        return preferences;
    }
}
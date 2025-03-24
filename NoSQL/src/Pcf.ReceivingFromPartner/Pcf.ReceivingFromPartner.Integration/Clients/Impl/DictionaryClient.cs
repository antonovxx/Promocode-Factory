using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Pcf.ReceivingFromPartner.Core.Domain;

namespace Pcf.ReceivingFromPartner.Integration.Clients.Impl;

public class DictionaryClient(HttpClient httpClient) : IDictionaryClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Preference>> GetPreferencesAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/preferences");
        
        response.EnsureSuccessStatusCode();
        
        var preferences = await response.Content.ReadFromJsonAsync<List<Preference>>();
        
        return preferences ?? [];
    }
}
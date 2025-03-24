using Microsoft.AspNetCore.Mvc;
using Pcf.Dictionary.DataAccess.Repositories;
using Pcf.Dictionary.WebHost.Models;

namespace Pcf.Dictionary.WebHost.Controllers;

/// <summary>
/// Предпочтения клиентов
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class PreferencesController(IPreferenceRepository preferencesRepository) : ControllerBase
{
    /// <summary>
    /// Получить список предпочтений
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PreferenceResponse>>> GetPreferencesAsync()
    {
        var preferences = await preferencesRepository.GetAllAsync();

        var response = preferences.Select(x => new PreferenceResponse
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return Ok(response);
    }
}
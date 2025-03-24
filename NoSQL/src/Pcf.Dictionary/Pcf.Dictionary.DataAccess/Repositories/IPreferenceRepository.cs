using Pcf.Dictionary.Core.Domain;

namespace Pcf.Dictionary.DataAccess.Repositories;

public interface IPreferenceRepository
{
    Task<IEnumerable<Preference>> GetAllAsync();
}
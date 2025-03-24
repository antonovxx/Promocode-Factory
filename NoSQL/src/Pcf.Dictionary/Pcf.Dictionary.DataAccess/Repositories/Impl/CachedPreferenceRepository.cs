using Pcf.Dictionary.Core.Domain;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Pcf.Dictionary.DataAccess.Repositories.Impl
{
    public class CachedPreferenceRepository(IRedisClient redisClient) : IPreferenceRepository
    {
        private const string PreferencesCacheKey = "preferences";
        
        private readonly IRedisDatabase _redisDatabase = redisClient.GetDefaultDatabase();

        private static readonly List<Preference> DefaultPreferences =
        [
            new() { Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"), Name = "Театр" },
            new() { Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"), Name = "Семья" },
            new() { Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"), Name = "Дети" }
        ];

        public async Task<IEnumerable<Preference>> GetAllAsync()
        {
            var preferences = await _redisDatabase.GetAsync<List<Preference>>(PreferencesCacheKey);

            if (preferences is null)
            {
                preferences = DefaultPreferences;
                await _redisDatabase.AddAsync(PreferencesCacheKey, preferences, TimeSpan.FromHours(24));
            }

            return preferences;
        }
    }
}
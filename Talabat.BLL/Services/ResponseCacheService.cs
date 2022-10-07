using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;

namespace Talabat.BLL.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CasheResponceAsync(string casheKey, object response, TimeSpan timeToLive)
        {
            if (response == null) return; 

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponse = JsonSerializer.Serialize(response , options);

            await _database.StringSetAsync(casheKey, serializedResponse, timeToLive);
        }

        public async Task<string> GetCashedResponse(string casheKey)
        {
            var casheResponse = await _database.StringGetAsync(casheKey);
            if (casheResponse.IsNullOrEmpty) 
                return null;
            return casheResponse;
        }
    }
}

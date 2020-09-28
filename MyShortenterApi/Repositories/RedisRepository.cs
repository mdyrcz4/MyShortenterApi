using MyShortenterApi.Repositories.Interfaces;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace MyShortenterApi.Repositories
{
    public class RedisRepository : IRepository
    {
        private readonly IDatabase _redis;

        public RedisRepository(ConnectionMultiplexer connection)
        {
            _redis = connection.GetDatabase();
        }

        public async Task Add(string key, string url)
        {
            await _redis.StringSetAsync(key, url);
        }

        public async Task<bool> DoesKeyExist(string key)
        {
            var value = await _redis.StringGetAsync(key);
            return !value.IsNullOrEmpty;
        }

        public async Task<string> GetUrl(string key)
        {
            return await _redis.StringGetAsync(key);
        }
    }
}
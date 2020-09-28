using MyShortenterApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShortenterApi.Repositories
{
    public class MockRepository : IRepository
    {
        private readonly Dictionary<string, string> _keyValuePairs;

        public MockRepository()
        {
            _keyValuePairs = new Dictionary<string, string>();
        }

        public Task Add(string key, string url)
        {
            _keyValuePairs.Add(key, url);
            return Task.CompletedTask;
        }

        public async Task<bool> DoesKeyExist(string key)
        {
            return await Task.FromResult(_keyValuePairs.ContainsKey(key));
        }

        public async Task<string> GetUrl(string key)
        {
            return await Task.FromResult(_keyValuePairs[key]);
        }
    }
}
using Mde.Project.Mobile.Services.Interfaces;

namespace Mde.Project.Mobile.Services
{
    public class FakeSecureStorageService : ISecureStorageService
    {
        private readonly Dictionary<string, string> _storage = new Dictionary<string, string>();

        public Task<string> GetAsync(string key)
        {
            _storage.TryGetValue(key, out var value);
            return Task.FromResult(value);
        }

        public Task SetAsync(string key, string value)
        {
            _storage[key] = value;
            return Task.CompletedTask;
        }
    }
}

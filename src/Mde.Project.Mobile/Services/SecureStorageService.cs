using Mde.Project.Mobile.Services.Interfaces;

namespace Mde.Project.Mobile.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public Task<string> GetAsync(string key)
        {
            return SecureStorage.GetAsync(key);
        }

        public Task SetAsync(string key, string value)
        {
            return SecureStorage.SetAsync(key, value);
        }
    }
}

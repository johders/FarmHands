﻿namespace Mde.Project.Mobile.Services.Interfaces
{
    public interface ISecureStorageService
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value);
    }
}

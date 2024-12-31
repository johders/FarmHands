using Firebase.Auth;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResultModel<string>> RegisterUserAsync(string email, string password, string name, UserRole role, string? farmName = null);
        Task<BaseResultModel> AssignRoleAsync(string uid, UserRole role);
        Task<ResultModel<string>> LoginUserAsync(string email, string password);
        Task<ResultModel<string>> GetAuthTokenAsync();
        Task<ResultModel<UserRole>> GetRoleFromTokenAsync(string token);
        Task<ResultModel<DateTime>> GetTokenExpirationDateTimeAsync(string token);
        BaseResultModel Logout();
    }
}

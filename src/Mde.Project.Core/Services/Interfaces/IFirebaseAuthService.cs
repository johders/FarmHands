using Firebase.Auth;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFirebaseAuthService
    {
        Task<BaseResultModel> AddUserRoleToToken(string uid, UserRole role);
        Task<ResultModel<string>> GetAuthTokenAsync();
        Task<ResultModel<UserRole>> GetRoleFromTokenAsync(string token);
        Task<ResultModel<UserCredential>> LoginUserAsync(string email, string password);
        BaseResultModel Logout();
        Task<ResultModel<UserCredential>> RegisterUserAsync(string email, string password);
        Task<ResultModel<string>> RefreshTokenAsync();
        ResultModel<DateTime> GetTokenExpirationTime(string token);
    }
}
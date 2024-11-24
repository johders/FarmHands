using Firebase.Auth;
using Google.Cloud.Firestore;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly FirebaseAuthService _authService;
        private readonly FirestoreDb _firestoreDb;

        public AccountService(FirebaseAuthService authService, IFirestoreContext firestoreDb)
        {
            _authService = authService;
            _firestoreDb = firestoreDb.GetFireStoreDb();
        }

        public async Task<ResultModel<string>> RegisterUserAsync(string email, string password, string name, UserRole role, string? farmName = null)
        {

            var result = new ResultModel<string>();

            try
            {
                var registerResult = await _authService.RegisterUserAsync(email, password);

                if (!registerResult.IsSuccess)
                {
                    foreach (var error in  registerResult.Errors)
                    {
                        result.Errors.Add(error);
                    }
                    return result;
                }

                var auth = registerResult.Data;
                string uid = auth.User.Uid;

                if (role == UserRole.Farmer && farmName is not null)
                {
                    var farm = new Farm { Name = farmName, OwnerId = uid };
                    var farmDoc = await _firestoreDb.Collection("Farms").AddAsync(farm);

                    var farmer = new Farmer
                    {
                        Uid = uid,
                        Name = name,
                        Role = role,
                        Email = email,
                        FarmId = farmDoc.Id,
                    };
                    await _firestoreDb.Collection("Users").Document(uid).SetAsync(farmer);
                    result.Data = uid;

                    return result;
                }
                else
                {
                    var consumer = new Consumer
                    {
                        Uid = uid,
                        Name = name,
                        Role = role,
                        Email = email,
                        FavoriteFarms = new List<string>(),
                        FavoriteProducts = new List<string>(),
                    };
                    await _firestoreDb.Collection("Users").Document(uid).SetAsync(consumer);
                    result.Data = uid;

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
       
        }

        public async Task<BaseResultModel> AssignRoleAsync(string uid, UserRole role)
        {
            var result = new BaseResultModel();

            var assignRoleResult = await _authService.AddUserRoleToToken(uid, role);

            if (!assignRoleResult.IsSuccess)
            {
                foreach (var error in assignRoleResult.Errors)
                {
                    result.Errors.Add(error);
                }
            }

            return result;
        }

        public async Task<ResultModel<UserCredential>> LoginUserAsync(string email, string password)
        {
            var result = new ResultModel<UserCredential>();

            var loginResult = await _authService.LoginUserAsync(email, password);

            if (!loginResult.IsSuccess)
            {
                foreach (var error in loginResult.Errors)
                {
                    result.Errors.Add(error);
                }
            }

            return result;
        }

        public async Task<ResultModel<string>> GetAuthTokenAsync()
        {
            var result = new ResultModel<string>();

            var tokenResult = await _authService.GetAuthTokenAsync();

            if (!tokenResult.IsSuccess)
            {
                foreach (var error in tokenResult.Errors)
                {
                    result.Errors.Add(error);
                }

                return result;
            }

            result.Data = tokenResult.Data;
            return result;
        }

        public async Task<ResultModel<UserRole>> GetRoleFromTokenAsync(string token)
        {
            var result = new ResultModel<UserRole>();

            var roleResult = await _authService.GetRoleFromTokenAsync(token);

            if (!roleResult.IsSuccess)
            {
                foreach (var error in roleResult.Errors)
                {
                    result.Errors.Add(error);
                }

                return result;
            }
        
            result.Data = roleResult.Data;
            return result;
        }
    }
}

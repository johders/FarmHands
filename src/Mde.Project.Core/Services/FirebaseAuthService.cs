using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin.Auth;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyBCzGRxyK9-JbKZrMP8nS_8TwRTS3y1dSY";
        private readonly FirebaseAuthClient _authClient;
        private readonly IFirestoreContext _firestoreContext;

        public FirebaseAuthService(IFirestoreContext firestoreContext)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = ApiKey,
                AuthDomain = $"farmhands-431df.firebaseapp.com",
                Providers = new FirebaseAuthProvider[] {
                    new EmailProvider()
                },
            };

            _authClient = new FirebaseAuthClient(config);
            _firestoreContext = firestoreContext;
        }

        public async Task<ResultModel<UserCredential>> RegisterUserAsync(string email, string password)
        {
            var result = new ResultModel<UserCredential>();

            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);
                result.Data = userCredential;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                Console.WriteLine($"Account register failure: {ex.Message}");
                return result;
            }

        }

        public async Task<ResultModel<UserCredential>> LoginUserAsync(string email, string password)
        {
            var result = new ResultModel<UserCredential>();

            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);

                result.Data = userCredential;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                Console.WriteLine($"Login failure: {ex.Message}");
                return result;
            }
            
        }

        public async Task<BaseResultModel> AddUserRoleToToken(string uid, UserRole role)
        {
            var result = new BaseResultModel();

            try
            {
                var claims = new Dictionary<string, object>
                {
                    { "role", role.ToString() }
                };

                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);

                Console.WriteLine($"Custom claims added for user {uid}: {role}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting custom claims: {ex.Message}");
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ResultModel<UserRole>> GetRoleFromTokenAsync(string token)
        {
            var result = new ResultModel<UserRole>();
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                if (decodedToken.Claims.TryGetValue("role", out var roleClaim))
                {
                    if (Enum.TryParse(typeof(UserRole), roleClaim.ToString(), out var role))
                    {
                        result.Data = (UserRole)role;
                        return result;
                    }
                }

                result.Errors.Add("Error getting role from token");
                Console.WriteLine($"Error retrieving role from token: ROLE not found");

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                Console.WriteLine($"Error retrieving role from token: {ex.Message}");
                return result;
            }
        }

        public async Task<ResultModel<string>> GetAuthTokenAsync()
        {
            var result = new ResultModel<string>();
            try
            {
                var user = _authClient.User;
                if (user == null)
                {
                    result.Errors.Add("User is not signed in");
                }

                var token = await user.GetIdTokenAsync();
                result.Data = token;

                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error setting custom claims: {ex.Message}");
                result.Errors.Add(ex.Message);
                return result;
            }
           
        }

        public BaseResultModel Logout()
        {
            var result = new BaseResultModel();

            try
            {
                _authClient.SignOut();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout Error: {ex.Message}");
                result.Errors.Add(ex.Message);
                return result;
            }

            return result;
        }
    }
}

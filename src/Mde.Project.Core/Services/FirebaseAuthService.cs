using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin.Auth;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Mde.Project.Core.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private const string ApiKey = "AIzaSyBCzGRxyK9-JbKZrMP8nS_8TwRTS3y1dSY";
        private readonly FirebaseAuthClient _authClient;

        public FirebaseAuthService()
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
            catch (Firebase.Auth.FirebaseAuthException authEx)
            {
                switch (authEx.Reason)
                {
                    case AuthErrorReason.EmailExists:
                        result.Errors.Add("This email is already in use.");
                        break;
                    case AuthErrorReason.InvalidEmailAddress:
                        result.Errors.Add("Please enter a valid email address.");
                        break;
                    case AuthErrorReason.WeakPassword:
                        result.Errors.Add("Your password is too weak. Please use a stronger password.");
                        break;
                    case AuthErrorReason.MissingEmail:
                        result.Errors.Add("Email address is required.");
                        break;
                    case AuthErrorReason.MissingPassword:
                        result.Errors.Add("Password is required.");
                        break;
                    default:
                        result.Errors.Add("An unexpected error occurred. Please try again.");
                        break;
                }
                Console.WriteLine($"Registration failure: {authEx.Message}");
            }
            catch (Exception ex)
            {
                result.Errors.Add("An unexpected error occurred. Please try again.");
                Console.WriteLine($"Registration failure: {ex.Message}");
            }

            return result;

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
            catch (Firebase.Auth.FirebaseAuthException authEx)
            {
                switch (authEx.Reason)
                {
                    case AuthErrorReason.InvalidEmailAddress:
                        result.Errors.Add("Please enter a valid email address.");
                        break;
                    case AuthErrorReason.MissingEmail:
                        result.Errors.Add("Please enter a valid email address.");
                        break;
                    case AuthErrorReason.WrongPassword:
                        result.Errors.Add("Incorrect password. Please try again.");
                        break;
                    case AuthErrorReason.UserNotFound:
                        result.Errors.Add("No account found with this email.");
                        break;
                    case AuthErrorReason.TooManyAttemptsTryLater:
                        result.Errors.Add("Too many attempts. Please try again later.");
                        break;
                    default:
                        result.Errors.Add("An error occurred. Please check your credentials and try again.");
                        break;
                }

                Console.WriteLine($"Login failure: {authEx.Message}");

            }
            catch (Exception ex)
            {
                result.Errors.Add("An unexpected error occurred. Please try again.");
                Console.WriteLine($"Login failure: {ex.Message}");
            }

            return result;
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

        public ResultModel<DateTime> GetTokenExpirationTime(string token)
        {
            var result = new ResultModel<DateTime>();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");

            if (expClaim != null && long.TryParse(expClaim.Value, out var expUnix))
            {
                result.Data = DateTimeOffset.FromUnixTimeSeconds(expUnix).LocalDateTime;
            }
            else
            {
                result.Errors.Add("Expiration claim not found in token.");
            }

            return result;
        }

        public async Task<ResultModel<string>> RefreshTokenAsync()
        {
            var result = new ResultModel<string>();
            try
            {
                var user = _authClient.User;
                if (user == null)
                {
                    result.Errors.Add("No signed-in user found to refresh token.");
                    return result;
                }

                var refreshedToken = await user.GetIdTokenAsync(forceRefresh: true);
                result.Data = refreshedToken;

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error refreshing token: {ex.Message}");
                Console.WriteLine($"Error refreshing token: {ex.Message}");
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

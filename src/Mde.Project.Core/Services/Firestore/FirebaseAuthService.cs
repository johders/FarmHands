using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin.Auth;

namespace Mde.Project.Core.Services.Firestore
{
    public class FirebaseAuthService
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

        public async Task<UserCredential> RegisterUserAsync(string email, string password)
        {
            var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);
            return userCredential;
        }

        public async Task<UserCredential> LoginUserAsync(string email, string password)
        {
            var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
            return userCredential;
        }

        public async Task<string> GetAuthTokenAsync()
        {
            var user = _authClient.User;
            if (user == null)
            {
                throw new InvalidOperationException("User is not signed in.");
            }

            var token = await user.GetIdTokenAsync();

            //await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);

            return token;
        }

        public void Logout()
        {
            _authClient.SignOut();
        }
    }
}

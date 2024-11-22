using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Firestore;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class LoginViewModel : ObservableObject
	{
		private string username;

        private readonly FirebaseAuthService _authService;

        public LoginViewModel(FirebaseAuthService authService)
        {
            _authService = authService;
        }

        public string Username
		{
			get { return username; }
			set { SetProperty(ref username, value); }
		}

		private string password;

		public string Password
		{
			get { return password; }
			set { SetProperty(ref password, value); }
		}

		private bool unSuccessful;

		public bool UnSuccessful
		{
			get { return unSuccessful; }
			set { SetProperty(ref unSuccessful, value); }
		}

		public ICommand LoginCommand => new Command(async () =>
		{
            //if (Username == "test@farm.com" && Password == "password")
            //{
            //	Application.Current.MainPage = new AppShellFarmer();
            //}
            //else if (Username == "test@user.com" && Password == "password")
            //{
            //	Application.Current.MainPage = new AppShellUser();
            //}
            //else
            //{
            //	UnSuccessful = true;
            //}

            var email = Username;
            var password = Password;

            try
            {
                var userCredential = await _authService.LoginUserAsync(email, password);
                await Shell.Current.DisplayAlert("Success", "Login successful!", "OK");

                SecureStorage.Default.SetAsync("auth_token", await _authService.GetAuthTokenAsync());

                Application.Current.MainPage = new AppShellUser();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }

        });
	}
}

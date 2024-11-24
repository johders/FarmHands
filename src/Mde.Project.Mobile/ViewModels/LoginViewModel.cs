using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Login;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class LoginViewModel : ObservableObject
	{
		private string username;
        private readonly IAccountService _accountService;
        private readonly IConnectivityService _connectivityService;
        public LoginViewModel(IAccountService accountService, IConnectivityService connectivityService)
        {
            _accountService = accountService;
            _connectivityService = connectivityService;
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
            var email = Username;
            var password = Password;
            
            var loginResult = await _accountService.LoginUserAsync(email, password);

            if (!loginResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", $"Login failed: {string.Join(", ", loginResult.Errors)}", "OK");
            }

            var uid = loginResult.Data;
            await SecureStorage.Default.SetAsync("userId", uid);

            var tokenResult = await _accountService.GetAuthTokenAsync();

            if (!tokenResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", $"Login failed: {string.Join(", ", tokenResult.Errors)}", "OK");
            }

            var token = tokenResult.Data;

            await SecureStorage.Default.SetAsync("authToken", token);

            var roleResult = await _accountService.GetRoleFromTokenAsync(token);
            
            if (!roleResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", $"Login failed: {string.Join(", ", roleResult.Errors)}", "OK");
            }

            var role = roleResult.Data;

            Application.Current.MainPage = role == Core.Enums.UserRole.User ? new AppShellUser() : role == Core.Enums.UserRole.Farmer ? new AppShellFarmer() : new AppShellStartup();           
        });

        public ICommand RegisterCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(RegisterOptionsPage), true);
        });

        //public ICommand CheckConnectionCommand => new Command(async () =>
        //{
        //    var connectionResult = _connectivityService.IsConnected();

        //    if (!connectionResult.IsSuccess)
        //    {
        //        await Shell.Current.DisplayAlert("No connectivity!", $"{connectionResult.Errors}", "OK");
        //        return;
        //    }
        //});

        public async Task CheckConnectionAsync()
        {
            var connectionResult = _connectivityService.IsConnected();

            if (!connectionResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("No connectivity!", $"{string.Join(", ", connectionResult.Errors)}", "OK");
            }
        }
    }
}

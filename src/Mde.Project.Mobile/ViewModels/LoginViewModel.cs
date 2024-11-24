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
        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
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

            var tokenResult = await _accountService.GetAuthTokenAsync();

            if (!tokenResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", $"Login failed: {string.Join(", ", tokenResult.Errors)}", "OK");
            }

            var token = tokenResult.Data;

            await SecureStorage.Default.SetAsync("auth_token", token);

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
	}
}

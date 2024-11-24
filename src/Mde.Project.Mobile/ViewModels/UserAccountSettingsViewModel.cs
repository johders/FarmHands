using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class UserAccountSettingsViewModel : ObservableObject
	{
		private readonly IAccountService _accountService;
        public UserAccountSettingsViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ICommand LogOutCommand => new Command(async () =>
		{
			bool isConfirmed = await ShowLogoutConfirmationAsync();

			if (isConfirmed)
			{
				var logoutResult = _accountService.Logout();

				if (!logoutResult.IsSuccess)
				{
					await Application.Current.MainPage.DisplayAlert("Oops", "Signout issue. Please try again later", "OK");
					return;
                }

                Application.Current.MainPage = new AppShellStartup();
			}
		});

		public async Task<bool> ShowLogoutConfirmationAsync()
		{
			return await Application.Current.MainPage.DisplayAlert("Confirm signout", "Are you sure you want to sign out?", "Yes", "No");
		}
	}

	
}

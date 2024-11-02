using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class UserAccountSettingsViewModel : ObservableObject
	{
		public ICommand LogOutCommand => new Command(async () =>
		{
			bool isConfirmed = await ShowLogoutConfirmationAsync();

			if (isConfirmed)
			{
				Application.Current.MainPage = new AppShellStartup();
			}
		});

		public async Task<bool> ShowLogoutConfirmationAsync()
		{
			return await Application.Current.MainPage.DisplayAlert("Confirm signout", "Are you sure you want to sign out?", "Yes", "No");
		}
	}

	
}

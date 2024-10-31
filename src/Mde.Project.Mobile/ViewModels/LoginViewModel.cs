using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class LoginViewModel : ObservableObject
	{
		private string username;

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

		public ICommand LoginCommand => new Command(() =>
		{
			if (Username == "test@farm.com" && Password == "password")
			{
				Application.Current.MainPage = new AppShellFarmer();
			}
			else if (Username == "test@user.com" && Password == "password")
			{
				Application.Current.MainPage = new AppShellUser();
			}
			else
			{
				UnSuccessful = true;
			}
		});
	}
}

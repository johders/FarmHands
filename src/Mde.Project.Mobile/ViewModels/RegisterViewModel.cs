using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Helpers;
using Mde.Project.Mobile.Pages.Login;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(Role), nameof(Role))]
    public class RegisterViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;

        public RegisterViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private UserRole role;
		public UserRole Role
		{
			get { return role; }
			set 
            {
                if (SetProperty(ref role, value))
                {
                    OnPropertyChanged(nameof(CanShowFarmName));
                }
            }
		}

		private string uid;
		public string Uid
		{
			get { return uid; }
			set { SetProperty(ref uid, value); }
		}

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string farmName;
        public string FarmName
        {
            get { return farmName; }
            set { SetProperty(ref farmName, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (SetProperty(ref password, value))
                {
                    ValidatePasswords();
                }
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get { return repeatPassword; }
            set
            {
                if (SetProperty(ref repeatPassword, value))
                {
                    ValidatePasswords();
                }
            }
        }

        private string passwordError;
        public string PasswordError
        {
            get { return passwordError; }
            set { SetProperty(ref passwordError, value); }
        }

        public bool IsPasswordValid => string.IsNullOrEmpty(PasswordError);
        public bool CanShowFarmName => Role == UserRole.Farmer;

        private void ValidatePasswords()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(RepeatPassword))
            {
                PasswordError = "Password fields cannot be empty.";
            }
            else if (Password != RepeatPassword)
            {
                PasswordError = "Passwords do not match.";
            }
            else if (Password.Length < 6)
            {
                PasswordError = "Password must be at least 6 characters.";
            }
            else
            {
                PasswordError = string.Empty;
            }

            OnPropertyChanged(nameof(IsPasswordValid));
        }

        public ICommand RegisterCommand => new Command(async () =>
        {
            var registerResult = await _accountService.RegisterUserAsync(Email, Password, Name, Role, FarmName);

            if (!registerResult.IsSuccess)
            {

                foreach (var error in registerResult.Errors)
                {
                    Console.WriteLine(error);
                }
                await Shell.Current.DisplayAlert("Error", "Something went wrong during registration, please try again later", "OK");
                
                return;
            }

            var assignRoleResult = await _accountService.AssignRoleAsync(registerResult.Data, Role);

            if (!assignRoleResult.IsSuccess)
            {
                foreach (var error in registerResult.Errors)
                {
                    Console.WriteLine(error);
                }
                await Shell.Current.DisplayAlert("Error", "Something went wrong during registration, please try again later", "OK");

                return;
            }

            await ToastHelper.ShowToastAsync($"Thank you for registering! You can now sign in");

            await Shell.Current.GoToAsync(nameof(LoginPage), true);
        });

    }
}

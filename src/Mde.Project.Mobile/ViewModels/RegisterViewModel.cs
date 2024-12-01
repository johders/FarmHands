using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Helpers;
using Mde.Project.Mobile.Pages.Login;
using System.Text.RegularExpressions;
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
            set
            {
                if (SetProperty(ref email, value))
                {
                    ValidateEmail();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (SetProperty(ref name, value))
                {
                    ValidateName();
                }
            }
        }

        private string farmName;
        public string FarmName
        {
            get { return farmName; }
            set
            {
                if (SetProperty(ref farmName, value))
                {
                    if (CanShowFarmName)
                    {
                        ValidateFarmName();
                    }
                }
            }
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

        private string emailError;
        public string EmailError
        {
            get { return emailError; }
            set { SetProperty(ref emailError, value); }
        }

        private string nameError;
        public string NameError
        {
            get { return nameError; }
            set { SetProperty(ref nameError, value); }
        }

        private string farmNameError;
        public string FarmNameError
        {
            get { return farmNameError; }
            set { SetProperty(ref farmNameError, value); }
        }

        public bool IsNameValid => string.IsNullOrEmpty(NameError);
        public bool IsFarmNameValid => string.IsNullOrEmpty(FarmNameError);

        public bool IsPasswordValid => string.IsNullOrEmpty(PasswordError);

        public bool IsEmailValid => string.IsNullOrEmpty(EmailError);

        public bool CanShowFarmName => Role == UserRole.Farmer;

        private void ValidateName()
        {
            NameError = string.IsNullOrWhiteSpace(Name) ? "Name cannot be empty." : string.Empty;
            OnPropertyChanged(nameof(IsNameValid));
        }

        private void ValidateFarmName()
        {
            FarmNameError = string.IsNullOrWhiteSpace(FarmName) ? "Farm name cannot be empty." : string.Empty;
            OnPropertyChanged(nameof(IsFarmNameValid));
        }

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

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email cannot be empty.";
            }
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailError = "Please enter a valid email address.";
            }
            else
            {
                EmailError = string.Empty;
            }

            OnPropertyChanged(nameof(IsEmailValid));
        }

        public ICommand RegisterCommand => new Command(async () =>
        {
            ValidateName();
            if (CanShowFarmName) ValidateFarmName();
            ValidateEmail();
            ValidatePasswords();

            if (!IsNameValid || !IsEmailValid || !IsPasswordValid || (CanShowFarmName && !IsFarmNameValid))
            {
                await Shell.Current.DisplayAlert("Error", "Please correct the highlighted fields.", "OK");
                return;
            }

            var registerResult = await _accountService.RegisterUserAsync(Email, Password, Name, Role, FarmName);

            if (!registerResult.IsSuccess)
            {

                await Shell.Current.DisplayAlert("Error", $"Registration failed: {string.Join(", ", registerResult.Errors)}", "OK");
                return;
            }

            var assignRoleResult = await _accountService.AssignRoleAsync(registerResult.Data, Role);

            if (!assignRoleResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to assign role. Please try again later.", "OK");
                return;
            }

            await ToastHelper.ShowToastAsync($"Thank you for registering! You can now sign in");

            await Shell.Current.GoToAsync(nameof(LoginPage), true);
        });

    }
}

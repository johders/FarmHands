using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Pages.Login;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class RegisterOptionsViewModel : ObservableObject
    {
        public ICommand RegisterAsUserCommand => new Command(async () =>
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { nameof(RegisterViewModel.RoleName), "user" }
            };

            await Shell.Current.GoToAsync(nameof(RegisterPage), true, navigationParameter);
        });

        public ICommand RegisterAsFarmerCommand => new Command(async () =>
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { nameof(RegisterViewModel.RoleName), "farmer" }
            };

            await Shell.Current.GoToAsync(nameof(RegisterPage), true, navigationParameter);
        });
    }
}

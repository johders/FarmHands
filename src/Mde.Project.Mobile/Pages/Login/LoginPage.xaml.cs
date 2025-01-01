using Mde.Project.Mobile.Constants;
using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Login;

public partial class LoginPage : ContentPage
{
    string _deviceToken;
    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        if (Preferences.ContainsKey(AppConstants.DeviceToken))
        {
            _deviceToken = Preferences.Get(AppConstants.DeviceToken, "");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        LoginViewModel viewModel = BindingContext as LoginViewModel;

        await viewModel.CheckConnectionAsync();
    }
}
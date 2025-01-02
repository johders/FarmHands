using Mde.Project.Mobile.Constants;
using Mde.Project.Mobile.Models;
using Mde.Project.Mobile.ViewModels;
using System.Text;
using System.Text.Json;

namespace Mde.Project.Mobile.Pages.Login;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        LoginViewModel viewModel = BindingContext as LoginViewModel;

        await viewModel.CheckConnectionAsync();
    }
}
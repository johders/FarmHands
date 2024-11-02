using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Login;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
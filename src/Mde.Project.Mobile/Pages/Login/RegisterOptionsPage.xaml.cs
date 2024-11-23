using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Login;

public partial class RegisterOptionsPage : ContentPage
{
	public RegisterOptionsPage(RegisterOptionsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
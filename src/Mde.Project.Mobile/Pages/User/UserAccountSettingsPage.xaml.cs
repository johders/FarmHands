using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserAccountSettingsPage : ContentPage
{
	public UserAccountSettingsPage(UserAccountSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
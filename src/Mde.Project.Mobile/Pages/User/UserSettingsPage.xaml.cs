using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserSettingsPage : ContentPage
{
	public UserSettingsPage()
	{
		InitializeComponent();
		BindingContext = new UserPreferencesViewModel();
	}

}
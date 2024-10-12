using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserItemDetailsPage : ContentPage
{
	public UserItemDetailsPage()
	{
		InitializeComponent();
		BindingContext = new UserItemDetailsViewModel();
	}
}
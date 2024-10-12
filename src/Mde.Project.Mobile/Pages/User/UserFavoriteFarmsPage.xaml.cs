using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserFavoriteFarmsPage : ContentPage
{
	public UserFavoriteFarmsPage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel();
	}
}
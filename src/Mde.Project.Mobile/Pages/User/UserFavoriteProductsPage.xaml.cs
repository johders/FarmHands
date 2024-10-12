using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserFavoriteProductsPage : ContentPage
{
	public UserFavoriteProductsPage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel();
	}
}
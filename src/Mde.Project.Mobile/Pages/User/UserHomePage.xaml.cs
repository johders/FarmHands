using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserHomePage : ContentPage
{
	public UserHomePage()
	{
		InitializeComponent();
		BindingContext = new ProductViewModel();
	}
}
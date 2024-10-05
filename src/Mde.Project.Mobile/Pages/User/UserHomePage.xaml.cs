using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.UserPages;

public partial class UserHomePage : ContentPage
{
	public UserHomePage()
	{
		InitializeComponent();
		BindingContext = new ProductViewModel();
	}
}
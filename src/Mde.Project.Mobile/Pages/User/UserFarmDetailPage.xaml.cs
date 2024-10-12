using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserFarmDetailPage : ContentPage
{
	public UserFarmDetailPage()
	{
		InitializeComponent();
		BindingContext = new UserFarmDetailViewModel();
	}
}
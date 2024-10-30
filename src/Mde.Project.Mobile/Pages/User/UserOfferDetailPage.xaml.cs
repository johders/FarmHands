using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserOfferDetailPage : ContentPage
{
	public UserOfferDetailPage(UserOfferDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
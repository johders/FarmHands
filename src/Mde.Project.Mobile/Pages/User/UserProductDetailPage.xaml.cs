using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserProductDetailPage : ContentPage
{
	public UserProductDetailPage(UserProductDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		UserProductDetailsViewModel viewModel = BindingContext as UserProductDetailsViewModel;
		viewModel.CheckIfProductIsFavoriteCommand?.Execute(null);
		base.OnAppearing();
	}
}
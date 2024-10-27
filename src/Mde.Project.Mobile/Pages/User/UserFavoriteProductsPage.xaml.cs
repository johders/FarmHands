using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserFavoriteProductsPage : ContentPage
{
	public UserFavoriteProductsPage(UserFavoriteProductsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		UserFavoriteProductsViewModel viewModel = BindingContext as UserFavoriteProductsViewModel;
		viewModel.RefreshFavoriteProductsListCommand?.Execute(null);
		base.OnAppearing();
	}
}
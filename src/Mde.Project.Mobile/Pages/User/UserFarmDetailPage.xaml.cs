using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserFarmDetailPage : ContentPage
{
	public UserFarmDetailPage(UserFarmDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		UserFarmDetailsViewModel viewModel = BindingContext as UserFarmDetailsViewModel;
		viewModel.CheckIfFarmIsFavoriteCommand?.Execute(null);
		base.OnAppearing();
	}
}
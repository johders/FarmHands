using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserFavoriteFarmsPage : ContentPage
{
	public UserFavoriteFarmsPage(UserFavoriteFarmsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
		UserFavoriteFarmsViewModel viewModel = BindingContext as UserFavoriteFarmsViewModel;
		viewModel.RefreshFavoriteFarmsListCommand?.Execute(null);
        base.OnAppearing();
    }
}
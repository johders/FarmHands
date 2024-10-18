using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserHomePage : ContentPage
{
	public UserHomePage(UserHomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
		UserHomeViewModel viewModel = BindingContext as UserHomeViewModel;
		viewModel.RefreshFarmListCommand?.Execute(null);
		viewModel.RefreshProductListCommand?.Execute(null);
		base.OnAppearing();
    }
}
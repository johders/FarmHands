using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerDashboardPage : ContentPage
{
	public FarmerDashboardPage(FarmerDashboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		FarmerDashboardViewModel viewModel = BindingContext as FarmerDashboardViewModel;
		viewModel.CheckIfUserProfileComplete?.Execute(null);
		viewModel.RefreshOffersListCommand?.Execute(null);
		base.OnAppearing();
	}


}
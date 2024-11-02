using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerInventoryListPage : ContentPage
{
	public FarmerInventoryListPage(FarmerInventoryListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override void OnAppearing()
	{
		FarmerInventoryListViewModel viewModel = BindingContext as FarmerInventoryListViewModel;
		viewModel.RefreshOffersListCommand?.Execute(null);
		base.OnAppearing();
	}
}
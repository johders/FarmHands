using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerInventoryEditPage : ContentPage
{
	public FarmerInventoryEditPage(FarmerInventoryEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        FarmerInventoryEditViewModel viewModel = BindingContext as FarmerInventoryEditViewModel;
        viewModel.InitializeAsync();
        base.OnAppearing();
    }
}
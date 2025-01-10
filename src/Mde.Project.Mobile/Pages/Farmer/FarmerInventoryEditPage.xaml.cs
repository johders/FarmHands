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

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        var viewModel = BindingContext as FarmerInventoryEditViewModel;
        viewModel?.StartEditing();
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        var viewModel = BindingContext as FarmerInventoryEditViewModel;
        viewModel?.StopEditing();
    }
}
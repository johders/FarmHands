using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerSettingsPage : ContentPage
{
	public FarmerSettingsPage(FarmerSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
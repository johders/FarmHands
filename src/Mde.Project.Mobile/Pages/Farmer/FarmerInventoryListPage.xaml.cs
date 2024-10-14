using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerInventoryListPage : ContentPage
{
	public FarmerInventoryListPage()
	{
		InitializeComponent();
		BindingContext = new FarmDashboardViewModel();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(FarmerInventoryEditPage), true);
    }
}
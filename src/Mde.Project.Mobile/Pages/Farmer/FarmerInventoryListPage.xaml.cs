using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerInventoryListPage : ContentPage
{
	public FarmerInventoryListPage()
	{
		InitializeComponent();
		BindingContext = new FarmDashboardViewModel();
	}
}
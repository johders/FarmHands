using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.Farmer;

public partial class FarmerDashboardPage : ContentPage
{
	public FarmerDashboardPage()
	{
		InitializeComponent();
		BindingContext = new FarmDashboardViewModel();
	}

	
}
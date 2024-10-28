using Mde.Project.Mobile.Pages.Farmer;

namespace Mde.Project.Mobile
{
    public partial class AppShellFarmer : Shell
    {
        public AppShellFarmer()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FarmerInventoryEditPage), typeof(FarmerInventoryEditPage));
            Routing.RegisterRoute(nameof(FarmerInventoryListPage), typeof(FarmerInventoryListPage));
        }
    }
}

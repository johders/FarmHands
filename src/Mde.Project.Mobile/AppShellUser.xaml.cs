using Mde.Project.Mobile.Pages.User;

namespace Mde.Project.Mobile
{
    public partial class AppShellUser : Shell
    {
        public AppShellUser()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(UserFarmDetailPage), typeof(UserFarmDetailPage));
            Routing.RegisterRoute(nameof(UserProductDetailPage), typeof(UserProductDetailPage));
        }
    }
}

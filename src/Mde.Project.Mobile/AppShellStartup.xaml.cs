using Mde.Project.Mobile.Pages.Login;

namespace Mde.Project.Mobile
{
    public partial class AppShellStartup : Shell
    {
        public AppShellStartup()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterOptionsPage), typeof(RegisterOptionsPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}

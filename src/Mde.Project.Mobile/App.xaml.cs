namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new AppShellStartup();
            MainPage = new AppShellUser();
		}
    }
}

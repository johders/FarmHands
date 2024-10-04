﻿using Mde.Project.Mobile.Pages.UserPages;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string role = "farmer";
			//string role = "user";

			if (role == "farmer")
            {
                MainPage = new AppShellFarmer();
			}
            else
            {
			    MainPage = new AppShellUser();
            }
		}
    }
}

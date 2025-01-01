using Mde.Project.Mobile.Constants;

namespace Mde.Project.Mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        string _deviceToken;

        public MainPage()
        {
            InitializeComponent();

            if (Preferences.ContainsKey(AppConstants.DeviceToken))
            {
                _deviceToken = Preferences.Get(AppConstants.DeviceToken, "");
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}

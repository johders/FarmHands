using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Mde.Project.Mobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        internal static readonly string Channel_ID = "TestChannel";
        internal static readonly int NotificationId = 101;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CreateNotificationChannel();
        }

        private void CreateNotificationChannel()
        {
            if (OperatingSystem.IsOSPlatformVersionAtLeast("android", 26))
            {
                var channel = new NotificationChannel(Channel_ID, "Test Notification Channel", NotificationImportance.Default);
                var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);

                notificationManager.CreateNotificationChannel(channel);
            }
        }
    }
}

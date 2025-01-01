using Android.App;
using Firebase.Messaging;
using Mde.Project.Mobile.Constants;

namespace Mde.Project.Mobile.Platforms.Android.Services
{
    [Service(Exported = true)]
    [IntentFilter(new[] {"com.google.firebase.MESSAGING_EVENT"})]
    public class FirebaseService : FirebaseMessagingService
    {
        public FirebaseService()
        {
            
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            if (Preferences.ContainsKey(AppConstants.DeviceToken))
            {
                Preferences.Remove(AppConstants.DeviceToken);
            }
            Preferences.Set(AppConstants.DeviceToken, token);
        }
    }
}

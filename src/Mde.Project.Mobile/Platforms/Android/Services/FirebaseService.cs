using Android.App;
using AndroidX.Core.App;
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

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            var notification = message.GetNotification();

            SendNotification(notification.Body, notification.Title, message.Data);
        }

        private void SendNotification(string messagebody, string title, IDictionary<string, string> data)
        {
            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.Channel_ID)
                .SetContentTitle(title)
                .SetSmallIcon(Resource.Mipmap.appicon)
                .SetContentText(messagebody)
                .SetChannelId(MainActivity.Channel_ID)
                .SetPriority(2);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NotificationId, notificationBuilder.Build());
        }
    }
}

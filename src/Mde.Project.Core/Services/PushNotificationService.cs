using FirebaseAdmin.Messaging;
using Google.Cloud.Firestore;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Core.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly FirestoreDb _firestoreDb;

        public PushNotificationService(IFirestoreContext firestoreContext)
        {
            _firestoreDb = firestoreContext.GetFireStoreDb();
        }

        public async Task SendPushNotificationAsync(string deviceToken, string title, string body)
        {
            var message = new Message
            {
                Token = deviceToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"Successfully sent notification: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification: {ex.Message}");
            }
        }

        public async Task NotifyUsersAsync(string productId, string offerDescription)
        {
            var usersQuery = _firestoreDb.Collection("Users").WhereArrayContains("FavoriteProducts", productId);
            var userSnapshots = await usersQuery.GetSnapshotAsync();

            foreach (var document in userSnapshots.Documents)
            {
                var user = document.ConvertTo<Consumer>();

                if (!string.IsNullOrEmpty(user.DeviceToken))
                {
                    await SendPushNotificationAsync(user.DeviceToken, "New Offer Available!", offerDescription);
                }
            }
        }
    }
}

using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Mde.Project.Core.Data.Firestore;
using Mde.Project.Core.Services.Interfaces;
using System.Text.Json;

namespace Mde.Project.Core.Services.Firestore
{
    public class FirestoreContext : IFirestoreContext
    {
        private static FirestoreDb _firestoreDb;

        public FirestoreContext()
        {
            if (_firestoreDb is null)
            {
                InitializeFirebase();
            }
        }

        private void InitializeFirebase()
        {
            try
            {
                var cred = new FsCredentials();
                var jsonCred = JsonSerializer.Serialize(cred);
                var builder = new FirestoreClientBuilder { JsonCredentials = jsonCred };
                _firestoreDb = FirestoreDb.Create("farmhands-431df", builder.Build());
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error initializing Firestore: {ex.Message}");
                throw new InvalidOperationException("Failed to initialize Firestore.", ex);
            }
        }

        public FirestoreDb GetFireStoreDb() => _firestoreDb;
    }
}

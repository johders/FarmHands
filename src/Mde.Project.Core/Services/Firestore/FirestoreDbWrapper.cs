using Google.Cloud.Firestore;
using Mde.Project.Core.Services.Interfaces.Wrappers;

namespace Mde.Project.Core.Services.Firestore
{
    public class FirestoreDbWrapper : IFirestoreDbWrapper
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreDbWrapper(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public CollectionReference Collection(string collectionName)
        {
            return _firestoreDb.Collection(collectionName);
        }

        public DocumentReference Document(string documentPath)
        {
            return _firestoreDb.Document(documentPath);
        }
    }
}

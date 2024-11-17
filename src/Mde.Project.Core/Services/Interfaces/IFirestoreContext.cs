using Google.Cloud.Firestore;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFirestoreContext
    {
        FirestoreDb GetFireStoreDb();
    }
}

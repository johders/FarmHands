using Google.Cloud.Firestore;
using Mde.Project.Core.Services.Interfaces.Wrappers;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFirestoreContext
    {
        FirestoreDb GetFireStoreDb();
        IFirestoreDbWrapper GetFirestoreDbWrapper();
    }
}

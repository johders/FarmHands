using Google.Cloud.Firestore;

namespace Mde.Project.Core.Services.Interfaces.Wrappers
{
    public interface IFirestoreDbWrapper
    {
        CollectionReference Collection(string collectionName);
        DocumentReference Document(string documentPath);
    }
}

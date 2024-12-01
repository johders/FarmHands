using Google.Cloud.Firestore;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class Consumer : ApplicationUserBase
    {
        public Consumer() { }

        [FirestoreProperty]
        public List<string> FavoriteFarms { get; set; } = new();

        [FirestoreProperty]
        public List<string> FavoriteProducts { get; set; } = new();
    }
}

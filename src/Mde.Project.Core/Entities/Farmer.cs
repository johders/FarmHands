using Google.Cloud.Firestore;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class Farmer : ApplicationUserBase
    {
        public Farmer() { }

        [FirestoreProperty]
        public string FarmId { get; set; }

        public Farm Farm { get; set; }
    }
}

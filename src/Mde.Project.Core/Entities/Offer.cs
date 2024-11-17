using Google.Cloud.Firestore;
using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class Offer
    {
        public Offer()
        {
            
        }

        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        [FirestoreProperty]
        public string Variant { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public Unit Unit { get; set; }

        [FirestoreProperty]
        public string FarmId { get; set; }
        public Farm Farm { get; set; }

        [FirestoreProperty]
        public decimal Price { get; set; }

        [FirestoreProperty]
        public string OfferImageUrl { get; set; }

        [FirestoreProperty]
        public bool IsAvailable { get; set; }

        [FirestoreProperty]
        public bool IsOrganic { get; set; }

    }
}

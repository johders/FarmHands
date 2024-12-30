using Google.Cloud.Firestore;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class Farm
	{
		public Farm() { }
        public Farm(string id, string name, string description, double latitude, double longitude, IEnumerable<Product> products, string imageUrl)
		{
			Id = id;
			Name = name;
			Description = description;
			Latitude = latitude;
			Longitude = longitude;
			//Products = products;
			ImageUrl = imageUrl;
		}

        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public double Latitude { get; set; }

        [FirestoreProperty]
        public double Longitude { get; set; }

        [FirestoreProperty]
        public string ImageUrl { get; set; }

        [FirestoreProperty]
        public string OwnerId { get; set; }

        [FirestoreProperty]
        public bool ProifileComplete { get; set; }

        //public IEnumerable<Product> Products { get; set; }

        //public ICollection<FavoriteFarm> FavoriteFarms { get; set; }
    }
}

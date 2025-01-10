using Google.Cloud.Firestore;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class Product
	{
        public Product() { }
        public Product(string id, string name, string description, string imageUrl)
		{
			Id = id;
			Name = name;
			Description = description;
			ImageUrl = imageUrl;
		}

        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public string ImageUrl { get; set; }

    }
}

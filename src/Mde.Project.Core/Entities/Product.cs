using Google.Cloud.Firestore;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class Product : IEquatable<Product>
	{
        public Product() { }
        public Product(string id, string name, string description, string imageUrl)
		{
			Id = id;
			Name = name;
			Description = description;
			ImageUrl = imageUrl;
		}

		public bool Equals(Product? other)
		{
			if (other == null) return false;
			return (this.Name.Equals(other.Name));
		}

        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public string ImageUrl { get; set; }
        //public ICollection<FavoriteProduct> FavoriteProducts { get; set; }

    }
}

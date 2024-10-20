﻿using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Entities
{
	public class Product
	{
		public Product(Guid id, string name, string description, string imageUrl)
		{
			Id = id;
			Name = name;
			Description = description;
			ImageUrl = imageUrl;
		}

		public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }

    }
}

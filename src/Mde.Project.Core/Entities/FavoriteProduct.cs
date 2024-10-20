﻿namespace Mde.Project.Core.Entities
{
    public class FavoriteProduct : FavoriteBase
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime FavoritedOn { get; set; }
    }
}

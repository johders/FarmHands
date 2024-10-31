namespace Mde.Project.Core.Entities
{
    public class FavoriteProduct : FavoriteBase
    {
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime FavoritedOn { get; set; }
    }
}

namespace Mde.Project.Core.Entities
{
	public class ApplicationUser : ApplicationUserBase
	{
        public ICollection<FavoriteFarm> FavoriteFarms { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
    }
}

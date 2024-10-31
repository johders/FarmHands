namespace Mde.Project.Core.Entities
{
    public class FavoriteFarm : FavoriteBase
    {
        public Guid FarmId { get; set; }
        public Farm Farm { get; set; }
        public DateTime FavoritedOn { get; set; }

    }
}

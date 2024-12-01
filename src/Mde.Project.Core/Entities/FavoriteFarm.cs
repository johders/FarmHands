namespace Mde.Project.Core.Entities
{
    public class FavoriteFarm : FavoriteBase
    {
        public string FarmId { get; set; }
        public Farm Farm { get; set; }
        public DateTime FavoritedOn { get; set; }

    }
}

namespace Mde.Project.Core.Entities
{
    public abstract class FavoriteBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

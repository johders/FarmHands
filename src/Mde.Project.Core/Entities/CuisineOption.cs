namespace Mde.Project.Core.Entities
{
	public class CuisineOption
	{
		public CuisineOption(int id, string name, string flagImageUrl)
		{
			Id = id;
			Name = name;
			FlagImageUrl = flagImageUrl;
		}

		public int Id { get; set; }
		public string Name { get; set; }
        public string FlagImageUrl { get; set; }
    }
}

namespace Mde.Project.Core.Entities
{
    public class DietaryOption
    {
		public DietaryOption(int id, string name, bool isSelected)
		{
			Id = id;
			Name = name;
			IsSelected = isSelected;
		}

		public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}

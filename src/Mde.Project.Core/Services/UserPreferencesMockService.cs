using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
namespace Mde.Project.Core.Services
{
	public class UserPreferencesMockService : IUserPreferencesService
	{
		public List<CuisineOption> GetCuisineOptions()
		{
			return new List<CuisineOption>(Seeder.SeedCuisineOptions());
		}

		public List<DietaryOption> GetDietaryOptions()
		{
			return new List<DietaryOption>(Seeder.SeedDietaryOptions());
		}
	}
}

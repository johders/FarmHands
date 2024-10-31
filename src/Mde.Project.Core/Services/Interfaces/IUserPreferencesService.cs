using Mde.Project.Core.Entities;

namespace Mde.Project.Core.Services.Interfaces
{
	public interface IUserPreferencesService
	{
		List<DietaryOption> GetDietaryOptions();
		List<CuisineOption> GetCuisineOptions();
	}
}

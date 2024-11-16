using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFavoriteFarmService
    {
		List<FavoriteFarm> UserFavoriteFarms { get; }
        Task<ResultModel<Farm>> GetAllFavoriteFarmsAsync();
        Task<BaseResultModel> IsFavoritedAsync(string farmId);
        Task<BaseResultModel> CreateAsync(string farmId);
        Task<BaseResultModel> DeleteAsync(string id);
        IQueryable<FavoriteFarm> GetAll();
    }
}

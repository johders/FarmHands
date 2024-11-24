using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFavoriteFarmService
    {
		List<FavoriteFarm> UserFavoriteFarms { get; }
        Task<ResultModel<IEnumerable<Farm>>> GetAllFavoriteFarmsAsync();
        Task<BaseResultModel> IsUserFavoritedAsync(string uid, string farmId);
        Task<ResultModel<IEnumerable<Farm>>> GetAllFavoriteFarmsByUserAsync(string uid);
        Task<BaseResultModel> IsFavoritedAsync(string farmId);
        Task<BaseResultModel> CreateAsync(string farmId);
        Task<BaseResultModel> CreateByUserAsync(string uid, string farmId);
        Task<BaseResultModel> DeleteByUserAsync(string uid, string farmId);
        Task<BaseResultModel> DeleteAsync(string id);
        IQueryable<FavoriteFarm> GetAll();
    }
}

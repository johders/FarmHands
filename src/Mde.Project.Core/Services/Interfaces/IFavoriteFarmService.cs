using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFavoriteFarmService
    {
        Task<ResultModel<FavoriteFarm>> GetByIdAsync(Guid id);
        Task<ResultModel<FavoriteFarm>> GetAllAsync();
        Task<ResultModel<Farm>> GetAllFavoriteFarmsAsync();
        Task<BaseResultModel> CreateAsync(Guid farmId);
        Task<BaseResultModel> UpdateAsync(Guid farmId);
        Task<BaseResultModel> DeleteAsync(Guid id);
        IQueryable<FavoriteFarm> GetAll();
        Task<BaseResultModel> SaveChangesAsync();
    }
}

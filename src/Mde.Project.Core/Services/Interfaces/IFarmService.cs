using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFarmService
    {
        Task<ResultModel<Farm>> GetByIdAsync(Guid id);
        Task<ResultModel<Farm>> GetAllAsync();
        Task<int> GetOfferCountAsync(Guid farmId);
		Task<BaseResultModel> CreateAsync(FarmCreateRequestModel createModel);
        Task<BaseResultModel> UpdateAsync(FarmUpdateRequestModel updateModel);
        Task<BaseResultModel> DeleteAsync(Guid id);
        IQueryable<Farm> GetAll();
        Task<BaseResultModel> SaveChangesAsync();
    }
}

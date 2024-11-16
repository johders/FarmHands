using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFarmService
    {
        Task<ResultModel<Farm>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<Farm>>> GetAllAsync();
        Task<int> GetOfferCountAsync(string farmId);
        Task<BaseResultModel> UpdateAsync(FarmUpdateRequestModel updateModel);
        IQueryable<Farm> GetAll();
    }
}

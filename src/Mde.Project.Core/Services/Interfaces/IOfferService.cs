using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IOfferService
    {
        Task<ResultModel<Offer>> GetByIdAsync(Guid id);
        Task<ResultModel<Offer>> GetAllAsync();
        Task<ResultModel<Offer>> GetAllOffersByFarmIdAsync(Guid farmId);
        Task<ResultModel<Offer>> GetAllOffersByProductIdAsync(Guid productId);
        Task<BaseResultModel> CreateAsync(OfferEditRequestModel createModel);
        Task<BaseResultModel> UpdateAsync(OfferEditRequestModel updateModel);
        Task<BaseResultModel> DeleteAsync(Guid id);
        IQueryable<Offer> GetAll();
        Task<BaseResultModel> SaveChangesAsync();
    }
}

using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IOfferService
    {
        Task<ResultModel<IEnumerable<Offer>>> GetAllAsync();
        Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByFarmIdAsync(string farmId);
        Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByProductIdAsync(string productId);
        Task<BaseResultModel> CreateAsync(OfferEditRequestModel createModel);
        Task<BaseResultModel> UpdateAsync(OfferEditRequestModel updateModel);
        Task<BaseResultModel> DeleteAsync(string id);
        IQueryable<Offer> GetAll();
    }
}

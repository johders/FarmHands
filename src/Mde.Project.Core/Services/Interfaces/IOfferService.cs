using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IOfferService
    {
        Task<ResultModel<IEnumerable<Offer>>> GetAllAsync();
        Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByFarmIdAsync(string farmId);
        Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByProductIdAsync(string productId);
        Task<BaseResultModel> CreateAsync(OfferEditRequestModel createModel, UserRole role);
        Task<BaseResultModel> UpdateAsync(OfferEditRequestModel updateModel, UserRole role);
        Task<BaseResultModel> DeleteAsync(string id, UserRole role);
        IQueryable<Offer> GetAll();
    }
}

using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultModel<Product>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<Product>>> GetAllAsync();
        Task<int> GetOfferCountAsync(string productId);
		Task<BaseResultModel> CreateAsync(ProductCreateRequestModel createModel);
        Task<BaseResultModel> UpdateAsync(ProductUpdateRequestModel updateModel);
        Task<BaseResultModel> DeleteAsync(Guid id);
        IQueryable<Product> GetAll();
        Task<BaseResultModel> SaveChangesAsync();
    }
}

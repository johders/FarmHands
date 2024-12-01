using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultModel<Product>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<Product>>> GetAllAsync();
        Task<int> GetOfferCountAsync(string productId);
        IQueryable<Product> GetAll();
    }
}

using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class ProductMockService : IProductService
    {
        private readonly List<Product> _products = new(Seeder.SeedProducts());

        public Task<BaseResultModel> CreateAsync(ProductCreateRequestModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll()
        {
            return _products.AsQueryable();
        }

        public async Task<ResultModel<Product>> GetAllAsync()
        {
            return await Task.FromResult(new ResultModel<Product>
            {
                IsSuccess = true,
                Data = GetAll().ToList()
            });
        }

        public Task<ResultModel<Product>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(ProductUpdateRequestModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}

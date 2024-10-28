﻿using Mde.Project.Core.Data;
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

        public async Task<ResultModel<Product>> GetByIdAsync(Guid id)
        {
            var product = GetAll().FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return await Task.FromResult(new ResultModel<Product>
				{
					IsSuccess = false,
					Errors = new List<string> { "Product not found!" }
				});
			}

			return await Task.FromResult(new ResultModel<Product>
			{
				IsSuccess = true,
				Data = new List<Product> { product }
			});
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

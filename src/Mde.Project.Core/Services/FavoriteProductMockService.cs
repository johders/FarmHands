using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
	public class FavoriteProductMockService : IFavoriteProductService
	{
		private readonly List<FavoriteProduct> _favoriteProducts = new List<FavoriteProduct>(Seeder.SeedFavoriteProducts());
		private readonly List<Product> _products = new List<Product>(Seeder.SeedProducts());

		public Task<BaseResultModel> CreateAsync(Guid farmId)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResultModel> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<FavoriteProduct> GetAll()
		{
			return _favoriteProducts.AsQueryable();
		}

		public Task<ResultModel<FavoriteProduct>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ResultModel<Product>> GetAllFavoriteProductssAsync()
		{
			var favoriteProducts = GetAll().ToList();
			var products = new List<Product>();
			foreach (var favProduct in favoriteProducts)
			{
				Product product = _products.FirstOrDefault(p => p.Id == favProduct.ProductId);

				if (product is null)
				{
					return new ResultModel<Product>
					{
						IsSuccess = false,
						Errors = new List<string> { "Product not found" }
					};
				}

				favProduct.Product = product;
				products.Add(product);
			}

			return await Task.FromResult(new ResultModel<Product>
			{
				IsSuccess = true,
				Data = products
			});
		}

		public Task<ResultModel<FavoriteProduct>> GetByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<BaseResultModel> IsFavoritedAsync(Guid productId)
		{
			var favoriteProducs = GetAll().ToList();
			if (!favoriteProducs.Any(p => p.ProductId == productId))
			{
				return await Task.FromResult(new BaseResultModel
				{
					IsSuccess = false
				});
			}
			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

		public Task<BaseResultModel> SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<BaseResultModel> UpdateAsync(Guid farmId)
		{
			throw new NotImplementedException();
		}
	}
}

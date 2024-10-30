using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
	public class FavoriteProductMockService : IFavoriteProductService
	{
		private readonly List<Product> _products = new List<Product>(Seeder.SeedProducts());

		public List<FavoriteProduct> UserFavoriteProducts { get; } = Seeder.SeedFavoriteProducts().ToList();
		public async Task<BaseResultModel> CreateAsync(Guid id)
		{
			var isFavorite = GetAll().Any(p => p.ProductId == id);
			if (!isFavorite)
			{
				var product = _products.FirstOrDefault(p => p.Id == id);
				UserFavoriteProducts.Add(new FavoriteProduct
				{
					Id = Guid.NewGuid(),
					Product = product,
					ProductId = product.Id,
					FavoritedOn = DateTime.Now,
				});
			}

			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

		public async Task<BaseResultModel> DeleteAsync(Guid id)
		{
			var favoriteProduct = UserFavoriteProducts.FirstOrDefault(p => p.ProductId == id);

			if(favoriteProduct is null)
			{
				return await Task.FromResult(new BaseResultModel
				{
					IsSuccess = false,
					Errors = new List<string> { "Favorite product not found!" }
				});
			}

			UserFavoriteProducts.Remove(favoriteProduct);

			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

		public IQueryable<FavoriteProduct> GetAll()
		{
			return UserFavoriteProducts.AsQueryable();
		}

		public async Task<ResultModel<FavoriteProduct>> GetAllAsync()
		{
			return await Task.FromResult(new ResultModel<FavoriteProduct>
			{
				IsSuccess = true,
				Data = GetAll().ToList()
			});
		}

		public async Task<ResultModel<Product>> GetAllFavoriteProductsAsync()
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
			var isFavorite = GetAll().Any(p => p.ProductId == productId);
			if (!isFavorite)
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

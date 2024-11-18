using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Pri.Pe1.Hsp.Core.Services.Helpers;

namespace Mde.Project.Core.Services
{
	public class FavoriteProductMockService : IFavoriteProductService
	{
		//private readonly List<Product> _products = new List<Product>(Seeder.SeedProducts());
		private readonly ProductService _productTestService;

		private readonly List<Product> _products = new List<Product>();
		public List<FavoriteProduct> UserFavoriteProducts { get; } = Seeder.SeedFavoriteProducts().ToList();

        public FavoriteProductMockService(ProductService productService)
        {
			_productTestService = productService;
        }

        public async Task InitializeAsync()
        {
            var result = await _productTestService.GetAllAsync();

            if (result.IsSuccess && result.Data != null)
            {
                _products.AddRange(result.Data);
            }
            else
            {
                Console.WriteLine("Failed to fetch products: " + string.Join(", ", result.Errors));
            }
        }

        public async Task<BaseResultModel> CreateAsync(string id)
		{
			var isFavorite = (await GetAll()).Any(p => p.ProductId == id);
			if (!isFavorite)
			{
				var product = _products.FirstOrDefault(p => p.Id == id.ToString());
				UserFavoriteProducts.Add(new FavoriteProduct
				{
					Id = Guid.NewGuid(),
					Product = product,
					ProductId = product.Id,
					FavoritedOn = DateTime.Now,
				});
			}

			return await Task.FromResult(new BaseResultModel());
		}

		public async Task<BaseResultModel> DeleteAsync(string id)
		{
			var favoriteProduct = UserFavoriteProducts.FirstOrDefault(p => p.ProductId == id);

			if(favoriteProduct is null)
			{
				return ResultHelper.CreateErrorResult("Favorite product not found!");
			}

			UserFavoriteProducts.Remove(favoriteProduct);

			return await Task.FromResult(new BaseResultModel());
		}

		public async Task< IQueryable<FavoriteProduct>> GetAll()
		{
			if (_products.Count == 0)
			{
				await InitializeAsync();
			}

            return UserFavoriteProducts.AsQueryable();
		}

		public async Task<ResultModel<IEnumerable<FavoriteProduct>>> GetAllAsync()
		{
            //return await Task.FromResult(new ResultModel<IEnumerable<FavoriteProduct>>
            //         {
            //	Data = GetAll()
            //});

            return new ResultModel<IEnumerable<FavoriteProduct>>
            {
                Data = await GetAll()
            };
        }

		public async Task<ResultModel<IEnumerable<Product>>> GetAllFavoriteProductsAsync()
		{
            var favoriteProducts = (await GetAllAsync()).Data;
			var products = new List<Product>();
			foreach (var favProduct in favoriteProducts)
			{
				Product product = _products.FirstOrDefault(p => p.Id == favProduct.ProductId);

				if (product is null)
				{
					return ResultHelper.CreateErrorResult<IEnumerable<Product>>("Product not found!");
				}

				favProduct.Product = product;
				products.Add(product);
			}

			return await Task.FromResult(new ResultModel<IEnumerable<Product>>
			{
				Data = products
			});
		}

		public async Task<BaseResultModel> IsFavoritedAsync(string productId)
		{
			var isFavorite = (await GetAll()).Any(p => p.ProductId == productId);
			if (!isFavorite)
			{
				return await Task.FromResult(new BaseResultModel
				{
					Errors = new List<string> { "Product not in favorites list!" }
				});
			}
			return await Task.FromResult(new BaseResultModel());
		}

    }
}

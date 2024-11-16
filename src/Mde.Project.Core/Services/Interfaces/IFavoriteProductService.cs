using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
	public interface IFavoriteProductService
	{
		List<FavoriteProduct> UserFavoriteProducts { get; }
		Task<ResultModel<IEnumerable<FavoriteProduct>>> GetAllAsync();
		Task<ResultModel<IEnumerable<Product>>> GetAllFavoriteProductsAsync();
		Task<BaseResultModel> IsFavoritedAsync(string id);
		Task<BaseResultModel> CreateAsync(string id);
		Task<BaseResultModel> DeleteAsync(string id);
		IQueryable<FavoriteProduct> GetAll();
	}
}

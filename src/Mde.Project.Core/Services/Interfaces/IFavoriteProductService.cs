using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
	public interface IFavoriteProductService
	{
		List<FavoriteProduct> UserFavoriteProducts { get; }
		Task<ResultModel<FavoriteProduct>> GetByIdAsync(Guid id);
		Task<ResultModel<FavoriteProduct>> GetAllAsync();
		Task<ResultModel<Product>> GetAllFavoriteProductsAsync();
		Task<BaseResultModel> IsFavoritedAsync(Guid id);
		Task<BaseResultModel> CreateAsync(Guid id);
		Task<BaseResultModel> UpdateAsync(Guid id);
		Task<BaseResultModel> DeleteAsync(Guid id);
		IQueryable<FavoriteProduct> GetAll();
		Task<BaseResultModel> SaveChangesAsync();
	}
}

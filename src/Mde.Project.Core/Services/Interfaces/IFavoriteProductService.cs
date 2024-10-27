using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
	public interface IFavoriteProductService
	{
		Task<ResultModel<FavoriteProduct>> GetByIdAsync(Guid id);
		Task<ResultModel<FavoriteProduct>> GetAllAsync();
		Task<ResultModel<Product>> GetAllFavoriteProductssAsync();
		Task<BaseResultModel> IsFavoritedAsync(Guid farmId);
		Task<BaseResultModel> CreateAsync(Guid farmId);
		Task<BaseResultModel> UpdateAsync(Guid farmId);
		Task<BaseResultModel> DeleteAsync(Guid id);
		IQueryable<FavoriteProduct> GetAll();
		Task<BaseResultModel> SaveChangesAsync();
	}
}

using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
	public interface IFavoriteProductService
	{
        Task<BaseResultModel> IsUserFavoritedAsync(string uid, string productId);
        Task<ResultModel<IEnumerable<Product>>> GetAllFavoriteProductsByUserAsync(string uid);
        Task<BaseResultModel> CreateAsync(string uid, string productId);
        Task<BaseResultModel> DeleteAsync(string uid, string productId);
    }
}

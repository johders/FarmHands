using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFavoriteFarmService
    {
        Task<BaseResultModel> IsUserFavoritedAsync(string uid, string farmId);
        Task<ResultModel<IEnumerable<Farm>>> GetAllFavoriteFarmsByUserAsync(string uid);
        Task<BaseResultModel> CreateAsync(string uid, string farmId);
        Task<BaseResultModel> DeleteAsync(string uid, string farmId);
    }
}

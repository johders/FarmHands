using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
	public interface IApplicationUserService
	{
		Task<ResultModel<ApplicationUser>> GetByIdAsync(Guid id);
		Task<ResultModel<ApplicationUser>> GetAllAsync();
		Task<BaseResultModel> CreateAsync(ApplicationUserCreateRequestModel createModel);
		Task<BaseResultModel> UpdateAsync(ApplicationUserUpdateRequestModel updateModel);
		Task<BaseResultModel> DeleteAsync(Guid id);
		IQueryable<ApplicationUser> GetAll();
		Task<BaseResultModel> SaveChangesAsync();
	}
}

using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;
using Pri.Pe1.Hsp.Core.Services.Helpers;

namespace Mde.Project.Core.Services
{
	public class ApplicationUserMockService : IApplicationUserService
	{
		private readonly List<ApplicationUser> _users = new(Seeder.SeedUsers());

		public Task<BaseResultModel> CreateAsync(ApplicationUserCreateRequestModel createModel)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResultModel> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<ApplicationUser> GetAll()
		{
			return _users.AsQueryable();
		}

		public Task<ResultModel<ApplicationUser>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ResultModel<ApplicationUser>> GetByIdAsync(Guid id)
		{
			var user = GetAll().FirstOrDefault(u => u.Id == id);

			if (user is null)
			{
				return ResultHelper.CreateErrorResult<ApplicationUser>("User not found!");
			}

			return await Task.FromResult(new ResultModel<ApplicationUser>
			{
				IsSuccess = true,
				Data = new List<ApplicationUser> { user }
			});
		}

		public Task<BaseResultModel> SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<BaseResultModel> UpdateAsync(ApplicationUserUpdateRequestModel updateModel)
		{
			throw new NotImplementedException();
		}
	}
}

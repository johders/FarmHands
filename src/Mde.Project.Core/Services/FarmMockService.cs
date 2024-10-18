using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class FarmMockService : IFarmService
    {
        
        public Task<BaseResultModel> CreateAsync(FarmCreateRequestModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Farm> GetAll()
        {
            return Seeder.SeedFarms().AsQueryable();
        }

        public async Task<ResultModel<Farm>> GetAllAsync()
        {
            return await Task.FromResult(new ResultModel<Farm>
            {
                IsSuccess = true,
                Data = GetAll().ToList()
            });
        }

        public Task<ResultModel<Farm>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(FarmUpdateRequestModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}

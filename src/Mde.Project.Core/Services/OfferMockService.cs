using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class OfferMockService : IOfferService
    {
        public Task<BaseResultModel> CreateAsync(OfferCreateRequestModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> GetAll()
        {
            return Seeder.SeedFarmOffers().AsQueryable();
        }

        public async Task<ResultModel<Offer>> GetAllAsync()
        {
            return await Task.FromResult(new ResultModel<Offer>
            {
                IsSuccess = true,
                Data = GetAll().ToList()
            });
        }

        public async Task<ResultModel<Offer>> GetAllOffersByFarmIdAsync(Guid farmId)
        {
            return await Task.FromResult(new ResultModel<Offer>
            {
                IsSuccess = true,
                Data = GetAll().Where(o => o.Farm.Id == farmId).ToList()
            });
        }

        public async Task<ResultModel<Offer>> GetAllOffersByProductIdAsync(Guid productId)
        {
            return await Task.FromResult(new ResultModel<Offer>
            {
                IsSuccess = true,
                Data = GetAll().Where(o => o.Product.Id == productId).ToList()
            });
        }

        public Task<ResultModel<Offer>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(OfferUpdateRequestModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}

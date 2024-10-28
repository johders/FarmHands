using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class OfferMockService : IOfferService
    {
        private readonly List<Offer> _offers = new(Seeder.SeedFarmOffers());
        public async Task<BaseResultModel> CreateAsync(OfferCreateRequestModel createModel)
        {
            var offer = new Offer
            {
                Id = createModel.Id,
                Price = createModel.Price,
                Description = createModel.Description,
                Unit = createModel.Unit,
                Product = createModel.Product,
                Farm = createModel.Farm,
            };

            _offers.Add(offer);

			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

        public Task<BaseResultModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> GetAll()
        {
            return _offers.AsQueryable();
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

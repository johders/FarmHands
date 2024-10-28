using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;
using Pri.Pe1.Hsp.Core.Services.Helpers;

namespace Mde.Project.Core.Services
{
    public class OfferMockService : IOfferService
    {
        private readonly List<Offer> _offers = new(Seeder.SeedFarmOffers());
        public async Task<BaseResultModel> CreateAsync(OfferEditRequestModel createModel)
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

        public async Task<BaseResultModel> DeleteAsync(Guid id)
        {
            var offer = _offers.FirstOrDefault(o => o.Id == id);

			if (offer is null)
			{
				return ResultHelper.CreateErrorResult("Offer not found!");
			}

            _offers.Remove(offer);

			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
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

        public async Task<BaseResultModel> UpdateAsync(OfferEditRequestModel updateModel)
        {
            var offer = await Task.FromResult(GetAll().FirstOrDefault(o => o.Id == updateModel.Id));

            if (offer is null)
            {
				return ResultHelper.CreateErrorResult("Offer not found!");
			}

            offer.Unit = updateModel.Unit;
            offer.Price = updateModel.Price;
            offer.Description = updateModel.Description;
            offer.Product = updateModel.Product;
            offer.Farm = updateModel.Farm;
            offer.IsOrganic = updateModel.IsOrganic;

            return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}
    }
}

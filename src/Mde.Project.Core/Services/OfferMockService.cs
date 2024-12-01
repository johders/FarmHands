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
                Variant = createModel.Variant,
                Unit = createModel.Unit,
                Product = createModel.Product,
                Farm = createModel.Farm,
            };

            _offers.Add(offer);

            return await Task.FromResult(new BaseResultModel());
		}

        public async Task<BaseResultModel> DeleteAsync(string id)
        {
            var offer = _offers.FirstOrDefault(o => o.Id == id);

			if (offer is null)
			{
				return ResultHelper.CreateErrorResult("Offer not found!");
			}

            _offers.Remove(offer);

            return await Task.FromResult(new BaseResultModel());
		}

        public IQueryable<Offer> GetAll()
        {
            return _offers.AsQueryable();
        }

        public async Task<ResultModel<IEnumerable<Offer>>> GetAllAsync()
        {
            return await Task.FromResult(new ResultModel<IEnumerable<Offer>>
            {
                Data = GetAll()
            });
        }

        public async Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByFarmIdAsync(string farmId)
        {
            return await Task.FromResult(new ResultModel<IEnumerable<Offer>>
            {
                Data = GetAll().Where(o => o.Farm.Id == farmId)
            });
        }

        public async Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByProductIdAsync(string productId)
        {
            return await Task.FromResult(new ResultModel<IEnumerable<Offer>>
            {
                Data = GetAll().Where(o => o.Product.Id == productId)
            });
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
            offer.Variant = updateModel.Variant;
            offer.Product = updateModel.Product;
            offer.Farm = updateModel.Farm;
            offer.IsOrganic = updateModel.IsOrganic;

            return await Task.FromResult(new BaseResultModel());
		}
    }
}

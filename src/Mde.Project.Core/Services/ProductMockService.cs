using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Pri.Pe1.Hsp.Core.Services.Helpers;

namespace Mde.Project.Core.Services
{
    public class ProductMockService : IProductService
    {
        private readonly IOfferService _offerService;

		public ProductMockService(IOfferService offerService)
		{
			_offerService = offerService;
		}

        private readonly List<Offer> _offers;
        private readonly List<Product> _products = new(Seeder.SeedProducts());

        public async Task<int> GetOfferCountAsync(string productId)
        {
            var result = await _offerService.GetAllOffersByProductIdAsync(productId);
            return result.Data.Count();
        }

        public IQueryable<Product> GetAll()
        {
            return _products.AsQueryable();
        }

        public async Task<ResultModel<IEnumerable<Product>>> GetAllAsync()
        {
            return await Task.FromResult(new ResultModel<IEnumerable<Product>>
            {
                Data = GetAll()
            });
        }

        public async Task<ResultModel<Product>> GetByIdAsync(string id)
        {
            var product = GetAll().FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
				return ResultHelper.CreateErrorResult<Product>("Product not found!");
			}

			return await Task.FromResult(new ResultModel<Product>
			{
				Data = product
			});
		}

    }
}

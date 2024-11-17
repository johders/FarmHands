using Google.Cloud.Firestore;
using Mde.Project.Core.Constants;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class OfferService : IOfferService
    {

        private readonly FirestoreDb _firestoreDb;
        private readonly ProductService _productService;
        private readonly FarmService _farmService;

        public OfferService(IFirestoreContext firestoreDb, FarmService farmService, ProductService productService)
        {
            _firestoreDb = firestoreDb.GetFireStoreDb();
            _farmService = farmService;
            _productService = productService;
        }

        public Task<BaseResultModel> CreateAsync(OfferEditRequestModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> GetAll()
        {
            //not favorable with firebase
            throw new NotImplementedException();
        }

        public async Task<ResultModel<IEnumerable<Offer>>> GetAllAsync()
        {
            var result = new ResultModel<IEnumerable<Offer>> { Data = new List<Offer>() };

            try
            {
                var offerCollection = _firestoreDb.Collection("Offers");
                var snapshot = await offerCollection.GetSnapshotAsync();
                var offers = new List<Offer>();

                foreach (var document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var offer = document.ConvertTo<Offer>();

                        if (!string.IsNullOrEmpty(offer.FarmId))
                        {
                            var farmResult = await _farmService.GetByIdAsync(offer.FarmId);

                            if (farmResult.IsSuccess)
                            {
                                offer.Farm = farmResult.Data;
                            }
                        }

                        if (!string.IsNullOrEmpty(offer.ProductId))
                        {
                            var productResult = await _productService.GetByIdAsync(offer.ProductId);

                            if (productResult.IsSuccess)
                            {
                                offer.Product = productResult.Data;
                            }
                        }

                        offers.Add(offer);
                    }
                }

                result.Data = offers;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "offers", ex.Message));
            }

            return result;
        }

        public Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByFarmIdAsync(string farmId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByProductIdAsync(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(OfferEditRequestModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}

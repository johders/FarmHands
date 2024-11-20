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
        private readonly IProductService _productService;
        private readonly IFarmService _farmService;

        public OfferService(IFirestoreContext firestoreDb, IFarmService farmService, IProductService productService)
        {
            _firestoreDb = firestoreDb.GetFireStoreDb();
            _farmService = farmService;
            _productService = productService;
        }

        public async Task<BaseResultModel> CreateAsync(OfferEditRequestModel createModel)
        {
            var result = new BaseResultModel();

            try
            {
                var farmResult = await _farmService.GetByIdAsync(createModel.Farm.Id);
                if (!farmResult.IsSuccess || farmResult.Data is null)
                {
                    result.Errors.Add(string.Format(FirestoreMessage.CreateError, nameof(Offer), FirestoreMessage.FarmNotFound));
                    return result;
                }

                var productResult = await _productService.GetByIdAsync(createModel.Product.Id);
                if (!productResult.IsSuccess || productResult.Data is null)
                {
                    result.Errors.Add(string.Format(FirestoreMessage.CreateError, nameof(Offer), FirestoreMessage.ProductNotFound));
                    return result;
                }

                var newOffer = new Offer
                {
                    ProductId = createModel.Product.Id,
                    FarmId = createModel.Farm.Id,
                    Variant = createModel.Variant,
                    Description = createModel.Description,
                    Unit = createModel.Unit,
                    Price = createModel.Price,
                    OfferImageUrl = createModel.OfferImageUrl,
                    IsAvailable = createModel.IsAvailable,
                    IsOrganic = createModel.IsOrganic,
                };

                var offerCollection = _firestoreDb.Collection("Offers");
                await offerCollection.AddAsync(newOffer);
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "offers", ex.Message));
            }

            return result;

        }

        public async Task<BaseResultModel> DeleteAsync(string id)
        {
            var result = new BaseResultModel();

            try
            {
                var offerDoc = _firestoreDb.Collection("Offers").Document(id);
                var snapshot = await offerDoc.GetSnapshotAsync();
                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.OfferNotFound);
                    return result;
                }

                await offerDoc.DeleteAsync();
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.DeleteError, "offer", ex.Message));
            }

            return result;
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

        public async Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByFarmIdAsync(string farmId)
        {
            var result = new ResultModel<IEnumerable<Offer>> { Data = new List<Offer>() };

            try
            {
                var offerQuery = _firestoreDb.Collection("Offers").WhereEqualTo("FarmId", farmId);
                var snapshot = await offerQuery.GetSnapshotAsync();
                var offers = new List<Offer>();

                foreach (var document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var offer = document.ConvertTo<Offer>();

                        var farmResult = await _farmService.GetByIdAsync(offer.FarmId);
                        if (farmResult.IsSuccess)
                        {
                            offer.Farm = farmResult.Data;
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
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "offers by FarmId", ex.Message));
            }

            return result;
        }



        public async Task<ResultModel<IEnumerable<Offer>>> GetAllOffersByProductIdAsync(string productId)
        {
            var result = new ResultModel<IEnumerable<Offer>> { Data = new List<Offer>() };

            try
            {
                var offerQuery = _firestoreDb.Collection("Offers").WhereEqualTo("ProductId", productId);
                var snapshot = await offerQuery.GetSnapshotAsync();
                var offers = new List<Offer>();

                foreach (var document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var offer = document.ConvertTo<Offer>();

                        var farmResult = await _farmService.GetByIdAsync(offer.FarmId);
                        if (farmResult.IsSuccess)
                        {
                            offer.Farm = farmResult.Data;
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
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "offers by ProductId", ex.Message));
            }

            return result;
        }

        public async Task<BaseResultModel> UpdateAsync(OfferEditRequestModel updateModel)
        {
            var result = new BaseResultModel();

            try
            {
                var offerDoc = _firestoreDb.Collection("Offers").Document(updateModel.Id);
                var snapshot = await offerDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.OfferNotFound);
                    return result;
                }

                var offerToUpdate = snapshot.ConvertTo<Offer>();

                if (!string.IsNullOrEmpty(updateModel.Product.Id))
                    offerToUpdate.ProductId = updateModel.Product.Id;

                if (!string.IsNullOrEmpty(updateModel.Farm.Id))
                    offerToUpdate.FarmId = updateModel.Farm.Id;

                if (!string.IsNullOrEmpty(updateModel.Variant))
                    offerToUpdate.Variant = updateModel.Variant;

                if (!string.IsNullOrEmpty(updateModel.Description))
                    offerToUpdate.Description = updateModel.Description;

                if (updateModel.Price != 0)
                    offerToUpdate.Price = updateModel.Price;

                if (!string.IsNullOrEmpty(updateModel.OfferImageUrl))
                    offerToUpdate.OfferImageUrl = updateModel.OfferImageUrl;

                offerToUpdate.IsAvailable = updateModel.IsAvailable;

                offerToUpdate.IsOrganic = updateModel.IsOrganic;

                await offerDoc.SetAsync(offerToUpdate, SetOptions.Overwrite);

            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.UpdateError, nameof(Offer), ex.Message));
            }

            return result;
        }
    }
}

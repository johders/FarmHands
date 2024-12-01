using Google.Cloud.Firestore;
using Mde.Project.Core.Constants;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly IProductService _productService;
        private readonly FirestoreDb _firestoreDb;

        public FavoriteProductService(IProductService productService, IFirestoreContext firestoreContext)
        {
            _productService = productService;
            _firestoreDb = firestoreContext.GetFireStoreDb();
        }

        public async Task<ResultModel<IEnumerable<Product>>> GetAllFavoriteProductsByUserAsync(string uid)
        {
            var result = new ResultModel<IEnumerable<Product>>();

            try
            {
                var consumerDoc = _firestoreDb.Collection("Users").Document(uid);
                var snapshot = await consumerDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.UserNotFound);
                    return result;
                }

                var user = snapshot.ConvertTo<Consumer>();

                var favoriteProducts = new List<Product>();

                foreach (var productId in user.FavoriteProducts)
                {
                    var product = (await _productService.GetByIdAsync(productId)).Data;
                    if (product is not null)
                    {
                        favoriteProducts.Add(product);
                    }
                }

                result.Data = favoriteProducts;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "favorite products", ex.Message));
                return result;
            }
        }

        public async Task<BaseResultModel> CreateAsync(string uid, string productId)
        {
            var result = new BaseResultModel();

            try
            {
                var consumerDoc = _firestoreDb.Collection("Users").Document(uid);
                var snapshot = await consumerDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.UserNotFound);
                    return result;
                }

                var consumer = snapshot.ConvertTo<Consumer>();

                if (consumer is null)
                {
                    result.Errors.Add("Failed to parse consumer data.");
                    return result;
                }

                var favoriteProducts = consumer.FavoriteProducts ?? new List<string>();

                if (favoriteProducts.Contains(productId))
                {
                    result.Errors.Add("This product is already saved in your favorites");
                    return result;
                }

                favoriteProducts.Add(productId);

                var updateFields = new Dictionary<string, object>
                {
                    { "FavoriteProducts", favoriteProducts }
                };

                await consumerDoc.UpdateAsync(updateFields);

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.CreateError, $"favorite product: {productId}", ex.Message));
                return result;
            }
        }

        public async Task<BaseResultModel> IsUserFavoritedAsync(string uid, string productId)
        {
            var result = new BaseResultModel();

            try
            {
                var consumerDoc = _firestoreDb.Collection("Users").Document(uid);
                var snapshot = await consumerDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.UserNotFound);
                    return result;
                }

                var consumer = snapshot.ConvertTo<Consumer>();
                if (consumer is null)
                {
                    result.Errors.Add("Failed to parse consumer data.");
                    return result;
                }

                var favoriteProducts = consumer.FavoriteProducts ?? new List<string>();

                if (favoriteProducts.Contains(productId))
                {
                    return result;
                }
                else
                {
                    result.Errors.Add("Product not in favorites");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "favorite products", ex.Message));
                return result;
            }
        }

        public async Task<BaseResultModel> DeleteAsync(string uid, string productId)
        {
            var result = new BaseResultModel();

            try
            {
                var consumerDoc = _firestoreDb.Collection("Users").Document(uid);
                var snapshot = await consumerDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.UserNotFound);
                    return result;
                }

                var consumer = snapshot.ConvertTo<Consumer>();
                if (consumer is null)
                {
                    result.Errors.Add("Failed to parse consumer data.");
                    return result;
                }

                var favoriteProducts = consumer.FavoriteProducts ?? new List<string>();

                if (!favoriteProducts.Contains(productId))
                {
                    result.Errors.Add("This farm is not in your favorites");
                    return result;
                }

                favoriteProducts.Remove(productId);

                var updateFields = new Dictionary<string, object>
                {
                    { "FavoriteProducts", favoriteProducts }
                };

                await consumerDoc.UpdateAsync(updateFields);

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.DeleteError, $"favorite product: {productId}", ex.Message));
                return result;
            }
        }

    }
}

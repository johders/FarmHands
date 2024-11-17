using Google.Cloud.Firestore;
using Mde.Project.Core.Constants;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly FirestoreDb _firestoreDb;

        public ProductService(IFirestoreContext firestoreDb)
        {
            _firestoreDb = firestoreDb.GetFireStoreDb();
        }

        public IQueryable<Product> GetAll()
        {
            //not favorable with firebase
            throw new NotImplementedException();
        }

        public async Task<ResultModel<IEnumerable<Product>>> GetAllAsync()
        {
            var result = new ResultModel<IEnumerable<Product>> { Data = new List<Product>() };

            try
            {
                var productsCollection = _firestoreDb.Collection("Products");
                var snapshot = await productsCollection.GetSnapshotAsync();
                var products = new List<Product>();

                foreach (var document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var product = document.ConvertTo<Product>();
                        products.Add(product);
                    }
                }

                result.Data = products;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "products", ex.Message));
            }

            return result;

        }

        public async Task<ResultModel<Product>> GetByIdAsync(string id)
        {
            var result = new ResultModel<Product>();

            try
            {
                var productDoc = _firestoreDb.Collection("Products").Document(id);
                var snapshot = await productDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.ProductNotFound);
                    return result;
                }

                var product = snapshot.ConvertTo<Product>();
                result.Data = product;

            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "product by ID", ex.Message));
            }

            return result;
        }

        public Task<int> GetOfferCountAsync(string productId)
        {
            //requires offers
            throw new NotImplementedException();
        }
    }
}

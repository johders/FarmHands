using Google.Cloud.Firestore;
using Mde.Project.Core.Constants;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class FarmService : IFarmService
    {
        private readonly FirestoreDb _firestoreDb;

        public FarmService(IFirestoreContext firestoreDb)
        {
            _firestoreDb = firestoreDb.GetFireStoreDb();
        }

        public async Task<ResultModel<IEnumerable<Farm>>> GetAllAsync()
        {
            var result = new ResultModel<IEnumerable<Farm>> { Data = new List<Farm>() };

            try
            {
                var farmsCollection = _firestoreDb.Collection("Farms");
                var snapshot = await farmsCollection.GetSnapshotAsync();
                var farms = new List<Farm>();

                foreach (var document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var farm = document.ConvertTo<Farm>();
                        farms.Add(farm);
                    }
                }

                result.Data = farms;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "farms", ex.Message));
            }

            return result;
        }

        public async Task<ResultModel<Farm>> GetByIdAsync(string id)
        {
            var result = new ResultModel<Farm>();
            try
            {
                var farmDoc = _firestoreDb.Collection("Farms").Document(id);
                var snapshot = await farmDoc.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    var farm = snapshot.ConvertTo<Farm>();
                    result.Data = farm;
                }
                else
                {
                    result.Errors.Add(FirestoreMessage.FarmNotFound);
                }
            }
            catch(Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "farm by ID", ex.Message));
            }

            return result;

        }

        public Task<int> GetOfferCountAsync(string farmId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(FarmUpdateRequestModel updateModel)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Farm> GetAll()
        {
            //not favorable with firebase
            throw new NotImplementedException();
        }
    }
}

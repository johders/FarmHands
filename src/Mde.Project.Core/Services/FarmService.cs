using Google.Cloud.Firestore;
using Mde.Project.Core.Constants;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Firestore;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Interfaces.Wrappers;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class FarmService : IFarmService
    {
        //private readonly FirestoreDb _firestoreDb;
        private readonly IFirestoreDbWrapper _firestoreDb;
        public FarmService(IFirestoreContext firestoreDb)
        {
            //_firestoreDb = firestoreDb.GetFireStoreDb();
            _firestoreDb = firestoreDb.GetFirestoreDbWrapper();
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

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.FarmNotFound);
                    return result;
                }

                var farm = snapshot.ConvertTo<Farm>();
                result.Data = farm;

            }
            catch(Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "farm by ID", ex.Message));
            }

            return result;

        }

        public async Task<int> GetOfferCountAsync(string farmId)
        {
            try
            {
                var offerCollection = _firestoreDb.Collection("Offers");
                var query = offerCollection
                    .WhereEqualTo("FarmId", farmId)
                    .WhereEqualTo("IsAvailable", true);
                var snapshot = await query.GetSnapshotAsync();
                int offerCount = snapshot.Documents.Count;
                return offerCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(FirestoreMessage.ExceptionMessage, "offer count", ex.Message));
                return 0;
            }
        }

        public async Task<BaseResultModel> UpdateAsync(FarmUpdateRequestModel updateModel)
        {
            var result = new BaseResultModel();

            try
            {
                var farmDoc = _firestoreDb.Collection("Farms").Document(updateModel.Id);
                var snapshot = await farmDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add(FirestoreMessage.FarmNotFound);
                    return result;
                }

                var updateFields = new Dictionary<string, object>
                {
                    { "Id", updateModel.Id },
                    { "Name", updateModel.Name },
                    { "Description", updateModel.Description },
                    { "Latitude", updateModel.Latitude },
                    { "Longitude", updateModel.Longitude },
                    { "ImageUrl", updateModel.ImageUrl },
                    { "ProfileComplete", updateModel.ProfileComplete },
                    { "AddressString", updateModel.AddressString }
                };

                await farmDoc.UpdateAsync(updateFields);

            }
            catch(Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.UpdateError, "farm", ex.Message));
            }

            return result;
        }

        public IQueryable<Farm> GetAll()
        {
            //not favorable with firebase
            throw new NotImplementedException();
        }
    }
}

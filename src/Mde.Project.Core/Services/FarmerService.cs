using Google.Cloud.Firestore;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly FirestoreDb _firestoreDb;
        public FarmerService(IFirestoreContext firestoreContext)
        {
            _firestoreDb = firestoreContext.GetFireStoreDb();
        }

        public async Task<ResultModel<string>> GetFarmIdByFarmerAsync(string farmerUid)
        {
            var result = new ResultModel<string>();

            try
            {
                var farmerDoc = _firestoreDb.Collection("Users").Document(farmerUid);
                var snapshot = await farmerDoc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    result.Errors.Add("Farmer not found");
                    return result;
                }

                var farmer = snapshot.ConvertTo<Farmer>();
                if (farmer is null || string.IsNullOrEmpty(farmer.FarmId))
                {
                    result.Errors.Add("FarmId not found for the specified farmer");
                    return result;
                }

                result.Data = farmer.FarmId;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error retrieving FarmId: {ex.Message}");
                return result;
            }
        }
    }
}

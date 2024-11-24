using Google.Cloud.Firestore;
using Mde.Project.Core.Constants;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Core.Services.Models
{
    public class FavoriteFarmService : IFavoriteFarmService
    {
        private readonly IFarmService _farmService;
        private readonly FirestoreDb _firestoreDb;

        public FavoriteFarmService(IFarmService farmService, IFirestoreContext firestoreContext)
        {
            _farmService = farmService;
            _firestoreDb = firestoreContext.GetFireStoreDb();
        }

        public async Task<ResultModel<IEnumerable<Farm>>> GetAllFavoriteFarmsByUserAsync(string uid)
        {
            var result = new ResultModel<IEnumerable<Farm>>();

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

                var favoriteFarms = new List<Farm>();

                foreach (var farmId in user.FavoriteFarms)
                {
                    var farm = (await _farmService.GetByIdAsync(farmId)).Data;
                    if (farm is not null)
                    {
                        favoriteFarms.Add(farm);
                    }
                }

                result.Data = favoriteFarms;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "favorite farms", ex.Message));
                return result;
            }
        }

        public async Task<BaseResultModel> CreateByUserAsync(string uid, string farmId)
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

                //var consumerRaw = snapshot.GetValue<object>("FavoriteFarms");
                var consumer = snapshot.ConvertTo<Consumer>();

                if (consumer is null)
                {
                    result.Errors.Add("Failed to parse consumer data.");
                    return result;
                }

                var favoriteFarms = consumer.FavoriteFarms ?? new List<string>();

                if (favoriteFarms.Contains(farmId))
                {
                    result.Errors.Add("This farm is already saved in your favorites");
                    return result;
                }

                favoriteFarms.Add(farmId);

                var updateFields = new Dictionary<string, object>
                {
                    { "FavoriteFarms", favoriteFarms }
                };

                await consumerDoc.UpdateAsync(updateFields);

                return result;
            }
            catch(Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.CreateError, $"favorite farm: {farmId}", ex.Message));
                return result;
            }
        }

        public List<FavoriteFarm> UserFavoriteFarms => throw new NotImplementedException();

        public Task<BaseResultModel> CreateAsync(string farmId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FavoriteFarm> GetAll()
        {
            //not favorable in firebase
            throw new NotImplementedException();
        }

        public async Task<ResultModel<IEnumerable<Farm>>> GetAllFavoriteFarmsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResultModel> IsUserFavoritedAsync(string uid, string farmId)
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

                var favoriteFarms = consumer.FavoriteFarms ?? new List<string>();

                if (favoriteFarms.Contains(farmId))
                {
                    return result;
                }
                else
                {
                    result.Errors.Add("Farm not in favorites");
                    return result;
                }
                
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.ExceptionMessage, "favorite farms", ex.Message));
                return result;
            }
        }

        public async Task<BaseResultModel> DeleteByUserAsync(string uid, string farmId)
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

                var favoriteFarms = consumer.FavoriteFarms ?? new List<string>();

                if (!favoriteFarms.Contains(farmId))
                {
                    result.Errors.Add("This farm is not in your favorites");
                    return result;
                }

                favoriteFarms.Remove(farmId);

                var updateFields = new Dictionary<string, object>
                {
                    { "FavoriteFarms", favoriteFarms }
                };

                await consumerDoc.UpdateAsync(updateFields);

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format(FirestoreMessage.DeleteError, $"favorite farm: {farmId}", ex.Message));
                return result;
            }
        }

        public Task<BaseResultModel> IsFavoritedAsync(string farmId)
        {
            throw new NotImplementedException();
        }
    }
}

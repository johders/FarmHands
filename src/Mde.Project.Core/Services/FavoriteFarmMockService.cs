using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Pri.Pe1.Hsp.Core.Services.Helpers;

namespace Mde.Project.Core.Services
{
    public class FavoriteFarmMockService : IFavoriteFarmService
    {
		private readonly IFarmService _farmService;
		public List<FavoriteFarm> UserFavoriteFarms { get; } = Seeder.SeedFavoriteFarms().ToList();

		public FavoriteFarmMockService(IFarmService farmService)
		{
			_farmService = farmService;
		}

		public async Task<BaseResultModel> CreateAsync(Guid farmId)
        {
			var isFavorite = GetAll().Any(f => f.FarmId == farmId);
			if (!isFavorite)
			{
				var farm = _farmService.GetAll().FirstOrDefault(f => f.Id == farmId);
				UserFavoriteFarms.Add(new FavoriteFarm
				{
					Id = Guid.NewGuid(),
					Farm = farm,
					FarmId = farm.Id,
					FavoritedOn = DateTime.Now,
				});
			}

			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

        public async Task<BaseResultModel> DeleteAsync(Guid farmId)
        {
			var favoriteFarm = UserFavoriteFarms.FirstOrDefault(f => f.FarmId == farmId);

			if (favoriteFarm is null)
			{
				return ResultHelper.CreateErrorResult("Favorite farm not found!");
			}

			UserFavoriteFarms.Remove(favoriteFarm);

			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

        public IQueryable<FavoriteFarm> GetAll()
        {
            return UserFavoriteFarms.AsQueryable();
        }

        public Task<ResultModel<FavoriteFarm>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultModel<Farm>> GetAllFavoriteFarmsAsync()
        {
			var favoriteFarms = GetAll().ToList();
			var farms = new List<Farm>();
			foreach (var favFarm in favoriteFarms)
			{
				Farm farm = _farmService.GetAll().FirstOrDefault(f => f.Id == favFarm.FarmId);

				if (farm is null)
				{
					return ResultHelper.CreateErrorResult<Farm>("Farm not found!");
				}

				favFarm.Farm = farm;
				farms.Add(farm);
			}

			return await Task.FromResult(new ResultModel<Farm>
			{
				IsSuccess = true,
				Data = farms
			});
		}

        public Task<ResultModel<FavoriteFarm>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();

        }

        public async Task<BaseResultModel> IsFavoritedAsync(Guid farmId)
        {
			var isFavorite = GetAll().Any(p => p.FarmId == farmId);
			if (!isFavorite)
			{
				return await Task.FromResult(new BaseResultModel
				{
					IsSuccess = false
				});
			}
			return await Task.FromResult(new BaseResultModel
			{
				IsSuccess = true
			});
		}

        public Task<BaseResultModel> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(Guid farmId)
        {
            throw new NotImplementedException();
        }
    }
}

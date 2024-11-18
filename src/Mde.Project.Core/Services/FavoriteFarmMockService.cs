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
        private readonly FarmService _farmTestService;
		
		public List<FavoriteFarm> UserFavoriteFarms { get; } = Seeder.SeedFavoriteFarms().ToList();
        

		public FavoriteFarmMockService(IFarmService farmService, FarmService tester)
		{
			_farmService = farmService;
			_farmTestService = tester;
		}

		public async Task<BaseResultModel> CreateAsync(string farmId)
        {
			var isFavorite = GetAll().Any(f => f.FarmId == farmId);
			if (!isFavorite)
			{
				//var farm = _farmService.GetAll().FirstOrDefault(f => f.Id == farmId);
				var farm = (await _farmTestService.GetByIdAsync(farmId)).Data;

                UserFavoriteFarms.Add(new FavoriteFarm
				{
					Id = Guid.NewGuid(),
					Farm = farm,
					FarmId = farm.Id,
					FavoritedOn = DateTime.Now,
				});
			}

			return await Task.FromResult(new BaseResultModel());
		}

        public async Task<BaseResultModel> DeleteAsync(string farmId)
        {
			var favoriteFarm = UserFavoriteFarms.FirstOrDefault(f => f.FarmId == farmId);

			if (favoriteFarm is null)
			{
				return ResultHelper.CreateErrorResult("Favorite farm not found!");
			}

			UserFavoriteFarms.Remove(favoriteFarm);

			return await Task.FromResult(new BaseResultModel());
		}

        public IQueryable<FavoriteFarm> GetAll()
        {
            return UserFavoriteFarms.AsQueryable();
        }

        public async Task<ResultModel<IEnumerable<Farm>>> GetAllFavoriteFarmsAsync()
        {
			var favoriteFarms = GetAll().ToList();
			var farms = new List<Farm>();
			foreach (var favFarm in favoriteFarms)
			{
				//Farm farm = _farmService.GetAll().FirstOrDefault(f => f.Id == favFarm.FarmId);
                
				var farm = (await _farmTestService.GetByIdAsync(favFarm.FarmId)).Data;

                if (farm is null)
				{
					return ResultHelper.CreateErrorResult<IEnumerable<Farm>>("Farm not found!");
				}

				favFarm.Farm = farm;
				farms.Add(farm);
			}

			return await Task.FromResult(new ResultModel<IEnumerable<Farm>>
            {
				Data = farms
			});
		}

        public async Task<BaseResultModel> IsFavoritedAsync(string farmId)
        {
			var isFavorite = GetAll().Any(p => p.FarmId == farmId);
			if (!isFavorite)
			{
				return await Task.FromResult(new BaseResultModel
				{
					Errors = new List<string> { "Farm not in favorites list" }
				});
			}
			return await Task.FromResult(new BaseResultModel());
		}

    }
}

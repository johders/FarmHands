using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services
{
    public class FavoriteFarmMockService : IFavoriteFarmService
    {
        private readonly List<FavoriteFarm> _favoriteFarms = new(Seeder.SeedFavoriteFarms());
        private readonly List<Farm> _farms = new(Seeder.SeedFarms());

        public async Task<BaseResultModel> CreateAsync(Guid farmId)
        {
            var farm = _farms.FirstOrDefault(f => f.Id == farmId);

            if (farm is null)
            {
                return await Task.FromResult(new BaseResultModel
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Farm not found!" }
                });
            }

            var favorite = new FavoriteFarm
            {
                Id = Guid.NewGuid(),
                FarmId = farm.Id,
                Farm = farm,
                FavoritedOn = DateTime.Now,
            };

            _favoriteFarms.Add(favorite);

            return await Task.FromResult(new BaseResultModel
            {
                IsSuccess = true
            });
        }

        public async Task<BaseResultModel> DeleteAsync(Guid farmId)
        {
            var favoriteFarm = _favoriteFarms.FirstOrDefault(f => f.FarmId == farmId);

            if (favoriteFarm is null)
            {
                return await Task.FromResult(new BaseResultModel
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Farm not found!" }
                });
            }

            _favoriteFarms.Remove(favoriteFarm);

            return await Task.FromResult(new BaseResultModel
            {
                IsSuccess = true
            });
        }

        public IQueryable<FavoriteFarm> GetAll()
        {
            return _favoriteFarms.AsQueryable();
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
                Farm farm = _farms.FirstOrDefault(f => f.Id == favFarm.FarmId);

                if(farm is null)
                {
                    return new ResultModel<Farm>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Farm not found" }
                    };
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
            var favoriteFarms = GetAll().ToList();
            if (!favoriteFarms.Any(f => f.FarmId == farmId))
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

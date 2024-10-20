﻿using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;

namespace Mde.Project.Core.Services
{
    public class FarmMockService : IFarmService
    {
        private readonly List<Farm> _farms = new(Seeder.SeedFarms());


        public Task<BaseResultModel> CreateAsync(FarmCreateRequestModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Farm> GetAll()
        {
            return _farms.AsQueryable();
        }

        public async Task<ResultModel<Farm>> GetAllAsync()
        {
            return await Task.FromResult(new ResultModel<Farm>
            {
                IsSuccess = true,
                Data = GetAll().ToList()
            });
        }

        public async Task<ResultModel<Farm>> GetByIdAsync(Guid id)
        {
            var farm = GetAll().FirstOrDefault(f => f.Id == id);

            if (farm is null)
            {
                return new ResultModel<Farm>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Farm not found!" }
                };
            }

            return await Task.FromResult(new ResultModel<Farm>
            {
                IsSuccess = true,
                Data = new List<Farm> { farm }
            });
        }

        public Task<BaseResultModel> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResultModel> UpdateAsync(FarmUpdateRequestModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}

using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IFarmerService
    {
        Task<ResultModel<string>> GetFarmIdByFarmerAsync(string farmerUid);
    }
}
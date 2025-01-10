using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IOpenStreetService
    {
        Task<ResultModel<List<OpenStreetResult>>> GetCoordinatesFromAddressAsync(string address);
    }
}

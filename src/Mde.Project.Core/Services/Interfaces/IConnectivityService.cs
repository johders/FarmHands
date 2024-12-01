using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IConnectivityService
    {
        BaseResultModel IsConnected();
    }
}

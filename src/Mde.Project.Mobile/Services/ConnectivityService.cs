using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;

namespace Mde.Project.Mobile.Services
{
    public class ConnectivityService : IConnectivityService
    {
        private readonly IConnectivity _connectivity;

        public ConnectivityService(IConnectivity connectivity)
        {
            _connectivity = connectivity;
        }

        public BaseResultModel IsConnected()
        {
            var result = new BaseResultModel();

            if( _connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                result.Errors.Add("You don't seem to be connected to the internet, Please check your connection and try again.");
                return result;
            }

            return result;
        }
    }
}

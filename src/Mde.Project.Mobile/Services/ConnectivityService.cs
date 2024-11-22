using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Mobile.Services
{
    public class ConnectivityService : IConnectivityService
    {
        private readonly IConnectivity _connectivity;

        public ConnectivityService(IConnectivity connectivity)
        {
            _connectivity = connectivity;
        }

        public bool IsConnected()
        {
            return _connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    }
}

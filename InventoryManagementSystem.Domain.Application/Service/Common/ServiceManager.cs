using InventoryManagementSystem.Application.ServiceContracts;
using InventoryManagementSystem.Application.ServiceContracts.Common;

namespace InventoryManagementSystem.Application.Service.Common
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserManagementService> _userManagementServiceFactory;

        public ServiceManager(Func<IUserManagementService> userManagementServiceFactory)
        {
            _userManagementServiceFactory = new Lazy<IUserManagementService>(userManagementServiceFactory);
        }

        public IUserManagementService UserManagementService => _userManagementServiceFactory.Value;
    }
}

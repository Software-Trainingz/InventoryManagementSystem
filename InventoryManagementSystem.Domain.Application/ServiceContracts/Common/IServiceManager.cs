using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.ServiceContracts.Common
{
   public interface IServiceManager
    {
        public IUserManagementService UserManagementService { get;  }
    }
}

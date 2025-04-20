using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSyste_.Core.InfrastructureAbstractions
{
    interface IServiceRegistration
    {
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}

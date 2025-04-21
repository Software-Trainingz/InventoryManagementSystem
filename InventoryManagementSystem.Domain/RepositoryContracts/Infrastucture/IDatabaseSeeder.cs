using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture
{
    public interface IDatabaseSeeder
    {
        Task SeedDatabaseAsync();
    }
}

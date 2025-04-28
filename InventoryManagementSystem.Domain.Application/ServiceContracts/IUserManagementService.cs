using InventoryManagementSystem.Application.DTOs.Cashier;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.DTOs.WareHouseStaff;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.ServiceContracts
{
    public interface IUserManagementService
    {
        // Cashier
        Task AddCashier(CashierCreateDto cashierDto);
        Task UpdateCashier(CashierUpdateDto cashierDto);

        Task DeleteCashier(int cashierId);

       Task< CashierResultDto?> GetCashier(int cashierId);

        Task<IEnumerable<CashierResultDto>> GetAllCashiers();
        // Warehouse Staff
        Task AddWarehouseStaff(WareHouseStaffCreateDto warehouseStaffDto);
        Task UpdateWarehouseStaff(WareHouseStaffCreateDto warehouseStaffDto);
        Task DeleteWarehouseStaff(int warehouseStaffId);

        Task<WareHouseStaffReturnDto?> GetWarehouseStaff(int warehouseStaffId);
        Task<IEnumerable<WareHouseStaffReturnDto>> GetAllWarehouseStaff();
        //Supplier
        Task AddSupplierAsync(SupplierCreateDto supplierDto);
        Task UpdateSupplier(SupplierCreateDto supplierDto);
        Task DeleteSupplier(int supplierId);
        Task<SupplierReturnDto?> GetSupplier(int supplierId);
        Task<IEnumerable<SupplierReturnDto>> GetAllSuppliers();




    }
}

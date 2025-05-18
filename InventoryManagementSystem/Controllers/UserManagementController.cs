using InventoryManagementSystem.APIs.Controllers.Base;
using InventoryManagementSystem.APIs.Exceptions;
using InventoryManagementSystem.Application.DTOs;
using InventoryManagementSystem.Application.DTOs.Cashier;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.DTOs.WareHouseStaff;
using InventoryManagementSystem.Application.Exceptions;
using InventoryManagementSystem.Application.ServiceContracts;
using InventoryManagementSystem.Application.ServiceContracts.Common;
using InventoryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.APIs.Controllers
{
    public class UserManagementController : BaseController
    {
        private readonly IServiceManager _serviceManager;

        public UserManagementController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        #region Cahier
        [HttpGet]//Get//api/UserManagement/Cashiers
        [Route("Cashiers")]
        public async Task<ActionResult<IEnumerable<CashierResultDto>>> GetCashiers()
        {
            var cashiers = await _serviceManager.UserManagementService.GetAllCashiers();

            return Ok(cashiers);
        }
        [HttpGet]//Get//api/UserManagement/Cashier
        [Route("Cashier")]
        public async Task<ActionResult<CashierResultDto>> GetCashier(int id)
        {
            var cashier = await _serviceManager.UserManagementService.GetCashier(id);

            return Ok(cashier);
        }

        [HttpPost]//Post//api/UserManagement/Cashiers

        [Route("Cashier")]//UserManagement/Cashier

        public async Task<ActionResult> AddCashier(CashierCreateDto cashier)
        {
            await _serviceManager.UserManagementService.AddCashier(cashier);

            return CreatedAtAction(
            nameof(GetCashier),
            cashier);
        }


        [HttpPut]//Post//api/UserManagement/Cashiers

        [Route("Cashier")]//UserManagement/Cashier

        public async Task<IActionResult> UpdateCashier([FromBody] CashierUpdateDto cashierDto)
        {
            try
            {
                await _serviceManager.UserManagementService.UpdateCashier(cashierDto);
                return NoContent(); // 204 No Content للتحديث الناجح
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ
                return StatusCode(500, "حدث خطأ أثناء معالجة الطلب");
            }
        }

        [HttpDelete]
        [Route("Cashier")]//UserManagement/Cashier

        public async Task DeleteCashier(int id)
        {
            await _serviceManager.UserManagementService.DeleteCashier(id);
        }



        #endregion

        #region Supplier
        [HttpGet]//Get//api/UserManagement/Suppliers
        [Route("Suppliers")]
       public async Task<ActionResult<IEnumerable<SupplierReturnDto>>> GetSuppliers()
        {
            var suppliers =await _serviceManager.UserManagementService.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpGet]
        [Route("Supplier/{id}")]
        public async Task<ActionResult<SupplierReturnDto>> GetSupplier(int id)
        {
            var supplier =await _serviceManager.UserManagementService.GetSupplier(id);
            return Ok(supplier);
        }

        [HttpPost]
        [Route("Supplier")]
        public async Task<ActionResult> AddSupplier(SupplierCreateDto createDto)
        {
            await _serviceManager.UserManagementService.AddSupplierAsync(createDto);

            return Ok(createDto);
        }
        [HttpPut]

        [Route("Supplier")]
        public async Task<ActionResult> UpdateSupplier(SupplierCreateDto createDto)
        {
            await _serviceManager.UserManagementService.UpdateSupplier(createDto);

            return Ok(createDto);
        }

        [HttpDelete]
        public async Task DeleteSupplier(int id)
        {
            await _serviceManager.UserManagementService.DeleteSupplier(id);
        }


        #endregion


        #region Cahier
        [HttpGet]//Get//api/UserManagement/WareHouseStaffs
        [Route("WareHouseStaffs")]
        public async Task<ActionResult<IEnumerable<WareHouseStaffReturnDto>>> GetWareHouseStaffs()
        {
            var WareHouseStaffs = await _serviceManager.UserManagementService.GetAllWarehouseStaff();

            return Ok(WareHouseStaffs);
        }
        [HttpGet]//Get//api/UserManagement/WareHouseStaff
        [Route("WareHouseStaff/{id}")]
        public async Task<ActionResult<WareHouseStaffReturnDto>> GetWareHouseStaff(int id)
        {
            var WareHouseStaff = await _serviceManager.UserManagementService.GetWarehouseStaff(id);

            return Ok(WareHouseStaff);
        }

        [HttpPost]//Post//api/UserManagement/Cashiers

        [Route("WareHouseStaffs")]//UserManagement/Cashier

        public async Task<ActionResult> AddWareHouseStaff(WareHouseStaffCreateDto wareHouseStaff)
        {
            await _serviceManager.UserManagementService.AddWarehouseStaff(wareHouseStaff);

            return CreatedAtAction(
            nameof(GetCashier),
            wareHouseStaff);
        }


        [HttpPut]//Post//api/UserManagement/Cashiers

        [Route("WareHouseStaff")]//UserManagement/Cashier

        public async Task<IActionResult> UpdateWareHouseStaff([FromBody] WareHouseStaffCreateDto wareHouseStaffDto)
        {
            try
            {
                await _serviceManager.UserManagementService.UpdateWarehouseStaff(wareHouseStaffDto);
                return NoContent(); // 204 No Content للتحديث الناجح
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ
                return StatusCode(500, "حدث خطأ أثناء معالجة الطلب");
            }
        }

        [HttpDelete]
        [Route("WareHouseStaff")]//UserManagement/Cashier

        public async Task DeleteWareHouseStaff(int id)
        {
            await _serviceManager.UserManagementService.DeleteWarehouseStaff(id);
        }





        #endregion


    }
}

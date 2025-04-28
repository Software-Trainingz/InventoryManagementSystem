using InventoryManagementSystem.APIs.Exceptions;
using InventoryManagementSystem.Application.DTOs.Cashier;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.DTOs.WareHouseStaff;
using InventoryManagementSystem.Application.Exceptions;
using InventoryManagementSystem.Application.ServiceContracts;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture;
using Mapster;
using MapsterMapper;

namespace InventoryManagementSystem.Application.Service
{

   public class UserManagementService :IUserManagementService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManagementService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddCashier(CashierCreateDto cashierDto)
        {
            // Validate input
            if (cashierDto is null)
                throw new BadRequestException("Cashier Data cannot be Null");

            // Map DTO to Entity using Mapster
            var cashier = cashierDto.Adapt<Cashier>();

            // Additional validation
            if (string.IsNullOrWhiteSpace(cashier.Name))
                throw new BadRequestException("Cashier name cannot be empty");

            // Add to database
             _unitOfWork.GetRepository<Cashier,int>().Add(cashier);
             await _unitOfWork.CompleteAsync();
        }

      
        public async Task AddSupplierAsync(SupplierCreateDto supplierDto)
        {
            if (supplierDto is null)
             throw new BadRequestException("Supplier Data cannot be empty");

            var supplier=supplierDto.Adapt<Supplier>();
            if (string.IsNullOrWhiteSpace(supplier.Name))
                throw new BadRequestException("Supplier name cannot be empty");
            _unitOfWork.GetRepository<Supplier,int>().Add(supplier);
            await _unitOfWork.CompleteAsync();

        }
        public async Task AddWarehouseStaff(WareHouseStaffCreateDto warehouseStaffDto)
        {
            if (warehouseStaffDto is null)
             throw new BadRequestException("WareHouseStaff Data cannot be empty");
            
            var warehouseStaff= warehouseStaffDto.Adapt<WarehouseStaff>();
            if (string.IsNullOrWhiteSpace(warehouseStaff.Name))
                throw new ArgumentException("Warehouse staff name cannot be empty");

            _unitOfWork.GetRepository<WarehouseStaff, int>().Add(warehouseStaff);

           await  _unitOfWork.CompleteAsync();

        }
        public  async Task DeleteCashier(int cashierId)
        {
            if (cashierId <= 0)
                throw new BadRequestException("Invalid cashier ID");

            var cashier = await _unitOfWork.GetRepository<Cashier, int>().GetAsync(cashierId);

            if (cashier is null)
                throw new NotFoundException($"Cashier with ID {cashierId} not found");

            _unitOfWork.GetRepository<Cashier, int>().Delete(cashier);
        }
        public async Task DeleteSupplier(int supplierId)
        {
            if (supplierId<=0)
                throw new BadRequestException("Invalid supplier ID");

            var supplier = await _unitOfWork.GetRepository<Supplier, int>().GetAsync(supplierId);
            if (supplier == null)
                throw new NotFoundException($"Supplier with ID {supplierId} not found");

            _unitOfWork.GetRepository<Supplier,int>().Delete(supplier);
        }
        public async Task DeleteWarehouseStaff(int warehouseStaffId)
        {
            if (warehouseStaffId <= 0)
                throw new BadRequestException("Invalid warehouseStaffId");

            var wareHouseStaff = await _unitOfWork.GetRepository<WarehouseStaff, int>().GetAsync(warehouseStaffId);

            if (wareHouseStaff is null)
                throw new NotFoundException($"WareHouse Staff  with ID {warehouseStaffId} not found");

            _unitOfWork.GetRepository<WarehouseStaff, int>().Delete(wareHouseStaff);

        }
        public async Task<IEnumerable<CashierResultDto>> GetAllCashiers()
        {
           var cashiers = await  _unitOfWork.GetRepository<Cashier, int>().GetAllAsync();

            if (cashiers is null)
                throw new NotFoundException("No Cashier  Founded");

            return _mapper.Map<IEnumerable<CashierResultDto>>(cashiers);
          
        }
        public async Task<IEnumerable<SupplierReturnDto>> GetAllSuppliers()
        {
            var Suppliers= await _unitOfWork.GetRepository<Supplier,int>().GetAllAsync();
            if (Suppliers is null)
                throw new NotFoundException("No Suppliers Founded");

            return _mapper.Map<IEnumerable<SupplierReturnDto>>(Suppliers);
        }
        public async Task<IEnumerable<WareHouseStaffReturnDto>> GetAllWarehouseStaff()
        {
            var wareHouseStaffs=await _unitOfWork.GetRepository<WarehouseStaff, int>().GetAllAsync();
            if (wareHouseStaffs is null)
                throw new NotFoundException("No WareHoues Staffs Founded");

            return _mapper.Map<IEnumerable<WareHouseStaffReturnDto>>(wareHouseStaffs);
        }
        public async Task<CashierResultDto ?> GetCashier(int cashierId)
        {
            if (cashierId <= 0)
                throw new BadRequestException("Invalid Cashier ID");

            var cashier =await _unitOfWork.GetRepository<Cashier,int>().GetAsync(cashierId);

            if (cashier is null)
                throw new NotFoundException($"No Cashier with ID : {cashierId} ");

            return _mapper.Map<CashierResultDto>(cashier);
        }
        public async Task<SupplierReturnDto?> GetSupplier(int supplierId)
        {
            if (supplierId <= 0)
                throw new BadRequestException("Invalid Supplier ID");

            var supplier = await _unitOfWork.GetRepository<Supplier, int>().GetAsync(supplierId);

            if (supplier is null)
                throw new NotFoundException($"No Supplier with ID : {supplierId} ");

            return _mapper.Map<SupplierReturnDto>(supplier);
        }
        public async Task<WareHouseStaffReturnDto?> GetWarehouseStaff(int warehouseStaffId)
        {
            if (warehouseStaffId <= 0)
                throw new BadRequestException("Invalid WareHouse Staff ID");

            var warehouseStaff = await _unitOfWork.GetRepository<WarehouseStaff, int>().GetAsync(warehouseStaffId);

            if (warehouseStaff is null)
                throw new NotFoundException($"No WarehouseStaff with ID : {warehouseStaffId} ");

            return _mapper.Map<WareHouseStaffReturnDto>(warehouseStaff);

        }
        public async Task UpdateCashier(CashierUpdateDto cashierDto)
        {
            if (cashierDto is null)
                throw new BadRequestException("Cashier Data Cant be Null");

            var cashier =  _mapper.Map<Cashier>(cashierDto);

         ;

           _unitOfWork.GetRepository<Cashier, int>().Update(cashier);

           await _unitOfWork.CompleteAsync();
        }
        public async Task UpdateSupplier(SupplierCreateDto supplierDto)
        {
            if (supplierDto is null) throw new BadRequestException("Supplier Data Cant be  Null");

            var supplier = _mapper.Map<Supplier>(supplierDto);

            _unitOfWork.GetRepository<Supplier, int>().Update(supplier);

            await _unitOfWork.CompleteAsync();

        }
        public async Task UpdateWarehouseStaff(WareHouseStaffCreateDto warehouseStaffDto)
        {
            if (warehouseStaffDto is null) throw new BadRequestException("warehouseStaff Data Cant be  Null");

            var warehouseStaff = _mapper.Map<WarehouseStaff>(warehouseStaffDto);

            _unitOfWork.GetRepository<WarehouseStaff, int>().Update(warehouseStaff);

            await _unitOfWork.CompleteAsync();
        }
    }
}

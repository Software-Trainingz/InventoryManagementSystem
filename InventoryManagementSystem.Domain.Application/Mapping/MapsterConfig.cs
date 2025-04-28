using InventoryManagementSystem.Application.DTOs.Cashier;
using InventoryManagementSystem.Application.DTOs.WareHouseStaff;
using InventoryManagementSystem.Domain.Entities;
using Mapster;
using System;
using System.Linq;

namespace InventoryManagementSystem.Application.Mapping
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            // Forward mappings (Entity → DTO)
            TypeAdapterConfig<Cashier, CashierResultDto>
                .NewConfig()
                .Map(dest => dest.HireDate, src => src.HireDate.ToString("yyyy-MM-dd"))
                .Map(dest => dest.Admin, src => src.Admin == null ? string.Empty : src.Admin.Name)
                .Map(dest => dest.Orders, src => string.Join(", ", src.Orders.Select(o => o.FirstName)));

            TypeAdapterConfig<Supplier, SupplirCreateDto>
                .NewConfig()
                .Map(dest => dest.Products, src => string.Join(", ", src.Products.Select(p => p.Name)));

            TypeAdapterConfig<WarehouseStaff, WareHouseStaffDto>
                .NewConfig()
                .Map(dest => dest.ManageBy, src => src.ManageBy == null ? string.Empty : src.ManageBy.Name)
                .Map(dest => dest.Products, src => string.Join(", ", src.Products.Select(p => p.Name)))
                .Map(dest => dest.StokeMoves, src => string.Join(", ", src.StokeMoves.Select(sm => sm.Id)))
                .Map(dest => dest.UserPermissions, src => string.Join(", ", src.UserPermissions.Select(up => up.PermissionName)));

            // Reverse mappings (DTO → Entity)
            TypeAdapterConfig<CashierCreateDto, Cashier>
                .NewConfig()
                .Map(dest => dest.HireDate, src => DateTime.Parse(src.HireDate))
                .Ignore(dest => dest.Admin);

            TypeAdapterConfig<SupplirCreateDto, Supplier>
                .NewConfig()
                .Ignore(dest => dest.Products);

            TypeAdapterConfig<WareHouseStaffDto, WarehouseStaff>
                .NewConfig()
                .Ignore(dest => dest.ManageBy)
                .Ignore(dest => dest.Products)
                .Ignore(dest => dest.StokeMoves)
                .Ignore(dest => dest.UserPermissions);
        }
    }
}
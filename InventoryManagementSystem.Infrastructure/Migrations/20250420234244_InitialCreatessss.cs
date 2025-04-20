using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatessss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryMethod_IsActive",
                table: "DeliveryMethod");

            migrationBuilder.DropColumn(
                name: "NeedsRestock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsSyncedWithTalabat",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "OptionItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DeliveryMethod");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Cashiers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSyncedWithTalabat",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "OptionItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DeliveryMethod",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Cashiers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NeedsRestock",
                table: "Products",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN ([QuantityOnHand] - [QuantityReserved]) < [MinimumStockLevel] THEN 1 ELSE 0 END");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMethod_IsActive",
                table: "DeliveryMethod",
                column: "IsActive");
        }
    }
}

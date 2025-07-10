using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class Add_PurchaseStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Inventories_InventoryID",
                table: "PurchaseDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "PurchaseDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Quantity] * [UnitPrice]");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "PurchaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 1m);

            migrationBuilder.AlterColumn<Guid>(
                name: "InventoryID",
                table: "PurchaseDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                table: "PurchaseDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "PurchaseDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PurchaseDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    DefaultCostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitDefaultPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[DefaultCostPrice] * 1.15 ", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.Id);
                    table.CheckConstraint("CK_ProductStock_QuantityAvailable", "[QuantityAvailable] >= 0");
                    table.ForeignKey(
                        name: "FK_ProductStocks_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_PurchaseDetail_Quantity",
                table: "PurchaseDetails",
                sql: "[Quantity] >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_InventoryID",
                table: "ProductStocks",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductID",
                table: "ProductStocks",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Inventories_InventoryID",
                table: "PurchaseDetails",
                column: "InventoryID",
                principalTable: "Inventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Inventories_InventoryID",
                table: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PurchaseDetail_Quantity",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PurchaseDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "PurchaseDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "InventoryID",
                table: "PurchaseDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "PurchaseDetails",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Quantity] * [UnitPrice]",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Inventories_InventoryID",
                table: "PurchaseDetails",
                column: "InventoryID",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

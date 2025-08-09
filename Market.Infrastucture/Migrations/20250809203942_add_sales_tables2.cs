using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class add_sales_tables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "SalesDetails",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[UnitPrice] * [Quantity] * [Discount]",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[UnitPrice] * [Quantity]",
                oldStored: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_SalesDeatil_DiscountRange",
                table: "SalesDetails",
                sql: "[Discount] >= 0 AND [Discount] <= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SalesDetail_Quantity",
                table: "SalesDetails",
                sql: "[Quantity] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_SalesDeatil_DiscountRange",
                table: "SalesDetails");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SalesDetail_Quantity",
                table: "SalesDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "SalesDetails",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[UnitPrice] * [Quantity]",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[UnitPrice] * [Quantity] * [Discount]",
                oldStored: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class product_stock_accepted_trigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE TRIGGER trg_UpdateStockAfterApproval
                    ON PurchaseDetails
                    AFTER UPDATE
                    AS
                    BEGIN
                        SET NOCOUNT ON;

                        MERGE ProductsStocks AS target
                        USING (
                            SELECT i.InventoryID, i.ProductID, i.Quantity, i.UnitPrice
                            FROM inserted i
                            INNER JOIN deleted d ON i.id = d.id
                            WHERE i.Status = 1 AND d.Status <> 1
                        ) AS src
                        ON target.InventoryID = src.InventoryID
                           AND target.ProductID = src.ProductID

                        WHEN MATCHED THEN
                            UPDATE SET 
                                target.QuantityAvailable = target.QuantityAvailable + src.Quantity,
                                target.DefaultCostPrice = src.UnitPrice

                        WHEN NOT MATCHED THEN
                            INSERT (InventoryID, ProductID, QuantityAvailable, DefaultUnitPrice)
                            VALUES (src.InventoryID, src.ProductID, src.Quantity, src.UnitPrice);
                    END;
                    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS trg_UpdateStockAfterApproval;");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class Add_TrigarForTotalPurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"  CREATE TRIGGER trg_UpdateInvoiceTotal
                             ON PurchaseDetails
                             AFTER INSERT, UPDATE, DELETE
                             AS
                             BEGIN
                             SET NOCOUNT ON;

                             UPDATE I
                             SET TotalAmount = 
                             (
                             SELECT SUM(Quantity * UnitPrice) FROM PurchaseDetails d 
                             WHERE d.PurchaseInvoiceId = I.Id 
                             )
           
                             FROM PurchaseInvoices I
                             WHERE I.Id IN 
                             (
                             SELECT DISTINCT PurchaseInvoiceId FROM inserted
                             UNION
                             SELECT DISTINCT PurchaseInvoiceId FROM deleted
                             )
                             END");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS trg_UpdateInvoiceTotal;");
        }
    }
}

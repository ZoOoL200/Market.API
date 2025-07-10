using Market.Domain.Entity.Main;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Operations;

public class PurchaseDetail
{
    [Key]
    public long Id { get; set; }

    [ForeignKey(nameof(PurchaseInvoice))]
    [Required]
    public long PurchaseInvoiceId { get; set; }

    [ForeignKey(nameof(Product) )]
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; } 
    public decimal TotalPrice  { get; private set; }

    public bool Status { get; set; }

    // Tdo: Navigation with Users for Aprroved 
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }

    [ForeignKey(nameof(Inventory))]
    public Guid? InventoryID { get; set; }


    // Navigation properties
    public PurchaseInvoice PurchaseInvoice { get; set; } = default!;
    public Product Product { get; set; } = default!;

    public Inventory Inventory { get; set; } = default!;
    // Tdo: Navigation with Users for Aprroved 
}

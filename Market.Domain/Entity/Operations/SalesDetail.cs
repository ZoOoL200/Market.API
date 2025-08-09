using Market.Domain.Entity.Main;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Operations;

public class SalesDetail
{
    [Key]
    public long Id { get; set; }

   [Required]
    [ForeignKey("SalesInvoice")]
    public long InvoiceID { get; set; }

    [Required]
    [ForeignKey("Product")]
    public Guid ProductID { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; } 
    public float Discount { get; set; } = 0.0f;
    public decimal TotalPrice { get; private set; }
    // Navigation properties
    public SalesInvoice SalesInvoice { get; set; } = default!;
    public Product Product { get; set; } = default!;
}

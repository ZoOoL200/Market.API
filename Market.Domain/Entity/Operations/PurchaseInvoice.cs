using Market.Domain.Entity.Main;
using Market.Domain.Entity.Management;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Operations;

public class PurchaseInvoice
{
    [Key]
    public long Id { get; set; }

    [ForeignKey("Supplier")]
    [Required]
    public Guid SupplierID { get; set; }

    [ForeignKey("Branch")]
    [Required]
    public Guid BranchID { get; set; }

    [Required]
    public DateTime PurchaseDate { get; private set; }

    public decimal TotalAmount { get; private set; } 

    // Navigation properties
    public Supplier Supplier { get; set; } = default!;
    public Branch Branch { get; set; } = default!;
    public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = [];
}

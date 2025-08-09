using Market.Domain.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Operations;
[Index(nameof(InvoiceNumber), IsUnique = true)]
public class SalesInvoice
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = default!;
    [Required]
    [ForeignKey("Branch")]
    public Guid BranchID { get; set; }
    public string CustomerNmae { get; set; } = default!;
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public Guid EmployeeID { get; set; }
    public decimal TotalAmount { get; set; }

    // Navigation properties
    public Branch Branch { get; set; } = default!;
    public ICollection<SalesDetail> SalesDetails { get; set; } = [];
}

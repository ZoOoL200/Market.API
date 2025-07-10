using Market.Domain.Entity.HR;
using Market.Domain.Entity.Operations;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entity.Management;

public class Supplier
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string SupplierName { get; set; } = default!;

    public string? Address { get; set; }

    // Navigation properties
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
}

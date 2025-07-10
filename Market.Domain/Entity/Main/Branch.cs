using Market.Domain.Entity.Operations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entity.Main;

[Index(nameof(Title), IsUnique = true)]
public class Branch
{
    [Key]
    public Guid Id { get; private set; }

    [MaxLength(30)]
    [Required]
    public string Title { get; set; } = default!;

    [MaxLength(100)]
    public string? Location { get; set; } 

    // Navigation properties
    public ICollection<Inventory> Inventories { get; set; } = [];
    public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } =[];
}

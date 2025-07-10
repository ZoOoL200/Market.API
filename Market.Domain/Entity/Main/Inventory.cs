using Market.Domain.Entity.Operations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Main;

[Index(nameof(Title), IsUnique = true)]
public class Inventory
{
    [Key]
    public Guid Id { get; private set; }

    [MaxLength(30)]
    [Required]
    public string Title { get; set; } = default!;

    [ForeignKey("Branch")]
    [Required]
    public Guid BranchID { get; set; }

    // Navigation properties
    public Branch Branch { get; set; } = default!;
    public ICollection<PurchaseDetail> PurchaseDetails { get; set; } =  [];
    public ICollection<ProductStock> ProductStocks { get; set; } = [];
}

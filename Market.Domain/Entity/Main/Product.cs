using Market.Domain.Entity.Operations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Main;

[Index(nameof(Title), IsUnique = true)]
public class Product
{
    [Key]
    public Guid Id { get; private set; }

    [MaxLength(50)]
    [Required]
    public string Title { get; set; } = default!;

    public string? Description { get; set; } 

    [ForeignKey("Category")]
    [Required]
    public short CategoryID { get; set; }

    // Navigation properties
    public Category Category { get; set; } = default!;
    public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = [];
    public ICollection<ProductStock> ProductStocks { get; set; } = [];
    public ICollection<SalesDetail> SalesDetails { get; set; } = [];
}

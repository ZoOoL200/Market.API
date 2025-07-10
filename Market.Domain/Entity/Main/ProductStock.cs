using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.Main;

public class ProductStock
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Inventory))]
    [Required]
    public Guid InventoryID { get; set; }

    [ForeignKey(nameof(Product))]
    [Required]
    public Guid ProductID { get; set; }
    
    public int QuantityAvailable { get; set; }
    public decimal DefaultCostPrice { get; set; }

    public decimal UnitDefaultPrice { get; set; }
    
    // Navigation properties
    public Inventory Inventory { get; set; } = default!;
    
    public Product Product { get; set; }= default!;
}

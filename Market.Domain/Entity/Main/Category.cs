using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entity.Main;

[Index(nameof(Title), IsUnique = true)]
public class Category
{
    [Key]
    public short Id { get; set; }

    [MaxLength(30)]
    [Required]
    public string Title { get; set; } = default!;

    public string? Description { get; set; }
    // Navigation property for related products
    public ICollection<Product> Products { get; set; } = [];
}
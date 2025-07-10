using Market.Domain.Entity.Management;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity.HR;
public class Contact
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Supplier")]
    [Required]
    public Guid PersonID { get; set; }

    [Required]
    [ForeignKey("Country")]
    public int CountryID { get; set; }

    [Required]
    public string Telephone { get; set; } = default!;
    // navigation property
    public CountryKey Country { get; set; } = default!;
    public Supplier Supplier { get; set; } = default!;
}

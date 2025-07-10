using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entity.HR;

[Index(nameof(Key), nameof(CountryName), IsUnique =true) ]
public class CountryKey
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(30)]
    [Required]
    public  string CountryName { get; set; } =default!;

    [MaxLength(7)]
    [Required]
    public  string Key { get; set; } = default!;

    //Relation with StudentContact Table
    public  ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}

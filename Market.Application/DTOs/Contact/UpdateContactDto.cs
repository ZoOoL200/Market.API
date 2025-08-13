
namespace Market.Application.DTOs.Contact;

public class UpdateContactDto 
{
    public int Id { get; set; }
    public int CountryID { get; set ; }
    public string Telephone { get ; set ; } = default!;
}

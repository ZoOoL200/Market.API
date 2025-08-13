
namespace Market.Application.DTOs.Contact;

public class CreatContactDto 
{
    public Guid PersonID { get; set ; }
    public int CountryID { get ; set ; }
    public string Telephone { get; set; } = default!;
}

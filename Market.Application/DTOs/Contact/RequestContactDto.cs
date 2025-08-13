using Market.Application.DTOs.Contact.Comman;

namespace Market.Application.DTOs.Contact;

public class RequestContactDto 
{
    public int Id { get; set; } 
    public string PersonName { get; set; } = default!;
    public string CountryName { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string Telephone { get; set; } = default!;
}


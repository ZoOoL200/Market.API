using Market.Application.DTOs.Contact;
using Market.Application.DTOs.Supliers;
using Market.Domain.Entity.HR;
using Market.Domain.Entity.Management;

namespace Market.Application.Profiles;

internal class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        // Contact mappings
        CreateMap<Contact, UpdateContactDto>().ReverseMap();
        CreateMap<Contact, CreatContactDto>().ReverseMap();
        CreateMap<Contact, RequestContactDto>()
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Supplier.SupplierName))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Country.Key)).ReverseMap();


        CreateMap<Supplier, SuplierDto>().ReverseMap();
        CreateMap<Supplier, CreateSuplierDto>().ReverseMap();
    }
    
}


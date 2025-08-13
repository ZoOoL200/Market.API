using Market.Application.DTOs.Contact;
using MediatR;

namespace Market.Application.Features.Contact.Requests.Commands;

public record InsertContactRequest(CreatContactDto Contact) : IRequest<RequestContactDto>
{
    
}

public record InsertListContactRequest(IList<CreatContactDto> Contacts) : IRequest<List<RequestContactDto>>
{
    
}
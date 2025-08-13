using Market.Application.DTOs.Contact;
using MediatR;

namespace Market.Application.Features.Contact.Requests.Commands;

public record UpdateContactRequest(UpdateContactDto ContactDto): IRequest<RequestContactDto>
{

}

public record UpdateListContactRequest(IList<UpdateContactDto> Contacts) : IRequest<List<RequestContactDto>>
{

}



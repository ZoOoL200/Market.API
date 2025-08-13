using AutoMapper;
using Market.Application.DTOs.Contact;
using Market.Application.DTOs.Contact.Validtors;
using Market.Application.Exceptions;
using Market.Application.Features.Contact.Requests.Commands;
using Market.Application.Presistences.UnitofWork;
using Market.Domain.Entity.HR;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Market.Application.Features.Contact.Handlers.Commands;

public class UpdateContactRequestHandler(IUnitofWork unitofWork, IMapper mapper) : IRequestHandler<UpdateContactRequest, RequestContactDto>
{
     public async Task<RequestContactDto> Handle(UpdateContactRequest request, CancellationToken cancellationToken)
    {
        // Validate the contact data before proceeding with the update
        var validator = new UpdateContactVaildtor(unitofWork);
        var validationResult = await validator.ValidateAsync(request.ContactDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var allMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(allMessages);
        }
        var contactEntity = mapper.Map<Domain.Entity.HR.Contact>(request.ContactDto);

        // Attempt to retrieve the existing contact by ID and update it
        try
        {
            var contact = await unitofWork.ContactRepo.GetByIdAsync(contactEntity.Id);
            mapper.Map(contactEntity, contact);
            await unitofWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<RequestContactDto>(contactEntity);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException($"Unexpected server error occurred.{ex}");
        }
    }
}

public class UpdateListContactRequestHandler(IUnitofWork unitofWork, IMapper mapper) : IRequestHandler<UpdateListContactRequest, List<RequestContactDto>>
{

    public async Task<List<RequestContactDto>> Handle(UpdateListContactRequest request, CancellationToken cancellationToken)
    {
        // Validate each contact in the list
        var validator = new UpdateContactVaildtor(unitofWork);
        var allErrors = new List<string>();

        for (int i = 0; i < request.Contacts.Count; i++)
        {
            var contact = request.Contacts[i];
            var result = await validator.ValidateAsync(contact, cancellationToken);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    allErrors.Add($"Contact #{i + 1}: {error.ErrorMessage}");
                }
            }
        }
        if (allErrors.Count > 0)
        {
            string allMessages = string.Join(Environment.NewLine, allErrors);
            throw new ValidationException(allMessages);
        }

        // If validation passes, proceed with the update
        try
        {
            foreach (var contact in request.Contacts)
            {
                var contactEntity = mapper.Map<Domain.Entity.HR.Contact>(contact);
                var existingContact = await unitofWork.ContactRepo.GetByIdAsync(contactEntity.Id );

                mapper.Map(contactEntity, existingContact);

            }
            await unitofWork.SaveChangesAsync(cancellationToken);
            var ids = request.Contacts.Select(c => c.Id).ToList();
            var Contacts = await unitofWork.ContactRepo.GetFullSpecificContactsByIdAsync(ids);
            return Contacts;
            
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException($"Unexpected server error occurred.{ex}");
        }

    }
}

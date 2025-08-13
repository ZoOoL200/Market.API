using AutoMapper;
using Market.Application.DTOs.Contact;
using Market.Application.DTOs.Contact.Validtors;
using Market.Application.Exceptions;
using Market.Application.Features.Contact.Requests.Commands;
using Market.Application.Presistences.UnitofWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Market.Application.Features.Contact.Handlers.Commands;
/// <summary>
/// Handles the insertion of a new contact into the system.
/// </summary>
/// <remarks>This handler processes an <see cref="InsertContactRequest"/> to add a new contact to the database. It
/// maps the incoming request data to a domain entity, persists the entity using the provided unit of work, and returns
/// a DTO representing the newly created contact.</remarks>
/// <param name="unitofWork">The unit of work used to manage database operations. Cannot be null.</param>
/// <param name="mapper">The mapper used to convert between request, domain, and DTO objects. Cannot be null.</param>
public class InsertContactRequestHandler(IUnitofWork unitofWork, IMapper mapper) : IRequestHandler<InsertContactRequest, RequestContactDto>
{
    public async Task<RequestContactDto> Handle(InsertContactRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreatContactValidtor(unitofWork);
        var validationResult = await validator.ValidateAsync(request.Contact, cancellationToken);
        if (!validationResult.IsValid)
        {
            var allMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(allMessages);
        }

        var contactEntity = mapper.Map<Domain.Entity.HR.Contact>(request.Contact);

        try
        {
            await unitofWork.ContactRepo.AddAsync(contactEntity);
            await unitofWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<RequestContactDto>(contactEntity);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException($"Unexpected server error occurred.{ex}");

        }

    }
}
/// <summary>
/// Handles the insertion of a list of contacts into the data store and returns the inserted contacts as DTOs.
/// </summary>
/// <remarks>This handler processes an <see cref="InsertListContactRequest"/> by mapping the provided contact data
/// to  domain entities, saving them to the repository, and then mapping the saved entities back to DTOs for the
/// response.</remarks>
/// <param name="unitofWork"></param>
/// <param name="mapper"></param>
public class InsertListContactRequestHandler(IUnitofWork unitofWork, IMapper mapper) : IRequestHandler<InsertListContactRequest, List<RequestContactDto>>
{

    public async Task<List<RequestContactDto>> Handle(InsertListContactRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreatContactValidtor(unitofWork);
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

        var contactEntities = mapper.Map<List<Domain.Entity.HR.Contact>>(request.Contacts);
        await unitofWork.ContactRepo.AddAsync(contactEntities);
        await unitofWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<List<RequestContactDto>>(contactEntities);
    }
}

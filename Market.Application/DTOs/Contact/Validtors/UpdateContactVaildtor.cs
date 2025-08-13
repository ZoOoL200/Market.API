using FluentValidation;
using Market.Application.Presistences.UnitofWork;

namespace Market.Application.DTOs.Contact.Validtors;

internal class UpdateContactVaildtor : AbstractValidator<UpdateContactDto>
{
    private readonly IUnitofWork unitofWork;
    public UpdateContactVaildtor(IUnitofWork _unitofWork)
    {
        unitofWork = _unitofWork;
        RuleFor(c => c.Id)
           .NotEmpty().WithMessage("Contact ID is required.")
           .MustAsync(async (id, token) =>
           {
               return await unitofWork.ContactRepo.IsExistsAsync(id);
           }).WithMessage("Contact with the specified ID does not exist.");

        RuleFor(c => c.Telephone)
           .NotEmpty().WithMessage("Telephone Number is required.")
           .Matches(@"^[0-9\-]+$").WithMessage("Telephone Number must be just numbers.")
           .MaximumLength(15);

        RuleFor(c => c.CountryID)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MustAsync(async (id, token) =>
           {
               return await unitofWork.CountryKeyRepo.IsExistsAsync(id);
           }).WithMessage("{PropertyName} Is not Exists");

    }
}

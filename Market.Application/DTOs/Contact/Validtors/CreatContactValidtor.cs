using FluentValidation;
using Market.Application.Presistences.UnitofWork;

namespace Market.Application.DTOs.Contact.Validtors;

internal class CreatContactValidtor: AbstractValidator<CreatContactDto>
{
    private readonly IUnitofWork unitofWork;
    public CreatContactValidtor(IUnitofWork _unitofWork)
    {
        unitofWork = _unitofWork;

        RuleFor(c => c.Telephone)
            .NotEmpty().WithMessage("Telephone Number is required.")
            .Matches(@"^[0-9\-]+$").WithMessage("{PropertyName} must be just numbers.")
            .MaximumLength(15);
        
        RuleFor(c => c.CountryID)
            .NotEmpty().WithMessage(" {PropertyName} is required.")
            .MustAsync(async(id, token) => 
            {
                return await unitofWork.CountryKeyRepo.IsExistsAsync(id);
            }).WithMessage("{PropertyName} Is not Exists");
        
        RuleFor(c => c.PersonID)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async(id, token)=>
            {
                return await unitofWork.SupplierRepo.IsExistsAsync(id);
            }).WithMessage("{PropertyName} is not exists");
    }
}

using FluentValidation;
using WarehouseService.ViewModels.Request;

namespace WarehouseService.Validators
{
    public class CreateUnitValidator : AbstractValidator<CreateUnitDto>
    {
        public CreateUnitValidator()
        {
            RuleFor(u => u.Name).NotEmpty();
        }
    }

    public class UpdateUnitValidator : AbstractValidator<UpdateUnitDto>
    {
        public UpdateUnitValidator()
        {
            RuleFor(u => u.Name).NotEmpty();
        }
    }
}

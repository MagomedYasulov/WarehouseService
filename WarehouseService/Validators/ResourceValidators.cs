using FluentValidation;
using WarehouseService.ViewModels.Request;

namespace WarehouseService.Validators
{
    public class CreateResourceValidator : AbstractValidator<CreateResourceDto>
    {
        public CreateResourceValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }

    public class UpdateResourceValidator : AbstractValidator<UpdateResourceDto>
    {
        public UpdateResourceValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}

using FluentValidation;
using WarehouseService.ViewModels.Request;

namespace WarehouseService.Validators
{
    public class CreateReceiptDocuementValidtor : AbstractValidator<CreateReceiptDocumentDto>
    {
        public CreateReceiptDocuementValidtor()
        {
            RuleFor(rd => rd.DocumentNumber).NotEmpty();
            RuleFor(rd => rd.ReceiptDate).NotEmpty(); //TODO: ПРОВЕРКУ НА UTC
            RuleFor(rd => rd.ReceiptResources).NotNull();
            RuleForEach(rd => rd.ReceiptResources).SetValidator(new CreateReceiptResourceValidator());
        }
    }

    public class CreateReceiptResourceValidator : AbstractValidator<CreateReceiptResourceDto>
    {
        public CreateReceiptResourceValidator()
        {
            RuleFor(r => r.Quantity).GreaterThan(0);
            RuleFor(r => r.ResourceId).NotEmpty();
            RuleFor(r => r.UnitId).NotEmpty();
        }
    }

    public class UpdateReceiptDocuementValidtor : AbstractValidator<UpdateReceiptDocumentDto>
    {
        public UpdateReceiptDocuementValidtor()
        {
            RuleFor(rd => rd.DocumentNumber).NotEmpty();
            RuleFor(rd => rd.ReceiptDate).NotEmpty(); //TODO: ПРОВЕРКУ НА UTC
            RuleFor(rd => rd.ReceiptResources).NotNull();
            RuleForEach(rd => rd.ReceiptResources).SetValidator(new UpdateReceiptResourceValidator());
        }
    }

    public class UpdateReceiptResourceValidator : AbstractValidator<UpdateReceiptResourceDto>
    {
        public UpdateReceiptResourceValidator()
        {
            RuleFor(r => r.Quantity).GreaterThan(0);
            RuleFor(r => r.ResourceId).NotEmpty();
            RuleFor(r => r.UnitId).NotEmpty();
        }
    }
}

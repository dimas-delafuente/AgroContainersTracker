using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain.ProductManagement;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class ProductEntryValidator : AbstractValidator<ProductEntry>
    {
        private const string DATE_OUTSIDE_CAMPAING_MESSAGE = "La fecha introducida no corresponde a la campaña asignada.";

        public ProductEntryValidator()
        {
            RuleFor(v => v.CampaingId)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(v => v.EntryDate)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .Must((entry, value) => {
                    return value.Year.Equals(entry.CampaingId);
                }).WithMessage(DATE_OUTSIDE_CAMPAING_MESSAGE);

            RuleFor(v => v.ProductEntryNumber)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Sellers)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(v => v.Buyer)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(v => v.Payer)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

        }
    }
}

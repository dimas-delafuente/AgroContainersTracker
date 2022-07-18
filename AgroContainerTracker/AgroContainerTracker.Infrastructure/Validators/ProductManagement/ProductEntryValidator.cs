using AgroContainerTracker.Domain.ProductManagement;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class InputValidator : AbstractValidator<Input>
    {
        private const string DATE_OUTSIDE_Campaign_MESSAGE = "La fecha introducida no corresponde a la campaña asignada.";

        public InputValidator()
        {
            RuleFor(v => v.CampaignId)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(v => v.EntryDate)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .Must((entry, value) => value.Year.Equals(entry.CampaignId)).WithMessage(DATE_OUTSIDE_Campaign_MESSAGE);

            RuleFor(v => v.InputNumber)
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

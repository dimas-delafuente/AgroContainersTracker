using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain.Rates;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{
	public class AddRateValidator : AbstractValidator<AddRateRequest>
    {
        private const string NAME_EXISTS_MESSAGE = "Ya existe una tarifa con este nombre.";
        

        public AddRateValidator(IRateService rateService)
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MaximumLength(50).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE)
                .MustAsync(async (name, cancellation) =>
                {
                    return !await rateService.ExistsAsync(name);

                }).WithMessage(NAME_EXISTS_MESSAGE);
                

            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.SecondaryValue)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Description)
                .MaximumLength(300).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE);
        }
    }
}

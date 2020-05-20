using System;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain.Rates;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class RateValidator : AbstractValidator<Rate>
    {
        private const string NAME_EXISTS_MESSAGE = "Ya existe una tarifa con este nombre.";

        public RateValidator(IRateService rateService)
        {
            RuleFor(v => v.Name)
                .MaximumLength(50).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MustAsync(async (rate, name, cancellation) =>
                {
                    Rate currentRate = await rateService.GetByIdAsync(rate.RateId).ConfigureAwait(false);

                    return NameHasChanged(currentRate.Name, name) ? !await rateService.ExistsAsync(name) : true;
                    
                }).WithMessage(NAME_EXISTS_MESSAGE);

            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(0.0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.SecondaryValue)
                .GreaterThanOrEqualTo(0.0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Description)
                .MaximumLength(300).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE);
        }

        private bool NameHasChanged(string currentName, string name)
        {
            return !currentName.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    
}

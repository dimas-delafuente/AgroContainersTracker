using AgroContainerTracker.Application.Features;
using AgroContainerTracker.Domain.Entities;
using FluentValidation;
using MediatR;
using System;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class RateValidator : AbstractValidator<Rate>
    {
        private const string NAME_EXISTS_MESSAGE = "Ya existe una tarifa con este nombre.";

        public RateValidator(IMediator mediator)
        {
            RuleFor(v => v.Name)
                .MaximumLength(50).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MustAsync(async (rate, name, cancellation) =>
                {
                    Rate currentRate = await mediator.Send(new GetRateByIdQuery(rate.RateId)).ConfigureAwait(false);
                    return NameHasChanged(currentRate.Name, name) ? !await mediator.Send(new ExistsRateNameQuery(name)).ConfigureAwait(false) : true;
                    
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

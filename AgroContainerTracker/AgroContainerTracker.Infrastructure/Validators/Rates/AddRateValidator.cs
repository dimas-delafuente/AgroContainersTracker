using AgroContainerTracker.Application.Features;
using FluentValidation;
using MediatR;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class AddRateValidator : AbstractValidator<CreateRateCommand>
    {
        private const string NAME_EXISTS_MESSAGE = "Ya existe una tarifa con este nombre.";
        
        public AddRateValidator(IMediator mediator)
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MaximumLength(50).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE)
                .MustAsync(async (name, cancellation) =>
                {
                    return !await mediator.Send(new ExistsRateNameQuery(name));

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

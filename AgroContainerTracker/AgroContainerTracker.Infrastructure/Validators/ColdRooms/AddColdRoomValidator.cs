using AgroContainerTracker.Application.Features;
using FluentValidation;
using MediatR;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class AddStorageValidator : AbstractValidator<CreateStorageCommand>
    {
        private const string NUMBER_EXISTS_MESSAGE = "Ya existe una cámara con este número.";

        public AddStorageValidator(IMediator mediator)
        {
            RuleFor(v => v.Number)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE)
                .MustAsync(async (number, cancellation) =>
                {
                    return !await mediator.Send(new ExistsStorageNumberQuery(number), cancellation);

                }).WithMessage(NUMBER_EXISTS_MESSAGE);


            RuleFor(v => v.Description)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MaximumLength(100).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE);

            RuleFor(v => v.Surface)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Capacity)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);
        }
    }
}

using AgroContainerTracker.Application.Features;
using AgroContainerTracker.Domain;
using FluentValidation;
using MediatR;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class StorageValidator : AbstractValidator<Storage>
    {
        private const string ID_EXISTS_MESSAGE = "Ya existe una cámara con este número.";

        public StorageValidator(IMediator mediator)
        {
            RuleFor(v => v.Number)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE)
                .MustAsync(async (currentStorage, number, cancellation) =>
                {
                    Storage storage = await mediator.Send(new GetStorageQuery(currentStorage.Id)).ConfigureAwait(false);
                    return storage != null && NumberHasChanged(storage.Number, number) ? !await mediator.Send(new ExistsStorageNumberQuery(currentStorage.Number)) : true;

                }).WithMessage(ID_EXISTS_MESSAGE);

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MaximumLength(100).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE);

            RuleFor(v => v.Surface)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Capacity)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Temperature);
        }

        private bool NumberHasChanged(int currentNumber, int number)
        {
            return !currentNumber.Equals(number);
        }
    }


}
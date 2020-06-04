using System;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class ColdRoomValidator : AbstractValidator<ColdRoom>
    {
        private const string ID_EXISTS_MESSAGE = "Ya existe una cámara con este número.";

        public ColdRoomValidator(IColdRoomService coldRoomService)
        {
            RuleFor(v => v.Number)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE)
                .MustAsync(async (currentColdRoom, number, cancellation) =>
                {
                    ColdRoom coldRoom = await coldRoomService.GetByIdAsync(currentColdRoom.ColdRoomId).ConfigureAwait(false);

                    return coldRoom != null && NumberHasChanged(coldRoom.Number, number) ? !await coldRoomService.ExistsAsync(number) : true;

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
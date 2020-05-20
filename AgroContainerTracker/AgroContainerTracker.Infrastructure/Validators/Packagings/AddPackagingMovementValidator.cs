using System;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain.Packagings;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators.Packagings
{

    public class AddPackagingMovementValidator : AbstractValidator<AddPackagingMovementRequest>
    {
        private const string INVALID_AMOUNT_MESSAGE = "No es posible registrar una salida con una cantidad mayor a la existente.";


        public AddPackagingMovementValidator(IPackagingService packagingService)
        {
            RuleFor(v => v.PackagingId)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(v => v.CustomerId)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(v => v.Operation)
                .IsInEnum();

            RuleFor(v => v.Amount)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE)
                .MustAsync(async (pkgMovement, amount, cancellation) =>
                {
                    if (pkgMovement.PackagingId is null)
                        return false;

                    Packaging packaging = await packagingService.FindAsync(pkgMovement.PackagingId).ConfigureAwait(false);
                    return IsValidAmount(packaging.Total, pkgMovement);

                }).WithMessage(INVALID_AMOUNT_MESSAGE);

            RuleFor(v => v.Created)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE); ;
        }

        private bool IsValidAmount(int currentTotal, AddPackagingMovementRequest packagingMovement)
        {
            return packagingMovement.Operation.Equals(Operation.Substract) ?
                (currentTotal - packagingMovement.Amount) >= 0 :
                packagingMovement.Amount >= 0;
        }
    }
}

using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain.Packagings;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{

    public class AddPackagingValidator : AbstractValidator<AddPackagingRequest>
    {
        private const string NAME_EXISTS_MESSAGE = "Ya existe un envase con este nombre.";


        public AddPackagingValidator(IPackagingService packagingService)
        {
            RuleFor(v => v.Code)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MaximumLength(8).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE)
                .MustAsync(async (code, cancellation) =>
                {
                    return !await packagingService.ExistsAsync(code);

                }).WithMessage(NAME_EXISTS_MESSAGE);


            RuleFor(v => v.Material)
                .IsInEnum();

            RuleFor(v => v.Weight)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Description)
                .MaximumLength(100).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE);

            RuleFor(v => v.CustomerId)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(v => v.Type)
                .IsInEnum();

            RuleFor(v => v.Total)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);
        }
    }
}

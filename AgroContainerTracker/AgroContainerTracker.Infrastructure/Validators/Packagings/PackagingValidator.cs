using AgroContainerTracker.Application.Features;
using AgroContainerTracker.Domain.Entities;
using FluentValidation;
using MediatR;
using System;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class PackagingValidator : AbstractValidator<UpdatePackagingCommand>
    {
        private const string NAME_EXISTS_MESSAGE = "Ya existe un envase con este nombre.";


        public PackagingValidator(IMediator mediator)
        {
            RuleFor(v => v.Code)
                .NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE)
                .MaximumLength(8).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE)
                .MustAsync(async (packaging, code, cancellation) =>
                {
                    Packaging currentPackaging = await mediator.Send(new GetPackagingByIdQuery(packaging.PackagingId)).ConfigureAwait(false);
                    return CodeHasChanged(currentPackaging.Code, code) ? !await mediator.Send(new ExistsPackagingCodeQuery(code)).ConfigureAwait(false) : true;

                }).WithMessage(NAME_EXISTS_MESSAGE);


            RuleFor(v => v.Material)
                .IsInEnum();

            RuleFor(v => v.Weight)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);

            RuleFor(v => v.Description)
                .MaximumLength(100).WithMessage(ValidationMessages.MAX_LENGTH_MESSAGE);

            RuleFor(v => v.CustomerId)
                .NotNull().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(v => v.Type)
                .IsInEnum();

            RuleFor(v => v.Total)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MIN_VALUE_MESSAGE);
        }

        private bool CodeHasChanged(string currentCode, string code)
        {
            return !currentCode.Equals(code, StringComparison.InvariantCultureIgnoreCase);
        }
    }

}

using System.Linq;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Domain.Packagings;
using AgroContainerTracker.Domain.ProductManagement;
using FluentValidation;

namespace AgroContainerTracker.Infrastructure.Validators
{
    public class ProductWeighingValidator : AbstractValidator<AddProductWeighingRequest>
    {
        private const string DATE_OUTSIDE_CAMPAING_MESSAGE = "La fecha introducida no corresponde a la campaña asignada.";
        private const string POSITIVE_VALUE_MESSAGE = "Debe ser un valor positivo mayor que 0.";
        private const string QUANTITY_GREATER_THAN_ZERO_MESSAGE = "La cantidad de envases no puede ser 0.";
        private const string ONLY_PALOTS_MESSAGE = "Los palots no pueden ser mezclados con otros tipos de envases.";
        private const string PALETS_MUST_HAVE_BOXES_MESSAGE = "Los palets no pueden añadirse sin cajas.";

        public ProductWeighingValidator()
        {

            RuleFor(x => x.CampaingId).NotEmpty();
            RuleFor(x => x.ProductEntryNumber).NotEmpty();

            RuleFor(x => x.SellerId).NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(x => x.BuyerId).NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(x => x.FruitId).NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(x => x.ColdRoomId).NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(x => x.RateId).NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);

            RuleFor(x => x.GrossWeight).GreaterThan(0).WithMessage(POSITIVE_VALUE_MESSAGE);
            RuleFor(x => x.TareWeight).GreaterThan(0).WithMessage(POSITIVE_VALUE_MESSAGE);
            RuleFor(x => x.NetWeight).GreaterThan(0).WithMessage(POSITIVE_VALUE_MESSAGE);

            RuleFor(x => x.ProductRecords)
                .NotNull().NotEmpty().WithMessage(ValidationMessages.REQUIRED_FIELD_MESSAGE);
            RuleFor(x => x.ProductRecords)
                .Must(productRecords => productRecords.All(x => x.Quantity > 0)).WithMessage(QUANTITY_GREATER_THAN_ZERO_MESSAGE);
            RuleFor(x => x.ProductRecords)
                .Must(productRecords => productRecords.Exists(x => x.Packaging.Type.Equals(PackagingType.Palot)) ? productRecords.All(x => x.Packaging.Type.Equals(PackagingType.Palot)) : true)
                .WithMessage(ONLY_PALOTS_MESSAGE);
            RuleFor(x => x.ProductRecords)
                .Must(productRecords => productRecords.Exists(x => x.Packaging.Type.Equals(PackagingType.Palet)) ? productRecords.Exists(x => x.Packaging.Type.Equals(PackagingType.Caja)) : true)
                .WithMessage(PALETS_MUST_HAVE_BOXES_MESSAGE);
        }
    }
}

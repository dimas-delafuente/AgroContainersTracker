using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Application.Features
{
    public class UpdatePackagingCommand : IRequest<bool>
    {
        public int CustomerId { get; set; }

        public int PackagingId { get; set; }

        [Required]
        [MaxLength(8)]
        public string Code { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingMaterial))]
        public PackagingMaterial Material { get; set; }

        [Required]
        public double Weight { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public string Color { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingType))]
        public PackagingType Type { get; set; }

        public bool Active { get; set; }

        [Required]
        public int Total { get; set; }
    }

    public static class UpdatePackagingCommandExtensions
    {
        public static Packaging ToDomain(this UpdatePackagingCommand command)
            => new Packaging
            {
                Active = command.Active,
                Total = command.Total,
                Code = command.Code,
                Material = command.Material,
                Weight = command.Weight,
                Description = command.Description,
                Color = command.Color,
                Type = command.Type,
                CustomerId = command.CustomerId,
                PackagingId = command.PackagingId
            };

        public static UpdatePackagingCommand ToCommand(this Packaging rate) => rate == null ? null
            : new UpdatePackagingCommand
            {
                CustomerId = rate.CustomerId,
                Active = rate.Active,
                Code = rate.Code,
                Color = rate.Color,
                Description = rate.Description,
                Material = rate.Material,
                PackagingId = rate.PackagingId,
                Total = rate.Total,
                Type = rate.Type,
                Weight = rate.Weight
            };
    }
}

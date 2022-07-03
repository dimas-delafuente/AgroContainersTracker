using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Application.Features
{
    public class CreatePackagingCommand : IRequest<Packaging>
    {
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

        [Required]
        public int CustomerId { get; set; }

        public string Color { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingType))]
        public PackagingType Type { get; set; }

        public bool Active { get; set; }

        [Required]
        public int Total { get; set; }
    }

    public static class CreatePackagingCommandExtensions
    {
        public static Packaging ToDomain(this CreatePackagingCommand command)
            => new Packaging
            {
                Code = command.Code,
                Material = command.Material,
                Weight = command.Weight,
                Description = command.Description,
                CustomerId = command.CustomerId,
                Color = command.Color,
                Type = command.Type,
                Active = command.Active,
                Total = command.Total
            };
    }
}

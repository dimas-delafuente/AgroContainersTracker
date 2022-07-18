using AgroContainerTracker.Domain;
using MediatR;
using System;

namespace AgroContainerTracker.Application.Features
{
    public class CreatePackagingMovementCommand : IRequest<PackagingMovement>
    {
        public int? PackagingId { get; set; }

        public PackagingMovementOperation Operation { get; set; }

        public int Amount { get; set; }

        public DateTime Created { get; set; }

        public int? CustomerId { get; set; }
    }

    public static class CreatePackagingMovementCommandExtensions
    {
        public static PackagingMovement ToDomain(this CreatePackagingMovementCommand command)
            => new PackagingMovement
            {
                PackagingId = command.PackagingId.Value,
                Operation = command.Operation,
                Quantity = command.Amount,
                Created = command.Created,
                Customer = new Customer
                {
                    Id = command.CustomerId.Value
                }
            };
    }
}

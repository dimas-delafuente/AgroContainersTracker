using AgroContainerTracker.Domain.Entities;
using MediatR;
using System;

namespace AgroContainerTracker.Application.Features
{
    public class CreatePackagingMovementCommand : IRequest<PackagingMovement>
    {
        public int? PackagingId { get; set; }

        public PackagMovementOperation Operation { get; set; }

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
                Amount = command.Amount,
                Created = command.Created,
                Customer = new Customer
                {
                    CustomerId = command.CustomerId.Value
                }
            };
    }
}

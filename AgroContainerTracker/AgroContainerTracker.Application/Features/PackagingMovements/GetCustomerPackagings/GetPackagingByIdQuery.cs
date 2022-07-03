using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Application.Features
{
    public class GetCustomerPackagingsQuery : IRequest<IEnumerable<PackagingMovement>>
    {
        public int CustomerId { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetCustomerPackagingsQuery(int customerId, DateTime initDate, DateTime endDate)
        {
            Ensure.Positive(customerId, nameof(customerId));

            if (initDate > endDate)
            {
                throw new ArgumentOutOfRangeException(nameof(initDate), nameof(endDate));
            }

            CustomerId = customerId;
            InitDate = initDate;
            EndDate = endDate;
        }
    }
}

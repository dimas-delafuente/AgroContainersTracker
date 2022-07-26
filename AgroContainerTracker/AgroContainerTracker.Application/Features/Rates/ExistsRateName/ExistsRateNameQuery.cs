﻿using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class ExistsRateNameQuery : IRequest<bool>
    {
        public string RateName { get; set; }
        public ExistsRateNameQuery(string rateName)
        {
            Ensure.NotNullOrEmpty(rateName, nameof(rateName));
            RateName = rateName;
        }
    }
}
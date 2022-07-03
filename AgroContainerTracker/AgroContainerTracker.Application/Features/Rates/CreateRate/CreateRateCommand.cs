﻿using AgroContainerTracker.Domain.Entities;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class CreateRateCommand : IRequest<Rate>
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double SecondaryValue { get; set; }
        public string Description { get; set; }
    }

    public static class CreateRateCommandExtensions
    {
        public static Rate ToDomain(this CreateRateCommand command) =>
            new Rate
            {
                Name = command.Name,
                Value = command.Value,
                SecondaryValue = command.SecondaryValue,
                Description = command.Description
            };
    }
}

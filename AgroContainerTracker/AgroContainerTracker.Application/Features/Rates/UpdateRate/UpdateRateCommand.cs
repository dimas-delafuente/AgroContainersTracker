using AgroContainerTracker.Domain.Entities;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class UpdateRateCommand : IRequest<bool>
    {
        public int RateId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double SecondaryValue { get; set; }
        public string Description { get; set; }
    }

    public static class UpdateRateCommandExtensions
    {
        public static Rate ToDomain(this UpdateRateCommand command) =>
            new Rate
            {
                RateId = command.RateId,
                Name = command.Name,
                Value = command.Value,
                SecondaryValue = command.SecondaryValue,
                Description = command.Description
            };

        public static UpdateRateCommand ToCommand(this Rate rate) => rate == null ? null 
            : new UpdateRateCommand
            {
                RateId = rate.RateId,
                Description = rate.Description,
                Name = rate.Name,
                SecondaryValue = rate.SecondaryValue,
                Value = rate.Value
            };
    }
}

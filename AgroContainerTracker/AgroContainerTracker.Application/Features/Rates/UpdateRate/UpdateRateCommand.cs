using AgroContainerTracker.Domain;
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
                Id = command.RateId,
                Name = command.Name,
                MainPrice = command.Value,
                SecondaryPrice = command.SecondaryValue,
                Description = command.Description
            };

        public static UpdateRateCommand ToCommand(this Rate rate) => rate == null ? null 
            : new UpdateRateCommand
            {
                RateId = rate.Id,
                Description = rate.Description,
                Name = rate.Name,
                SecondaryValue = rate.SecondaryPrice,
                Value = rate.MainPrice
            };
    }
}

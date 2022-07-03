using AgroContainerTracker.Domain.Entities;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class UpdateColdRoomCommand : IRequest<bool>
    {
        public int ColdRoomId { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public double Surface { get; set; }
        public double Capacity { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }

    public static class UpdateColdRoomCommandExtensions
    {
        public static ColdRoom ToDomain(this UpdateColdRoomCommand command)
        {
            return new ColdRoom
            {
                ColdRoomId = command.ColdRoomId,
                Number = command.Number,
                Description = command.Description,
                Surface = command.Surface,
                Capacity = command.Capacity,
                Temperature = command.Temperature,
                Humidity = command.Humidity
            };
        }
    }
}

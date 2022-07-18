using AgroContainerTracker.Domain;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class UpdateStorageCommand : IRequest<bool>
    {
        public int StorageId { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public double Surface { get; set; }
        public double Capacity { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }

    public static class UpdateStorageCommandExtensions
    {
        public static Storage ToDomain(this UpdateStorageCommand command)
        {
            return new Storage
            {
                Id = command.StorageId,
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

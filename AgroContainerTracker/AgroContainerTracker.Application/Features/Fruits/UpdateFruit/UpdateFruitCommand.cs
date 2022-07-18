using AgroContainerTracker.Domain;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class UpdateFruitCommand : IRequest<bool>
    {
        public int FruitId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class UpdateFruitCommandExtensions
    {
        public static Fruit ToDomain(this UpdateFruitCommand command) =>
            new Fruit
            {
                FruitId = command.FruitId,
                Code = command.Code,
                Name = command.Name,
                Description = command.Description
            };
    }
}

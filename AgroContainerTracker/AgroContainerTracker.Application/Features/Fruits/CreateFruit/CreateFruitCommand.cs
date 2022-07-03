using AgroContainerTracker.Domain.Entities;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class CreateFruitCommand : IRequest<Fruit>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class CreateFruitCommandExtensions
    {
        public static Fruit ToDomain(this CreateFruitCommand command) =>
            new Fruit
            {
                Code = command.Code,
                Name = command.Name,
                Description = command.Description
            };
    }
}

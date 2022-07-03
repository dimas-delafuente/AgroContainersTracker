using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace AgroContainerTracker.Application.Features
{
    public class GetAllFruitsQuery : IRequest<IEnumerable<Fruit>>
    {
    }
}

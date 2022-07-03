using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace AgroContainerTracker.Application.Features
{
    public class GetAllCountriesQuery : IRequest<IEnumerable<Country>>
    {
    }
}

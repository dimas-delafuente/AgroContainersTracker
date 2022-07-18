using AgroContainerTracker.Domain;
using MediatR;
using System.Collections.Generic;

namespace AgroContainerTracker.Application.Features
{
    public class GetAllCampaignsQuery : IRequest<IEnumerable<Campaign>>
    {
    }
}

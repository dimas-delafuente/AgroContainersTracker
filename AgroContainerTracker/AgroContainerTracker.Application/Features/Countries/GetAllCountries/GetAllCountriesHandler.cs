using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, IEnumerable<Country>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetAllCountriesHandler(ICountryRepository countryRepository)
        {
            Ensure.NotNull(countryRepository, nameof(countryRepository));
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _countryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

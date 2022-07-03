using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class DeleteFruitHandler : FruitsBaseHandler, IRequestHandler<DeleteFruitCommand, bool>
    {
        public DeleteFruitHandler(IFruitRepository fruitRepository) : base(fruitRepository)
        {
        }

        public async Task<bool> Handle(DeleteFruitCommand request, CancellationToken cancellationToken)
        {
            return await _fruitRepository.DeleteAsync(request.FruitId, cancellationToken).ConfigureAwait(false);
        }
    }
}

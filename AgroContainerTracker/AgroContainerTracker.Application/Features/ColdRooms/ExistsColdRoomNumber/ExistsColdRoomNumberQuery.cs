using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class ExistsColdRoomNumberQuery : IRequest<bool>
    {
        public int ColdRoomNumber { get; set; }

        public ExistsColdRoomNumberQuery(int coldRoomNumber)
        {
            Ensure.Positive(coldRoomNumber, nameof(coldRoomNumber));
            ColdRoomNumber = coldRoomNumber;
        }
    }
}

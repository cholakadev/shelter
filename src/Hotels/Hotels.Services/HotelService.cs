using FluentResult;
using Hotels.Core.Requests;
using Hotels.Core.Services;

namespace Hotels.Services
{
    public class HotelService : IHotelService
    {
        public async Task<Result<bool>> CreateHotelAsync(CreateHotelRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

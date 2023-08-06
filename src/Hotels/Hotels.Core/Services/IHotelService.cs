using FluentResult;
using Hotels.Core.Requests;

namespace Hotels.Core.Services
{
    public interface IHotelService
    {
        Task<Result<bool>> CreateHotelAsync(CreateHotelRequest request, CancellationToken cancellationToken);
    }
}

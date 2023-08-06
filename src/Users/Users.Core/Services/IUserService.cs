using FluentResult;
using Users.Core.Requests;

namespace Users.Core.Services
{
    public interface IUserService
    {
        Task<Result<bool>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    }
}

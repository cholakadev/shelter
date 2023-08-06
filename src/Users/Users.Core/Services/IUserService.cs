using Users.Core.Requests;

namespace Users.Core.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}

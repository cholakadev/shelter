using Users.Core.Requests;
using Users.Core.Services;

namespace Users.Services
{
    public class UserService : IUserService
    {
        public Task<bool> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

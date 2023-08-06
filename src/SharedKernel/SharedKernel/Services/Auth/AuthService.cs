using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using SharedKernel.Contracts;

namespace SharedKernel.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpContext? _context;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext;
        }

        public string GetUserId => _context.User.Claims.Single(x => x.Type == AuthConstants.UserIdType).Value;

        public async Task<string> GetUserAccessToken()
            => await _context.GetTokenAsync("token");
    }
}

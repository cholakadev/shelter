using FluentResult;
using Microsoft.AspNetCore.Mvc;
using Users.Core.Requests;
using Users.Core.Services;

namespace Users.Presentation.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
            => await _userService.RegisterAsync(request, cancellationToken).ToActionResultAsync(this);
    }
}

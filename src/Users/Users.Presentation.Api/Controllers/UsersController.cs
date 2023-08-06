using FluentResult;
using Microsoft.AspNetCore.Mvc;
using Users.Core.Requests;
using Users.Core.Services;

namespace Users.Presentation.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>Registers a user.</summary>
        /// <param name="request">The request to register a user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost("register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
            => await _userService.RegisterAsync(request, cancellationToken).ToActionResultAsync(this);
    }
}

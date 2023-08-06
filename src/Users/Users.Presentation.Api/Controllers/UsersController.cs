using Microsoft.AspNetCore.Mvc;
using Users.Core.Requests;

namespace Users.Presentation.Api.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

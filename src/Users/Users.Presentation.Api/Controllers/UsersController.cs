using Microsoft.AspNetCore.Mvc;

namespace Users.Presentation.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IConfiguration _config;
        public UsersController(IConfiguration config)
        {
            _config = config;

            var connStr = _config.GetConnectionString("UsersDbConnectionString");
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

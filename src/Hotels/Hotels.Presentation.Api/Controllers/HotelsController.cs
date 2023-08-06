using FluentResult;
using Hotels.Core.Requests;
using Hotels.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Presentation.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>Registers a user.</summary>
        /// <param name="request">The request to register a user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost("create")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken cancellationToken)
            => await _hotelService.CreateHotelAsync(request, cancellationToken).ToActionResultAsync(this);
    }
}

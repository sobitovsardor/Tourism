using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Api.Dtos.Booking;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBookingDto dto)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized("User identity not found.");
            }
            var userId = int.Parse(claim.Value);
            var id = await _service.CreateAsync(dto, userId);
            return Ok(id);
        }


        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyBookingsAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var bookings = await _service.GetUserBookingsAsync(userId);
            return Ok(bookings);
        }


    }
}

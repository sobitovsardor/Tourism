using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Dtos.Payment;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePaymentDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet("by-booking/{bookingId}")]
        [Authorize]
        public async Task<IActionResult> GetByBookingIdAsync(int bookingId)
        {
            var payment = await _service.GetByBookingIdAsync(bookingId);
            return payment == null ? NotFound() : Ok(payment);
        }

        [HttpPost("{paymentId}/mark-paid")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MarkAsPaidAsync(int paymentId)
        {
            var success = await _service.MarkAsPaidAsync(paymentId);
            return success ? Ok("Paid and confirmed") : NotFound();
        }
    }
}

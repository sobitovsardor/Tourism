using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserPanelController : ControllerBase
    {
        private IBookingService _bookingService;
        private IPaymentService _paymentService;
        private IWishlistService _wishlistService;
        private IReviewService _reviewService;

        public UserPanelController(
            IBookingService bookingService,
            IPaymentService paymentService,
            IWishlistService wishlistService,
            IReviewService reviewService)
        {
            this._bookingService = bookingService;
            this._paymentService = paymentService;
            this._wishlistService = wishlistService;
            this._reviewService = reviewService;
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetMyBookingsAsync()
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("review")]
        public async Task<IActionResult> GetMyReviewsAsync()
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var reviews = await _reviewService.GetByUserIdAsync(userId);
            return Ok(reviews);
        }

        [HttpGet("wishlist")]
        public async Task<IActionResult> GetMyWishlistAsync()
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var wishlist = await _wishlistService.GetUserWishlistAsync(userId);
            return Ok(wishlist);
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetMyPaymentsAsync()
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var payments = await _paymentService.GetMyPaymentsAsync(userId);
            return Ok(payments);
        }
    }
}

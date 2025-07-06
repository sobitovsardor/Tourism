using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Tourism.Api.Dtos.Review;
using Tourism.Api.Interfaces;
using static Microsoft.VisualStudio.Utilities.UnifiedSettings.ExternalSettingOperationResult;

namespace Tourism.Api.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewService _service;

        public ReviewController(IReviewService reviewService)
        {
            _service = reviewService;
        }

        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetByTourIdAsync(int tourId)
            => Ok(await _service.GetByTourIdAsync(tourId));
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReviewDto dto)
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var reviewId = await _service.CreateAsync(userId, dto);
            return Ok(reviewId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var success = await _service.DeleteAsync(id, userId);
            return success ? NoContent() : NotFound();
        }
    }
}

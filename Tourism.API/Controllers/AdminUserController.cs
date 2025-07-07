using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : ControllerBase
    {
        private IAdminUserService _service;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _service = adminUserService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{userId}/role")]
        public async Task<IActionResult> ChangeRoleAsync(int userId, [FromQuery] string newRole)
        {
            var result = await _service.ChangeRoleAsync(userId, newRole);
            return result ? Ok("Role updated") : BadRequest("Invalid user or role");
        }

        [HttpGet("{id}/bookings")]
        public async Task<IActionResult> GetUserBookingsAsync(int id)
        {
            var bookings = await _service.GetUserBookingsAsync(id);
            return Ok(bookings);
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetUserReviewsAsync(int id)
        {
            var reviews = await _service.GetUserReviewsAsync(id);
            return Ok(reviews);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _service.DeleteUserAsync(id);
            return success ? NoContent() : NotFound();
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Dtos.Wishlist;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers
{
    [Route("api/wishlists")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private IWishlistService _service;

        public WishlistController(IWishlistService wishlistService)
        {
            this._service = wishlistService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserWishlistAsync()
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var wishlist = await _service.GetUserWishlistAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToWishlistAsync([FromBody] CreateWishlistDto dto)
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var id = await _service.AddToWishlistAsync(userId, dto);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromWishlistAsync(int id)
        {
            var userId = int.Parse(User.FindFirst("Id")!.Value);
            var result = await _service.RemoveFromWishlistAsync(id, userId);
            return result ? NoContent() : NotFound();
        }

    }
}

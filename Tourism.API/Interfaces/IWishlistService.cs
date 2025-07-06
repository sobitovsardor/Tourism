using Tourism.Api.Dtos.Wishlist;

namespace Tourism.Api.Interfaces;

public interface IWishlistService
{
    public Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(int userId);

    public Task<int> AddToWishlistAsync(int userId, CreateWishlistDto createWishlistDto);

    public Task<bool> RemoveFromWishlistAsync(int id, int userId);
}

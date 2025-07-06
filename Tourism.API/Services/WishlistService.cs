using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.Wishlist;
using Tourism.Api.Interfaces;
using Tourism.Api.Models;

namespace Tourism.Api.Services;

public class WishlistService : IWishlistService
{
    private AppDbContext _repository;

    public WishlistService(AppDbContext appDbContext)
    {
        this._repository = appDbContext;
    }

    public async Task<int> AddToWishlistAsync(int userId, CreateWishlistDto createWishlistDto)
    {
        var exists = await _repository.Wishlists
           .AnyAsync(w => w.UserId == userId && w.TourPackageId == createWishlistDto.TourPackageId);
        
        if (exists) 
            throw new Exception("Bu tur avval wishlistga qo‘shilgan");

        var wishlist = new Wishlist
        {
            UserId = userId,
            TourPackageId = createWishlistDto.TourPackageId,
        };

        _repository.Wishlists.Add(wishlist);
        await _repository.SaveChangesAsync();
        return wishlist.Id;
    }

    public async Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(int userId)
    {
        return await _repository.Wishlists
        .Where(w => w.UserId == userId)
        .Include(w => w.TourPackage)
        .Select(w => new WishlistDto
        {
            Id = w.Id,
            TourPackageId = w.TourPackageId,
            TourTitle = w.TourPackage!.Title,
            Price = w.TourPackage.Price,
            Location = w.TourPackage.Location,
            CreatedAt = w.CreatedAt
        })
        .ToListAsync(); 
    }

    public async Task<bool> RemoveFromWishlistAsync(int id, int userId)
    {
        var wishlist = await _repository.Wishlists
           .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

        if (wishlist == null)
            return false;

        _repository.Wishlists.Remove(wishlist);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<WishlistDto>> GetMyWishlistAsync(int userId)
    {
        return await _repository.Wishlists
            .Include(w => w.TourPackage)
            .Where(w => w.UserId == userId)
            .Select(w => new WishlistDto
            {
                Id = w.Id,
                TourPackageId = w.TourPackageId,
                TourTitle = w.TourPackage!.Title,
                Location = w.TourPackage.Location,
                Price = w.TourPackage.Price,
            })
            .ToListAsync();
    }

}

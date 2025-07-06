using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.Review;
using Tourism.Api.Interfaces;
using Tourism.API.Common.Enums;
using Tourism.API.Models;

namespace Tourism.Api.Services;

public class ReviewService : IReviewService
{
    private AppDbContext _repository;

    public ReviewService(AppDbContext appDbContext)
    {
        _repository = appDbContext;
    }

    public async Task<int> CreateAsync(int userId, CreateReviewDto dto)
    {
        var hasBooked = await _repository.Bookings
             .AnyAsync(b => b.UserId == userId &&
                            b.TourPackageId == dto.TourPackageId &&
                            b.Status == BookingStatus.Confirmed);
        
        if (!hasBooked) 
            throw new Exception("Siz bu turga izoh qoldira olmaysiz (bron yo‘q).");

        var review = new Review
        {
            UserId = userId,
            TourPackageId = dto.TourPackageId,
            Rating = dto.Rating,
            Comment = dto.Comment
        };

        _repository.Reviews.Add(review);
        await _repository.SaveChangesAsync();
        return review.Id;
    }


    public async Task<bool> DeleteAsync(int reviewId, int userId)
    {
        var review = await _repository.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review == null) return false;

        _repository.Reviews.Remove(review);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ReviewDto>> GetByTourIdAsync(int tourPackageId)
    {
        return await _repository.Reviews
            .Where(r => r.TourPackageId == tourPackageId)
            .Include(r => r.User)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                UserName = r.User!.FullName,
                TourPackageId = r.TourPackageId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();
    }
}

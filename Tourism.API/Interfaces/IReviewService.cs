using Tourism.Api.Dtos.Review;

namespace Tourism.Api.Interfaces;

public interface IReviewService
{
    public Task<IEnumerable<ReviewDto>> GetByTourIdAsync(int tourPackageId);

    public Task<int> CreateAsync(int userId, CreateReviewDto dto);

    public Task<bool> DeleteAsync(int reviewId, int userId);
}

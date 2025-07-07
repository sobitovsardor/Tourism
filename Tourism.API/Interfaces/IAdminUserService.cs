using Tourism.Api.Dtos.Admin;

namespace Tourism.Api.Interfaces;

public interface IAdminUserService
{
    Task<List<AdminUserDto>> GetAllUsersAsync();

    Task<bool> ChangeRoleAsync(int userId, string newRole);

    Task<IEnumerable<AdminBookingDto>> GetUserBookingsAsync(int userId);

    Task<IEnumerable<AdminReviewDto>> GetUserReviewsAsync(int userId);

    Task<bool> DeleteUserAsync(int userId);

}

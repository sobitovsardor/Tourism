using Microsoft.EntityFrameworkCore;
using System.Data;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.Admin;
using Tourism.Api.Interfaces;
using Tourism.API.Common.Enums;

namespace Tourism.Api.Services;

public class AdminUserService : IAdminUserService
{
    private AppDbContext _repository;

    public AdminUserService(AppDbContext appDbContext)
    {
        this._repository = appDbContext;
    }

    public async Task<bool> ChangeRoleAsync(int userId, string newRole)
    {
        var user = await _repository.Users.FindAsync(userId);
        if (user == null) return false;

        if (!Enum.TryParse<UserRole>(newRole, true, out var parsedRole))
            return false;


        user.Role = parsedRole;
        _repository.Users.Update(user);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<List<AdminUserDto>> GetAllUsersAsync()
    {
        return await _repository.Users
            .Select(user => new AdminUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<AdminBookingDto>> GetUserBookingsAsync(int userId)
    {
        return await _repository.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.TourPackage)
            .Select(b => new AdminBookingDto
            {
                Id = b.Id,
                TourTitle = b.TourPackage!.Title,
                Location = b.TourPackage.Location,
                Price = b.TourPackage.Price,
                NumberOfPeople = b.NumberOfPeople,
                BookingStatus = b.Status,
                BookingDate = b.BookingDate
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<AdminReviewDto>> GetUserReviewsAsync(int userId)
    {
        return await _repository.Reviews
            .Where(r => r.UserId == userId)
            .Include(r => r.TourPackage) 
            .Select(r => new AdminReviewDto
            {
                Id = r.Id,
                TourTitle = r.TourPackage!.Title,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _repository.Users
            .Include(u => u.Bookings)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return false;

        if (user.Bookings != null)
            _repository.Bookings.RemoveRange(user.Bookings);

        if (user.Reviews != null)
            _repository.Reviews.RemoveRange(user.Reviews);

        _repository.Users.Remove(user);
        await _repository.SaveChangesAsync();

        return true;
    }

}

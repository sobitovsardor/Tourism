using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.Booking;
using Tourism.Api.Interfaces;
using Tourism.API.Common.Enums;
using Tourism.API.Models;

namespace Tourism.Api.Services;

public class BookingService : IBookingService
{
    private AppDbContext _repository;

    public BookingService(AppDbContext appDbContext)
    {
        _repository = appDbContext;
    }

    public async Task<int> CreateAsync(CreateBookingDto dto, int userId)
    {
        var tour = await _repository.TourPackages.FindAsync(dto.TourPackageId);
        if (tour == null) throw new Exception("Tour package not found");

        var booking = new Booking
        {
            UserId = userId,
            TourPackageId = dto.TourPackageId,
            NumberOfPeople = dto.NumberOfPeople,
            Status = BookingStatus.Pending
        };

        _repository.Bookings.Add(booking);
        await _repository.SaveChangesAsync();
        
        return booking.Id;
    }

    public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId)
    {
        return await _repository.Bookings
             .Include(b => b.TourPackage)
             .Where(b => b.UserId == userId)
             .Select(b => new BookingDto
             {
                 Id = b.Id,
                 TourTitle = b.TourPackage!.Title,
                 NumberOfPeople = b.NumberOfPeople,
                 Status = b.Status.ToString(),
                 BookingDate = b.BookingDate
             }).ToListAsync();
    }
}

using Tourism.Api.Dtos.Booking;

namespace Tourism.Api.Interfaces;

public interface IBookingService
{
    Task<int> CreateAsync(CreateBookingDto dto, int userId);
    
    Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId);
}

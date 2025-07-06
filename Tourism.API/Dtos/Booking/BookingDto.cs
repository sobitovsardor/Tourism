using Tourism.API.Common.Enums;

namespace Tourism.Api.Dtos.Booking;

public class BookingDto
{
    public int Id { get; set; }

    public string TourTitle { get; set; } = string.Empty;

    public int NumberOfPeople { get; set; }

    public DateTime BookingDate { get; set; }

    public BookingStatus BookingStatus { get; set; }

    public decimal Price { get; set; }

    public string Location { get; set; } = null!;
}

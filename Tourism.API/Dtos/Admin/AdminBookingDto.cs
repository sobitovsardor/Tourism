using Tourism.API.Common.Enums;

namespace Tourism.Api.Dtos.Admin;

public class AdminBookingDto
{
    public int Id { get; set; }
    public string TourTitle { get; set; } = null!;
    public string Location { get; set; } = null!;
    public decimal Price { get; set; }
    public int NumberOfPeople { get; set; }
    public BookingStatus BookingStatus { get; set; }
    public DateTime BookingDate { get; set; }
}

namespace Tourism.Api.Dtos.Booking;

public class BookingDto
{
    public int Id { get; set; }

    public string TourTitle { get; set; } = string.Empty;

    public int NumberOfPeople { get; set; }

    public string Status { get; set; } = null!;
    
    public DateTime BookingDate { get; set; }
}

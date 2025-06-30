using Tourism.API.Common.Enums;

namespace Tourism.API.Models;

public class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int TourPackageId { get; set; }
    public TourPackage? TourPackage { get; set; }

    public int NumberOfPeople { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public Payment? Payment { get; set; }
}

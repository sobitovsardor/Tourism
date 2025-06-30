using Tourism.API.Common.Enums;

namespace Tourism.API.Models;

public class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }
    public Booking? Booking { get; set; }

    public decimal Amount { get; set; }
    public string StripeSessionId { get; set; } = null!;
    public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

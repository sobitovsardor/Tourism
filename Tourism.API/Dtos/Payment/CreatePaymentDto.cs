namespace Tourism.Api.Dtos.Payment;

public class CreatePaymentDto
{
    public int BookingId { get; set; } = 0;
    public decimal Amount { get; set; } = 0.0m;
}

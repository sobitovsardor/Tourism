using Tourism.Api.Dtos.Payment;

namespace Tourism.Api.Interfaces;

public interface IPaymentService
{
    Task<int> CreateAsync(CreatePaymentDto dto);

    Task<PaymentDto?> GetByBookingIdAsync(int bookingId);

    Task<bool> MarkAsPaidAsync(int paymentId);

}

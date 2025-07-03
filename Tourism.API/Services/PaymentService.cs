using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.Payment;
using Tourism.Api.Interfaces;
using Tourism.API.Common.Enums;
using Tourism.API.Models;

namespace Tourism.Api.Services;

public class PaymentService : IPaymentService
{
    private AppDbContext _repository;

    public PaymentService(AppDbContext appDbContext)
    {
        _repository = appDbContext;
    }

    public async Task<int> CreateAsync(CreatePaymentDto dto)
    {
        var booking = await _repository.Bookings
            .Include(b => b.TourPackage)
            .FirstOrDefaultAsync(b => b.Id == dto.BookingId);

        if (booking == null)
            throw new Exception("Booking topilmadi");

        var exciting = await _repository.Payments
            .FirstOrDefaultAsync(p => p.BookingId == dto.BookingId);
        if (exciting != null)
            throw new Exception("Bu booking uchun to'lov allaqachon mavjud");

        var payment = new Payment
        {
            BookingId = dto.BookingId,
            Amount = dto.Amount,
            Status = PaymentStatus.Unpaid,
            StripeSessionId = Guid.NewGuid().ToString()
        };

        _repository.Payments.Add(payment);
        await _repository.SaveChangesAsync();
        return payment.Id;
    }

    public async Task<PaymentDto?> GetByBookingIdAsync(int bookingId)
    {
        var payment = await _repository.Payments
            .FirstOrDefaultAsync(p => p.BookingId == bookingId);

        if (payment is null) return null;

        return new PaymentDto
        {
            Id = payment.Id,
            BookingId = payment.BookingId,
            Amount = payment.Amount,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        };
    }

    public async Task<bool> MarkAsPaidAsync(int paymentId)
    {
        var payment = await _repository.Payments
            .Include(p => p.Booking)
            .FirstOrDefaultAsync(p => p.Id == paymentId);

        if (payment == null) return false;

        payment.Status = PaymentStatus.Paid;

        if (payment.Booking != null)
        {
            payment.Booking.Status = BookingStatus.Confirmed;
        }

        await _repository.SaveChangesAsync();
        return true;
    }
}

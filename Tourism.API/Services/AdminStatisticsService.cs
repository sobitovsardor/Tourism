using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.Admin;
using Tourism.Api.Dtos.Statistics;
using Tourism.Api.Interfaces;
using Tourism.API.Common.Enums;

namespace Tourism.Api.Services;

public class AdminStatisticsService : IAdminStatisticsService
{
    private AppDbContext _repository;

    public AdminStatisticsService(AppDbContext appDbContext)
    {
        _repository = appDbContext;
    }

    public async Task<StatisticsDto> GetStatisticsAsync()
    {
        var now = DateTime.UtcNow;

        var bookingsByMonth = await _repository.Bookings
           .Where(b => b.BookingDate >= now.AddMonths(-5))
           .GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
           .Select(g => new MonthlyBookingStat
           {
               Month = $"{g.Key.Month:00}/{g.Key.Year}",
               BookingCount = g.Count()
           }).ToListAsync();

        var revenueByMonth = await _repository.Payments
            .Where(p => p.CreatedAt >= now.AddMonths(-5) && p.Status == PaymentStatus.Paid)
            .GroupBy(p => new { p.CreatedAt.Year, p.CreatedAt.Month })
            .Select(g => new MonthlyRevenueStat
            {
                Month = $"{g.Key.Month:00}/{g.Key.Year}",
                Revenue = g.Sum(x => x.Amount)
            }).ToListAsync();

        var topTours = await _repository.Bookings
            .GroupBy(b => b.TourPackage!.Title)
            .Select(g => new TopTourDto
            {
                Title = g.Key,
                BookingCount = g.Count()
            })
            .OrderByDescending(t => t.BookingCount)
            .Take(5)
            .ToListAsync();

        return new StatisticsDto
        {
            TotalUsers = await _repository.Users.CountAsync(),
            TotalBookings = await _repository.Bookings.CountAsync(),
            TotalTours = await _repository.TourPackages.CountAsync(),
            BookingsByMonth = bookingsByMonth,
            RevenueByMonth = revenueByMonth,
            TopTours = topTours
        };
    }
}

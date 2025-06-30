using Microsoft.EntityFrameworkCore;
using Tourism.API.Models;

namespace Tourism.Api.Common.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }
    public DbSet<User> Users => Set<User>();
    public DbSet<TourPackage> TourPackages => Set<TourPackage>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Review> Reviews => Set<Review>();
}

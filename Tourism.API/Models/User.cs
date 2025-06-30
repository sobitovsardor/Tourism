using Tourism.API.Common.Enums;

namespace Tourism.API.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Salt { get; set; } = String.Empty;
    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Booking>? Bookings { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}

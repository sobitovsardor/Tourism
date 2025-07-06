using Tourism.API.Models;

namespace Tourism.Api.Models;

public class Wishlist
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int TourPackageId { get; set; }
    public TourPackage? TourPackage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

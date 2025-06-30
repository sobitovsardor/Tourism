namespace Tourism.API.Models;

public class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int TourPackageId { get; set; }
    public TourPackage? TourPackage { get; set; }

    public int Rating { get; set; } // 1–5
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

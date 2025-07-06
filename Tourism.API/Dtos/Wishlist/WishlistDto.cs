namespace Tourism.Api.Dtos.Wishlist;

public class WishlistDto
{
    public int Id { get; set; }
    public int TourPackageId { get; set; }
    public string TourTitle { get; set; } = null!;
    public decimal Price { get; set; }
    public string Location { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}


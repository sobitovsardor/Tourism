namespace Tourism.Api.Dtos.Review;

public class ReviewDto
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int TourPackageId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}

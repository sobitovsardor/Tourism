namespace Tourism.Api.Dtos.Review;

public class CreateReviewDto
{
    public int Rating { get; set; }

    public int TourPackageId { get; set; }

    public string Comment { get; set; } = string.Empty;
}

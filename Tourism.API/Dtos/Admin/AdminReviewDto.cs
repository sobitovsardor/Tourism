namespace Tourism.Api.Dtos.Admin;

public class AdminReviewDto
{
    public int Id { get; set; }
    public string TourTitle { get; set; } = null!;
    public int Rating { get; set; } // 1–5
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

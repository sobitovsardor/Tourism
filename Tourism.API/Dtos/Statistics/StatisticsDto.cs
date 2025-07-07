namespace Tourism.Api.Dtos.Statistics;

public class StatisticsDto
{
    public int TotalUsers { get; set; }
    public int TotalBookings { get; set; }
    public int TotalTours { get; set; }
    public List<MonthlyBookingStat> BookingsByMonth { get; set; } = new();
    public List<MonthlyRevenueStat> RevenueByMonth { get; set; } = new();
    public List<TopTourDto> TopTours { get; set; } = new();
}

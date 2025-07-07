using Tourism.Api.Dtos.Statistics;

namespace Tourism.Api.Interfaces;

public interface IAdminStatisticsService
{
    Task<StatisticsDto> GetStatisticsAsync();

}

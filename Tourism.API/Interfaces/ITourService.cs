using Tourism.Api.Dtos.TourPackage;

namespace Tourism.Api.Interfaces;

public interface ITourService
{
    public Task<IEnumerable<TourDto>> GetAllAsync();

    public Task<TourDto?> GetByIdAsync(int id);

    public Task<int> CreateAsync(CreateTourDto dto);

    public Task<bool> UpdateAsync(int id, UpdateTourDto dto);

    public Task<bool> DeleteAsync(int id);
}

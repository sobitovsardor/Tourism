using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Dtos.TourPackage;
using Tourism.Api.Interfaces;
using Tourism.API.Models;

namespace Tourism.Api.Services;

public class TourService : ITourService
{
    private AppDbContext _repository;

    public TourService(AppDbContext appDbContext)
    {
        this._repository = appDbContext;
    }

    public async Task<int> CreateAsync(CreateTourDto dto)
    {
        var tour = new TourPackage
        {
            Title = dto.Title,
            Location = dto.Location,
            Price = dto.Price,
            DurationDays = dto.DurationDays,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            TourType = dto.TourType
        };

        _repository.TourPackages.Add(tour);
        await _repository.SaveChangesAsync();
        return tour.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tour = await _repository.TourPackages.FindAsync(id);
        if (tour == null) return false;

        _repository.TourPackages.Remove(tour);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TourDto>> GetAllAsync()
    {
        return await _repository.TourPackages
            .Select(t => new TourDto
            {
                Id = t.Id,
                Title = t.Title,
                Location = t.Location,
                Price = t.Price,
                DurationDays = t.DurationDays,
                ImageUrl = t.ImageUrl,
                TourType = t.TourType
            })
            .ToListAsync();
    }

    public async Task<TourDto?> GetByIdAsync(int id)
    {
        var tour = await _repository.TourPackages.FindAsync(id);
        if (tour == null) return null;

        return new TourDto
        {
            Id = tour.Id,
            Title = tour.Title,
            Location = tour.Location,
            Price = tour.Price,
            DurationDays = tour.DurationDays,
            ImageUrl = tour.ImageUrl,
            TourType = tour.TourType
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateTourDto dto)
    {
        var tour = await _repository.TourPackages.FindAsync(id);
        if (tour == null) return false;

        tour.Title = dto.Title;
        tour.Location = dto.Location;
        tour.Price = dto.Price;
        tour.DurationDays = dto.DurationDays;
        tour.Description = dto.Description;
        tour.ImageUrl = dto.ImageUrl;
        tour.TourType = dto.TourType;

        await _repository.SaveChangesAsync();
        return true;
    }
}

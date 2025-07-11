﻿using Tourism.API.Common.Enums;

namespace Tourism.Api.Dtos.TourPackage;

public class TourDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Location { get; set; } = null!;
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public string ImageUrl { get; set; } = null!;
    public TourType TourType { get; set; }
}

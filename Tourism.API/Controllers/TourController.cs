using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Dtos.TourPackage;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers;

[Route("api/tours")]
[ApiController]
public class TourController : ControllerBase
{
    private ITourService _tourService;

    public TourController(ITourService tourService)
    {
        _tourService = tourService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _tourService.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _tourService.GetByIdAsync(id));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTourDto dto)
        => Ok(await _tourService.CreateAsync(dto));

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTourDto dto)
        => Ok(await _tourService.UpdateAsync(id, dto));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(int id)
        => Ok(await _tourService.DeleteAsync(id));

}

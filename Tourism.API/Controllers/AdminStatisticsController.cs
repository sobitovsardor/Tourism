using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers
{
    [ApiController]
    [Route("api/admin/statistics")]
    [Authorize(Roles = "Admin")]
    public class AdminStatisticsController : ControllerBase
    {
        private readonly IAdminStatisticsService _service;

        public AdminStatisticsController(IAdminStatisticsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var stats = await _service.GetStatisticsAsync();
            return Ok(stats);
        }
    }
}

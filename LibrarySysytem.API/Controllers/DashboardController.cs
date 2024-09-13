using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDashboardInfo()
        {
            var result = await _dashboardService.GetDashboardInfo();
            return Ok(result);
        }
    }
}

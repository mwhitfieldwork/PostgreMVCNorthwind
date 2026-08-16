using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NWCodeFirstMVC.Domain.Contracts;

using NWCodeFirstMVC.Domain.Dto;
using NWCodeFirstMVC.Infrastructure.Services;

namespace NWCodeFirstMVC.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        // GET: DashboardController
        private readonly IDashboardService dashboardService;
        private readonly IMapper mapper;

        public DashboardController(IDashboardService dashboardService, IMapper mapper)
        {
            this.dashboardService = dashboardService;
            this.mapper = mapper;
        }
        [HttpGet("totals")]
        public async Task<IActionResult> GetTopCardValues()
        {
            var totals = await dashboardService.GetAllTopCardTotals();
            return Ok(totals);

        }

        [HttpGet("salestotals")]
        public async Task<IActionResult> GetSalesTotalsValues(
    [FromQuery] DateTime beginningDate,
    [FromQuery] DateTime endingDate)
        {
            var totals = await dashboardService.GetAllSalesTotals(beginningDate, endingDate);
            return Ok(totals);
        }

        // GET: CategoryController
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var sales = await dashboardService.GetAllAsync();
            return Ok(sales);
        }
    }
}

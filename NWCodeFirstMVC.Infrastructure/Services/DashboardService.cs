using Microsoft.CodeAnalysis.CSharp.Syntax;
using NWCodeFirstMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NWCodeFirstMVC.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.Domain.Contracts;


namespace NWCodeFirstMVC.Infrastructure.Services
{
    public class DashboardService : GenericService<SalesByCategory>, IDashboardService
    {
        private readonly PgNwContext _dc;
        public DashboardService(PgNwContext dc) : base(dc)
        {
            this._dc = dc;
        }


        public async Task<List<TopCardTotalDTO>> GetAllTopCardTotals()
        {
            try
            {
                var cardTotals = await _dc.OrderDetails
                    .OrderByDescending(o => o.UnitPrice)
                    .Take(4)
                    .Select(o => new TopCardTotalDTO
                    {
                        OrderID = o.OrderId,
                        UnitPrice = o.UnitPrice
                    })
                    .ToListAsync();

                return cardTotals;
            }
            catch (Exception ex)
            {
                // Log the error however your app logs (Serilog, ILogger, etc.)
                Console.WriteLine($"DashboardService.GetAllTopCardTotals ERROR: {ex.Message}");

                return new List<TopCardTotalDTO>(); // prevents Swagger from blowing up
            }
        }


    }
}

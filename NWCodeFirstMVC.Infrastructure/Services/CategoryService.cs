using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.Domain.Dto;
using NWCodeFirstMVC.Domain.PocoModels;
using System.Data;
using NWCodeFirstMVC.Domain.Contracts;
using DomainCategory = NWCodeFirstMVC.Domain.PocoModels.Category;
using PgCategory = NWCodeFirstMVC.Infrastructure.PgModels.Category;
using AutoMapper;




namespace NWCodeFirstMVC.Infrastructure.Services
{
    public class CategoryService:GenericService<Category>, ICategoryService
    {
        private readonly PgNwContext _dc;
        private readonly IMapper _mapper;
        public CategoryService(PgNwContext dc, IMapper mapper) : base(dc)
        {
            this._dc = dc;
            _mapper = mapper;

        }

        public override async Task<List<DomainCategory>> GetAllAsync()
        {
            var pgCategories = await _dc.Categories.ToListAsync(); // PgModels.Category
            return _mapper.Map<List<DomainCategory>>(pgCategories);
        }



        public async Task<List<SalesByCategoryDTO>> GetSalesByCategory(string categoryName, string orderYear)
        {
            var salesData = new List<SalesByCategoryDTO>();

            try
            {
                using var connection = _dc.Database.GetDbConnection();
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                using var command = connection.CreateCommand();
                command.CommandText = "SalesByCategory";
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@CategoryName", categoryName),
                    new SqlParameter("@OrdYear", orderYear)
                });

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    salesData.Add(new SalesByCategoryDTO
                    {
                        ProductName = reader["ProductName"].ToString(),
                        TotalPurchase = reader.GetDecimal(reader.GetOrdinal("TotalPurchase")).ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving sales data", ex);
            }

            return salesData;
        }


        public async Task<List<CustomerOrderDTO>> GetCustomerOrders(string customerId)
        {
            try
            {
                var customerOrders = await _dc.OrderDetails
                    .Join(_dc.Products,
                        o => o.ProductId,
                        p => p.ProductId,
                        (o, p) => new { o, p })
                    .Join(_dc.Orders,
                        temp => temp.o.OrderId,
                        od => od.OrderId,
                        (temp, od) => new { temp.o, temp.p, od })
                    .Join(_dc.Customers,
                        temp => temp.od.CustomerId,
                        c => c.CustomerId,
                        (temp, c) => new { temp.o, temp.p, temp.od, c })
                    .Join(_dc.Categories,
                        temp => temp.p.CategoryId,
                        ca => ca.CategoryId,
                        (temp, ca) => new CustomerOrderDTO
                        {
                            CustomerID = temp.c.CustomerId,
                            ProductName = temp.p.ProductName,
                            ProductID = temp.p.ProductId,
                            CategoryName = ca.CategoryName,
                            Quantity = temp.o.Quantity,
                            UnitPrice = temp.o.UnitPrice,
                            OrderDate = temp.od.OrderDate
                        })
                    .Where(result => result.CustomerID == customerId) // Filter by CustomerID
                    .ToListAsync();

                return customerOrders;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer orders", ex);
            }
        }

        public async Task<List<DistinctCustomerDTO>> GetTopCustomersAsync()
        {
            try
            {
                var customers = await _dc.Customers
                    .GroupBy(c => new { c.CustomerId, c.ContactName }) // Group by CustomerID and ContactName
                    .Select(group => new DistinctCustomerDTO
                    {
                        CustomerID = group.Key.CustomerId,
                        ContactName = group.Key.ContactName
                    })
                    .Take(15) // Fetch the top 15 records
                    .ToListAsync();

                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer data", ex);
            }
        }
    }

}

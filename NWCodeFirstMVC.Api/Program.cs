using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Infrastructure.Services;
using NWCodeFirstMVC.Domain.Models;
using NwCodeFirstMVC.Data;
using NWCodeFirstMVC.Domain;
using NLog;
using NuGet.Protocol.Plugins;
using NWCodeFirstMVC.Api.Configurations;
using AutoMapper;
using NWCodeFirstMVC.Infrastructure;
using NWCodeFirstMVC.Api.Converters;
using NWCodeFirstMVC.Infrastructure.Mapping;
using NWCodeFirstMVC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/*builder.Services.AddDbContext<northwindContext>(opt =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("Default");
    opt.UseSqlServer(connectionString);
});*/

builder.Services.AddDbContext<PgNwContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        b=> b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IOrderHistory, OrderHistoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericService<>));
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

NLog.LogManager.Setup().LoadConfiguration(builder => {
    builder.ForLogger().FilterMinLevel(NLog.LogLevel.Info).WriteToConsole();
    builder.ForLogger().FilterMinLevel(NLog.LogLevel.Debug).WriteToFile(fileName: "file.txt");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

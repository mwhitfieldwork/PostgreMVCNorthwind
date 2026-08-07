using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Infrastructure.Services;
using NWCodeFirstMVC.Domain.Models;
using NwCodeFirstMVC.Data;
using NWCodeFirstMVC.Domain;
using NLog;
using NWCodeFirstMVC.Api.Configurations;
using AutoMapper;
using NWCodeFirstMVC.Infrastructure;
using NWCodeFirstMVC.Api.Converters;
using NWCodeFirstMVC.Infrastructure.Mapping;
using NWCodeFirstMVC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//if (builder.Environment.IsProduction())//for  prod
//{//for  prod
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
//}//for  prod

builder.Services.AddDbContext<PgNwContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IOrderHistory, OrderHistoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericService<>));
builder.Services.AddScoped<ProductRepository>();


builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


NLog.LogManager.Setup().LoadConfiguration(builder =>
{
    builder.ForLogger().FilterMinLevel(NLog.LogLevel.Info).WriteToConsole();
    builder.ForLogger().FilterMinLevel(NLog.LogLevel.Debug).WriteToFile(fileName: "file.txt");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseStaticFiles();


// Minimal hosting model requires MapControllers(), NOT UseEndpoints()
app.MapControllers();

app.Run();

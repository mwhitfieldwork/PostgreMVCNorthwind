using AutoMapper;
using NWCodeFirstMVC.Domain.Dto;
using NWCodeFirstMVC.Domain.PocoModels;
using DomainCategory = NWCodeFirstMVC.Domain.PocoModels.Category;
using PgCategory = NWCodeFirstMVC.Infrastructure.PgModels.Category;


namespace NWCodeFirstMVC.Api.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<SalesByCategory, GetSalesDto>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<PgCategory, DomainCategory>().ReverseMap();


        }
    }
}

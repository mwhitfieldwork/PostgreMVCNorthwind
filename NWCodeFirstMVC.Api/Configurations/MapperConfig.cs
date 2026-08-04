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
            CreateMap<ProductModel, ProductDto>().ReverseMap();
            CreateMap<ProductModel, GetProductDto>().ReverseMap();
            CreateMap<ProductModel, UpdateProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<SalesByCategory, GetSalesDto>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<PgCategory, DomainCategory>().ReverseMap();


        }
    }
}

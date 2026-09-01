using AutoMapper;
using NWCodeFirstMVC.Domain.PocoModels;
using NWCodeFirstMVC.Infrastructure.PgModels;

namespace NWCodeFirstMVC.Infrastructure.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => (int)src.ProductId))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId.HasValue ? (int?)src.SupplierId : null))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.HasValue ? (int?)src.CategoryId : null))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.HasValue ? (decimal?)src.UnitPrice : null))
                .ForMember(dest => dest.Discontinued, opt => opt.MapFrom(src => src.Discontinued == 1))
                ;

            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => (short)src.ProductId))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId.HasValue ? (short?)src.SupplierId : null))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.HasValue ? (short?)src.CategoryId : null))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.HasValue ? (float?)src.UnitPrice : null))
                .ForMember(dest => dest.Discontinued, opt => opt.MapFrom(src => src.Discontinued ? 1 : 0))
                ;
        }
    }
}

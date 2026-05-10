using AutoMapper;
using bageri_api.DTOs.Products;
using bageri_api.Entities;

namespace bageri_api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PostProductDto, Product>().ForMember(d => d.ProductName, m => m.MapFrom(s => s.ProductName));
        CreateMap<Product, GetProductDto>().ForMember(d => d.ProductName, m => m.MapFrom(s => s.ProductName));
    }
}

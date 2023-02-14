using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<TypeOfClothing, TypeDto>();
            CreateMap<Clothing, ClothingDto>().ForMember("ImageUrl", opt => opt.MapFrom<UrlToNameResolver, string>(c => c.Image));
        }
    }
}

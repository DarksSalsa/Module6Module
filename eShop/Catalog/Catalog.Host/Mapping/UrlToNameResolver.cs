using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Configuration;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Mapping
{
    public class UrlToNameResolver : IMemberValueResolver<Clothing, ClothingDto, string, object>
    {
        private readonly CatalogConfig _config;
        public UrlToNameResolver(IOptionsSnapshot<CatalogConfig> config)
        {
            _config = config.Value;
        }

        public object Resolve(Clothing source, ClothingDto destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
        }
    }
}

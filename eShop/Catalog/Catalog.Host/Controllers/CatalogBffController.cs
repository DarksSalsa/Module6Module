using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Core.Identity;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(RoutingConfig.DefaultRoute)]
    public class CatalogBffController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly IShowcaseService _showcaseService;

        public CatalogBffController(
            ILogger<CatalogBffController> logger,
            IShowcaseService showcaseService)
        {
            _logger = logger;
            _showcaseService = showcaseService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedItemResponse<ClothingDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetClothingByPage(PaginatedClothingRequest request)
        {
            var result = await _showcaseService.GetByPageAsync(request.PageIndex, request.PageSize, request.BrandIdFilter, request.CategoryIdFilter);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<BrandDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _showcaseService.GetAllBrandsAsync();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<TypeDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTypes()
        {
            var result = await _showcaseService.GetAllTypesAsync();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _showcaseService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClothingDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetClothing(UniversalGetByIdRequest request)
        {
            var result = await _showcaseService.GetClothingByIdAsync(request.Id);
            return Ok(result);
        }
    }
}

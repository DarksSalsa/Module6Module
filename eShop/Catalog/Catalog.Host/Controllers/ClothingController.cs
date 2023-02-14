using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Configuration;
using Infrastructure.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(RoutingConfig.DefaultRoute)]
    [Scope("catalog.other")]
    public class ClothingController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly IClothingService _clothingService;

        public ClothingController(
            ILogger<CatalogBffController> logger,
            IClothingService clothingService)
        {
            _logger = logger;
            _clothingService = clothingService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalAddResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddClothingRequest request)
        {
            var result = await _clothingService.AddAsync(request.Name, request.Color, request.Size, request.CategoryId, request.BrandId, request.Price, request.AvailableStock, request.Image, request.Season);
            return Ok(new UniversalAddResponse() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalDeleteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(UniversalDeleteRequest request)
        {
            var result = await _clothingService.DeleteAsync(request.Id);
            return Ok(new UniversalDeleteResponse() { Result = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateClothingResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UniversalUpdateRequest request)
        {
            var result = await _clothingService.UpdateAsync(request.Id, request.Property, request.Value);
            return Ok(result);
        }
    }
}

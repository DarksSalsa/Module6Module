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
    public class BrandController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(
            ILogger<CatalogBffController> logger,
            IBrandService brandService)
        {
            _logger = logger;
            _brandService = brandService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalAddResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddBrandRequest request)
        {
            var result = await _brandService.AddAsync(request.Name);
            return Ok(new UniversalAddResponse() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalDeleteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(UniversalDeleteRequest request)
        {
            var result = await _brandService.DeleteAsync(request.Id);
            return Ok(new UniversalDeleteResponse() { Result = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateBrandResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UniversalUpdateRequest request)
        {
            var result = await _brandService.UpdateAsync(request.Id, request.Property, request.Value);
            return Ok(result);
        }
    }
}

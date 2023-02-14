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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ILogger<CatalogBffController> logger,
            ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalAddResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddCategoryRequest request)
        {
            var result = await _categoryService.AddAsync(request.Name, request.TypeId);
            return Ok(new UniversalAddResponse() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalDeleteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(UniversalDeleteRequest request)
        {
            var result = await _categoryService.DeleteAsync(request.Id);
            return Ok(new UniversalDeleteResponse() { Result = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateCategoryResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UniversalUpdateRequest request)
        {
            var result = await _categoryService.UpdateAsync(request.Id, request.Property, request.Value);
            return Ok(result);
        }
    }
}

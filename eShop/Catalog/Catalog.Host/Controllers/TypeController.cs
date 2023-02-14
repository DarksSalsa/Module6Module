﻿using System.Net;
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
    public class TypeController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly ITypeService _typeService;

        public TypeController(
            ILogger<CatalogBffController> logger,
            ITypeService typeService)
        {
            _logger = logger;
            _typeService = typeService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalAddResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddTypeRequest request)
        {
            var result = await _typeService.AddAsync(request.Name);
            return Ok(new UniversalAddResponse() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UniversalDeleteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(UniversalDeleteRequest request)
        {
            var result = await _typeService.DeleteAsync(request.Id);
            return Ok(new UniversalDeleteResponse() { Result = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateTypeResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UniversalUpdateRequest request)
        {
            var result = await _typeService.UpdateAsync(request.Id, request.Property, request.Value);
            return Ok(result);
        }
    }
}

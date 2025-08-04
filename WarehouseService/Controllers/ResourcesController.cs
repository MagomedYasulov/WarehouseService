using Microsoft.AspNetCore.Mvc;
using WarehouseService.Abstractions;
using WarehouseService.Filters;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Controllers
{
    [Route("api/v1/[controller]")]
    [TypeFilter<ApiExceptionFilter>]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> Create(CreateResourceDto model)
        {
            var courseDto = await _resourceService.Create(model);
            return Ok(courseDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResourceDto>> Get(Guid id)
        {
            var courseDto = await _resourceService.Get(id);
            return Ok(courseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ResourceDto[]>> Get(ResourceFilter filter)
        {
            var coursesDto = await _resourceService.Get(filter);
            return Ok(coursesDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResourceDto>> Update(Guid id, UpdateResourceDto model)
        {
            var courseDto = await _resourceService.Update(id, model);
            return Ok(courseDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, bool isSoft)
        {
            await _resourceService.Delete(id, isSoft);
            return Ok();
        }
    }
}

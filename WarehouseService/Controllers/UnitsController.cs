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
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpPost]
        public async Task<ActionResult<UnitDto>> Create(CreateUnitDto model)
        {
            var unitDto = await _unitService.Create(model);
            return Ok(unitDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UnitDto>> Get(Guid id)
        {
            var unitDto = await _unitService.Get(id);
            return Ok(unitDto);
        }

        [HttpGet]
        public async Task<ActionResult<UnitDto[]>> Get(UnitsFilter filter)
        {
            var coursesDto = await _unitService.Get(filter);
            return Ok(coursesDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UnitDto>> Update(Guid id, UpdateUnitDto model)
        {
            var unitDto = await _unitService.Update(id, model);
            return Ok(unitDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, bool isSoft)
        {
            await _unitService.Delete(id, isSoft);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WarehouseService.Abstractions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InventoryBalancesController : ControllerBase
    {
        private readonly IInventoryBalanceService _inventoryBalanceService;

        public InventoryBalancesController(IInventoryBalanceService inventoryBalanceService)
        {
            _inventoryBalanceService = inventoryBalanceService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientDto>> Get(Guid id)
        {
            var courseDto = await _inventoryBalanceService.Get(id);
            return Ok(courseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ClientDto[]>> Get()
        {
            var coursesDto = await _inventoryBalanceService.Get();
            return Ok(coursesDto);
        }
    }
}

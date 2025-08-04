using Microsoft.AspNetCore.Mvc;
using WarehouseService.Abstractions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Controllers
{
    [Route("api/v1[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> Create(CreateClientDto model)
        {
            var courseDto = await _clientService.Create(model);
            return Ok(courseDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientDto>> Get(Guid id)
        {
            var courseDto = await _clientService.Get(id);
            return Ok(courseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ClientDto[]>> Get(ClientsFilter filter)
        {
            var coursesDto = await _clientService.Get(filter);
            return Ok(coursesDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResourceDto>> Update(Guid id, UpdateClientDto model)
        {
            var courseDto = await _clientService.Update(id, model);
            return Ok(courseDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, bool isSoft)
        {
            await _clientService.Delete(id, isSoft);
            return Ok();
        }
    }
}

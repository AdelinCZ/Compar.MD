using Compar.MD.Application.StoreCommands;
using Compar.MD.Application.StoreQueries;
using Compar.MD.Server.Mapping;
using Compar.MD.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Compar.MD.Server.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IMediator mediator;

        public StoresController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);

            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<StoreDto>> GetStores()
        {
            var stores = await this.mediator.Send(new GetAllStoresQuery());
            var dto = Mapper.GetStores(stores);

            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> AddStore(StoreDto data)
        {
            var command = new AddStoreCommand()
            {
                StoreName = data.StoreName
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteStore([FromBody] string id)
        {
            var command = new DeleteStoreCommand()
            {
                StoreId = id
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }
    }
}
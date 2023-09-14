using Compar.MD.Application.ProductTypeCommands;
using Compar.MD.Application.ProductTypeQueries;
using Compar.MD.Server.Mapping;
using Compar.MD.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Compar.MD.Server.Controllers
{
    [Route("api/productTypes")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductTypesController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);

            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ProductTypeDto>> GetProductTypes()
        {
            var productTypes = await this.mediator.Send(new GetAllProductTypesQuery());
            var dto = Mapper.GetProductTypes(productTypes);

            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(ProductTypeDto data)
        {
            var command = new AddProductTypeCommand()
            {
                ProductTypeName = data.ProductTypeName
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteProductType([FromBody] string id)
        {
            var command = new DeleteProductTypeCommand()
            {
                ProductTypeId = id
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }
    }
}
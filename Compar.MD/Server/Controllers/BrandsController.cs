using Compar.MD.Application.BrandCommands;
using Compar.MD.Application.BrandQueries;
using Compar.MD.Server.Mapping;
using Compar.MD.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Compar.MD.Server.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public BrandsController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);

            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<BrandDto>> GetBrands()
        {
            var brands = await this.mediator.Send(new GetAllBrandsQuery());
            var dto = Mapper.GetBrands(brands);

            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandDto data)
        {
            var command = new AddBrandCommand()
            {
                BrandName = data.BrandName
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteBrand([FromBody] string id)
        {
            var command = new DeleteBrandCommand()
            {
                BrandId = id
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }
    }
}
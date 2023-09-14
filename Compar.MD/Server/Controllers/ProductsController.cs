using Compar.MD.Application.ProductCommands;
using Compar.MD.Application.ProductQueries;
using Compar.MD.Server.Mapping;
using Compar.MD.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Compar.MD.Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);

            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await this.mediator.Send(new GetAllProductsQuery());

            return Mapper.GetAllProducts(products);
        }

        [HttpGet]
        [Route("selected")]
        public async Task<List<ProductDto>> GetSelectedProducts()
        {
            var products = await this.mediator.Send(new GetSelectedProductsQuery());

            return Mapper.GetAllProducts(products);
        }

        [HttpPost]
        [Route("selected")]
        public async Task<IActionResult> UpdateSelectedProduct(SelectedItemsDto data)
        {
            var command = new SelectProductCommand()
            {
                NewSelectedProducts = data.NewSelectedItems
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductDto data)
        {
            var command = new AddProductCommand()
            {
                Name = data.Name,
                Brand = data.ProductBrand,
                Type = data.ProductType,
                ImageURL = data.ImageURL,
            };

            foreach (var price in data.Prices)
            {
                var supplier = (price.SupplierName, price.Price);

                command.Suppliers.Add(supplier);
            }

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] string id)
        {
            var command = new DeleteProductCommand()
            {
                ProductId = id
            };

            var result = await this.mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }
    }
}
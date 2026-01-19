
using Asp.Versioning;
using ECEMAPI.Controllers.Version1.Employees;
using ECEMCore.Features.Products.Commands.CreateProduct;
using ECEMCore.Features.Products.Commands.RemoveProduct;
using ECEMCore.Features.Products.Commands.UpdateProduct;
using ECEMCore.Features.Products.Queries.GetAllProducts;
using ECEMCore.Features.Products.Queries.GetProduct;
using ECMDomain.Entities.Products.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECEMAPI.Controllers.Version1.Product
{
    [Authorize]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController(ISender sender) : BaseController
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto request
           , CancellationToken cancellation = default)
        {
            var response = await _sender.Send(new CreateProductCommand(request), cancellation);
            return CreateResult(response);
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetProductAsync(Guid ProductId, CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new GetProductQuery(ProductId), cancellation);
            return CreateResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync(CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new GetAllProductsQuery(), cancellation);
            return CreateResult(response);
        }

        [HttpPut("{ProductId}")]
        public async Task<IActionResult> UpdateProductAsync(
            Guid ProductId,
            UpdateProductDto request,
            CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new UpdateProductCommand(ProductId, request), cancellation);
            return CreateResult(response);
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> DeleteProductAsync(Guid ProductId,
           CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new RemoveProductCommand(ProductId), cancellation);
            return CreateResult(response);
        }
    }
}

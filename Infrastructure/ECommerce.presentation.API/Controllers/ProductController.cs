using ECommerce.Domain.Entities.Products;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects;
using ECommerce.Shared.DataTransfareObjects.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.API.Controllers
{
    public class ProductController ( IProductService productService) : APIBaseController
    {

        [HttpGet]

        public async Task<ActionResult<PaginatedResult<ProductResponse>>> GetProducts([FromQuery]ProductQueryParameters Parameters, CancellationToken cancellationToken = default)
        {
            var respones = await productService.GetProductsAsync(Parameters , cancellationToken);
            return Ok(respones);
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> Get( int id ,CancellationToken cancellationToken = default)
        {
            var respones = await productService.GetByIdAsync( id , cancellationToken);
            return Ok(respones);

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands(CancellationToken cancellationToken = default)
        {
            var respones = await productService.GetBrandsAsync(cancellationToken);
            return Ok(respones);

        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes(CancellationToken cancellationToken = default)
        {
            var respones = await productService.GetTypesAsync(cancellationToken);
            return Ok(respones);

        }

    }
}

using ECommerce.Domain.Entities.Products;
using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects;
using ECommerce.Shared.DataTransfareObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IProductService
    {
        Task<Result<ProductResponse?>> GetByIdAsync(int id , CancellationToken cancellationToken );
        Task<PaginatedResult<ProductResponse>> GetProductsAsync( ProductQueryParameters parameters,CancellationToken cancellationToken);
        Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken);

    }
}

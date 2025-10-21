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
        Task<ProductResponse?> GetByIdAsync(int id , CancellationToken cancellationToken );
        Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken);

    }
}

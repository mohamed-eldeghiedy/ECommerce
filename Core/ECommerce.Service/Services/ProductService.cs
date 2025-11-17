using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.Service.Exceptions;
using ECommerce.Service.Specification;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects;
using ECommerce.Shared.DataTransfareObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.Service.Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper)  : IProductService
    {
        public async Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>()
                .GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<BrandResponse>>(brands);
        }

        public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
           var Product = await unitOfWork.GetRepository<Product, int>()
                .GetAsync(new ProductWithBrandTypeSpecification(id) , cancellationToken);
            if (Product == null)
                throw new ProductNotFoundExecption(id);
            return mapper.Map<ProductResponse?>(Product);
        }

        public async Task<PaginatedResult<ProductResponse>> GetProductsAsync(ProductQueryParameters Parameters, CancellationToken cancellationToken)
        {
            var spec = new ProductWithBrandTypeSpecification(Parameters);
            var data = await unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(spec,cancellationToken);

            //var TotalCount = await unitOfWork.GetRepository<Product>()
            //    .CountAsync(new ProductCountSpecification(Parameters), cancellationToken);
            var products = mapper.Map<IEnumerable<ProductResponse>>(data);

            return new(Parameters.PageIndex, products.Count(), products.Count(), products);
        }

        public async Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken)
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>()
                        .GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<TypeResponse>>(Types);
        }
    }
}

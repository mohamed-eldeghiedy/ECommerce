using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .GetByIdAsync(id , cancellationToken);
            return mapper.Map<ProductResponse?>(Product);
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var Products = await unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ProductResponse>>(Products);
        }

        public async Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken)
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>()
                        .GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<TypeResponse>>(Types);
        }
    }
}

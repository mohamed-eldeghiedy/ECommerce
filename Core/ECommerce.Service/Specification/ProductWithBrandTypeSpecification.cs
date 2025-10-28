using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specification
{
    internal class ProductWithBrandTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandTypeSpecification(ProductQueryParameters parameters) :base(CreateCriteria (parameters))
        {
            AddInclude(p => p.ProductBrand); 
            AddInclude(p => p.ProductType);
            ApplyPaging(parameters.PageSize, parameters.PageIndex);
            Sort(parameters);


        }

        private void Sort(ProductQueryParameters parameters)
        {
            switch (parameters.SortBy)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }

        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return p=>
                (!parameters    .BrandId.HasValue || p.BrandId == parameters.BrandId) 
                &&
                (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search) );
        }

        public ProductWithBrandTypeSpecification(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

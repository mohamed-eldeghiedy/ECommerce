using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specification
{
    internal sealed class ProductCountSpecification(ProductQueryParameters Parameters) :
        BaseSpecification<Product>(CreateCriteria(Parameters))
    {


        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return p =>
                (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search));
        }
    }
}

using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specification
{
    internal class GetProductsByIdsSpecification(List<int> ids)
        : BaseSpecification<Product>(p=>ids.Contains(p.Id));


}

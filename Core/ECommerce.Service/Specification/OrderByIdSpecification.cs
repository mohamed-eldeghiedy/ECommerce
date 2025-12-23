using ECommerce.Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specification
{
    internal class OrderByIdAndEmailSpecification : BaseSpecification<Order>
    {
        public OrderByIdAndEmailSpecification(string email , Guid id) : base(o=>o.Id == id 
        && o.UserEmail == email)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}

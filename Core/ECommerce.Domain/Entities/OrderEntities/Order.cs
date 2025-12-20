using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.OrderEntities
{
    public class Order : Entity<Guid>
    {
        public ICollection<OrderItem> Items { get; set; } = [];
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; } 
        public decimal Subtotal { get; set; }
        public string UserEmail { get; set; }
        public OrderAddress Address { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string PaymentIntentId { get; set; }

    }


    public class OrderAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
    }
}

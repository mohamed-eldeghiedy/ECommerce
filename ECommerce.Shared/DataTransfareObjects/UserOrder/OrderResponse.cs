using ECommerce.Shared.DataTransfareObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects.UserOrder
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public ICollection<OrderItemDTO> Items { get; set; } = [];
        public string DeliveryMethod { get; set; }
        public decimal? DeliveryMethodCost { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string UserEmail { get; set; }
        public AddressDTO Address { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string Status { get; set; } 
        public string PaymentIntentId { get; set; }
    }
}

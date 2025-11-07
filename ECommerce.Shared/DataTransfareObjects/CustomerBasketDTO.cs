using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects
{
    public class CustomerBasketDTO
    {
        public String Id { get; set; }

        public ICollection<BasketItemDTO>  basketItemDTO { get; set; } 
    }

    public class BasketItemDTO
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string PictureUrl {  get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; set; }
    }
}

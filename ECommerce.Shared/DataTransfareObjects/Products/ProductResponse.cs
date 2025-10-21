using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects.Products
{
    public record ProductResponse
    {
        public int Id { get; set; } 
        public string Name { get; init; } 
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string PictureUrl { get; init; }
        public string Brand { get; init; }
        public string Type { get; init; }
    }
}

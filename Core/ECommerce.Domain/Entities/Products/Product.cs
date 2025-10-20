using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Products
{
    public class Product : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductType ProductType { get; set; } = default!;
        public int TypeId { get; set; }
        public ProductBrand ProductBrand { get; set; } = default!;
        public int BrandId { get; set; }
    }
}

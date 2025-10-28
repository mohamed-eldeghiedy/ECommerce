using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductQueryParameters
    {
        private const int MaxPageSize = 10;
        private const int DefaultePageZize = 5;


        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions? SortBy { get; set; }

        private int pageSize = DefaultePageZize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize :
                value< DefaultePageZize ? DefaultePageZize : value;
        }
        public int PageIndex { get; set; } = 1;
    }

    public enum ProductSortingOptions
    {
        NameAsc =1,
        NameDesc =2,
        PriceAsc =3,
        PriceDesc =4,

    }
}

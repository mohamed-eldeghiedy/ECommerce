using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects
{
     public record PaginatedResult<TResult>(
        int PageIndex,
        int PageCount,
        int TotalCount,
        IEnumerable<TResult> Data);
    
}

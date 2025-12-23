using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects.UserOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IOrderService
    {
        Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email , CancellationToken cancellationToken );
        Task<Result<OrderResponse>> GetByIdAsync( string email,Guid id ,CancellationToken cancellationToken );

        Task<IEnumerable<OrderResponse>> GetByUserEmailAsync(string email , CancellationToken cancellationToken);

        Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync(CancellationToken cancellationToken);
    }
}

using ECommerce.Shared.DataTransfareObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects.UserOrder
{
    public  record OrderRequest(AddressDTO Address, string basketId , int DeliveryMethodId);

}

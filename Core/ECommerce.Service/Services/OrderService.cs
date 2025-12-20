using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Basket;
using ECommerce.Domain.Entities.OrderEntities;
using ECommerce.Domain.Entities.Products;
using ECommerce.Persistence.Repositories;
using ECommerce.Service.Specification;
using ECommerce.ServiceAbstraction;
using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects.UserOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerce.ServiceAbstraction.Common.Error;
using Product = ECommerce.Domain.Entities.Products.Product;

namespace ECommerce.Service.Services
{
    internal class OrderService(IUnitOfWork unitOfWork 
        , IBasketRepository basketRepository
        , IMapper mapper 
        , CancellationToken cancellationToken)
        : IOrderService
    {
        public async Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email)
        {
           var basket= await basketRepository.GetAsync(request.basketId);
            if (basket is null)
                return Error.NotFound(" basket Not Found", $"Basket with id {request.basketId} was not Found");
            var method = await unitOfWork.GetRepository<DeliveryMethod, int >()
                .GetByIdAsync(request.DeliveryMethodId , cancellationToken);
            if(method is null )
                return Error.NotFound(" Delivery Method Not Found", $"Delivery Method with id {request.DeliveryMethodId} was not Found");

            var ProductRepo =  unitOfWork.GetRepository<Product , int >();
            var ids = basket.Items.Select(i => i.Id).ToList();
            var products = (await ProductRepo.GetAllAsync(new GetProductsByIdsSpecification(ids) , cancellationToken))
                .ToDictionary(p=>p.Id);
            var orderItems = new List<OrderItem>();

            var validationErrors = new List<Error>();
            foreach (var item in basket.Items)
            {
                
                if (!products.TryGetValue(item.Id , out Product? product ))
                {
                    validationErrors.Add(Error.Vailedation("Product Not Found", $"Product with id {item.Id} was not Found"));
                    continue;
                }

                var orderItem = new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductInOrderItem
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                };
                orderItems.Add(new());

            }

            if (validationErrors.Any())
                return validationErrors;
            var Address = mapper.Map<OrderAddress>(request.Address);
            var subtotal = orderItems.Sum(i => i.Price * i.Quantity);
            var order = new Order
            {
                DeliveryMethod = method,
                UserEmail = email,
                Items = orderItems,
                Subtotal = subtotal,
                Address = Address

            };

            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            orderRepo.Add(order);
            await unitOfWork.SaveChengesAsync(cancellationToken);
            return mapper.Map<OrderResponse>(order);

        }
    }
}

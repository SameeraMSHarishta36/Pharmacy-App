﻿

using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Repositories.Interfaces;
using PharmApp.Services.Interfaces;

namespace PharmApp.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepo = orderRepository;
        }
        public OrderModel GetOrderDetails(string OrderId)
        {
            var model = _orderRepo.GetOrderDetails(OrderId);
            if (model != null && model.Items.Count > 0)
            {
                decimal subTotal = 0;
                foreach (var item in model.Items)
                {
                    item.Total = item.UnitPrice * item.Quantity;
                    subTotal += item.Total;
                }
                model.Total = subTotal;
                //5% tax
                model.Tax = Math.Round((model.Total * 5) / 100, 2);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;

        }

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return _orderRepo.GetUserOrders(UserId);
        }

        public int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, AddressModel address)
        {
            Order order = new Order
            {
                PaymentId = paymentId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                Id = orderId,
                Street = address.Street,
                Locality = address.Locality,
                City = address.City,
                ZipCode = address.ZipCode,
                PhoneNumber = address.PhoneNumber
            };
            foreach (var item in cart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.ItemId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total
                };
                order.OrderItems.Add(orderItem);
            }
            _orderRepo.Add(order);
            return _orderRepo.SaveChanges();
        }
    }
}

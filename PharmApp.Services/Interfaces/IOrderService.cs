using PharmApp.DAL.Entities;
using PharmApp.Models;

namespace PharmApp.Services.Interfaces
{
    public  interface IOrderService
    {
        OrderModel GetOrderDetails(string OrderId);
        IEnumerable<Order> GetUserOrders(int UserId);
        int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, AddressModel address);
    }
}

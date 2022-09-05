using PharmApp.DAL.Entities;
using PharmApp.Models;


namespace PharmApp.Repositories.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        OrderModel GetOrderDetails(string id);
        IEnumerable<Order> GetUserOrders(int UserId);
    }
}

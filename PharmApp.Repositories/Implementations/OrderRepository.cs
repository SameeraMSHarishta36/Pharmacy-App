using Microsoft.EntityFrameworkCore;
using PharmApp.DAL;
using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Repositories.Interfaces;

namespace PharmApp.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public AppDbContext dbContext 
        { 
            get 
            {
                return _db as AppDbContext;
            } 
        }
        public OrderRepository(AppDbContext dbContext): base(dbContext)
        {

        }
        public OrderModel GetOrderDetails(string orderId)
        {
            var model = (from order in dbContext.Orders
                         where order.Id == orderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             CreatedDate = order.CreatedDate,
                             Items = (from orderItem in dbContext.OrderItems
                                      join item in dbContext.Items
                                      on orderItem.ItemId equals item.Id
                                      where orderItem.OrderId == orderId
                                      select new ItemModel
                                      {
                                          Id = orderItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = orderItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = orderItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        
        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return dbContext.Orders.Include(o => o.OrderItems)
               .Where(x => x.UserId == UserId).ToList();
        }
    }
}


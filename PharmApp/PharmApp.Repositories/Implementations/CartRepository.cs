using Microsoft.EntityFrameworkCore;
using PharmApp.DAL;
using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Repositories.Interfaces;

namespace PharmApp.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        AppDbContext Context
        {
            get
            {
                return _db as AppDbContext;
            }
        }
        public CartRepository(AppDbContext db) : base(db)
        {

        }
        
        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = Context.CartItems.Where(ci => ci.CartId == cartId && ci.Id == itemId).FirstOrDefault();
            if(item!=null)
            {
                Context.CartItems.Remove(item);
                return Context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public Cart GetCart(Guid CartId)
        {
            return Context.Carts.Include("CartItems").Where(p => p.Id == CartId && p.IsActive == true).FirstOrDefault();
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in Context.Carts
                         where cart.Id == CartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             Items = (from cartItem in Context.CartItems
                                      join item in Context.Items
                                      on cartItem.ItemId equals item.Id
                                      where cartItem.CartId == CartId
                                      select new ItemModel
                                      {
                                          Id = cartItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = cartItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = cartItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return Context.SaveChanges();
        }

        public int UpdateQuantity(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId);
            if (cart != null)
            {
                var cartItems = cart.CartItems.ToList();

                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems[i].Id == itemId)
                    {
                        flag = true;
                        cartItems[i].Quantity += (Quantity);
                        break;
                    }
                }
                if (flag)
                {
                    cart.CartItems = cartItems;
                    return Context.SaveChanges();
                }
            }
            return 0;
        }
    }
}

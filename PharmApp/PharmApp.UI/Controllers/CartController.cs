﻿using Microsoft.AspNetCore.Mvc;
using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Services.Interfaces;
using PharmApp.UI.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PharmApp.UI.Controllers
{
    public class CartController : Controller
    {
        IUserAccessor _userAccessor;
        ICartService _cartService;
        public CartController(IUserAccessor userAccessor, ICartService cartService)
        {
            _userAccessor = userAccessor;
            _cartService = cartService;
        }

        Guid CartId
        {
            get
            {
                Guid Id;
                string CId = Request.Cookies["CId"];
                if (string.IsNullOrEmpty(CId))
                {
                    Id = Guid.NewGuid();
                    Response.Cookies.Append("CId", Id.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                }
                else
                {
                    Id = Guid.Parse(CId);
                }
                return Id;
            }
        }
        UserModel CurrentUser
        {
            get
            {
                return _userAccessor.GetUser();
            }

        }
       
        public IActionResult Index()
        {
            CartModel cart = _cartService.GetCartDetails(CartId);
             if(CurrentUser!=null && cart!=null)
             {
              _cartService.UpdateCart(cart.Id, CurrentUser.Id);
              TempData.Set("Cart", cart);
             }
            return View(cart);
        }
        [Route("Cart/AddToCart/{ItemId}/{UnitPrice}/{Quantity}")]
        public IActionResult AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            int UserId = CurrentUser != null ? CurrentUser.Id : 0;

            if (ItemId > 0 && Quantity > 0)
            {
                Cart cart = _cartService.AddItem(UserId, CartId, ItemId, UnitPrice, Quantity);
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                var data = JsonSerializer.Serialize(cart, options);
                return Json(data);
            }
            else
            {
                return Json("");
            }
        }

        [Route("Cart/UpdateQuantity/{Id}/{Quantity}")]
        public IActionResult UpdateQuantity(int Id, int Quantity)
        {
            int count = _cartService.UpdateQuantity(CartId, Id, Quantity);
            return Json(count);
        }

        public IActionResult DeleteItem(int Id)
        {
            int count = _cartService.DeleteItem(CartId, Id);
            return Json(count);
        }
        public IActionResult GetCartCount()
        {
            int count = _cartService.GetCartCount(CartId);
            return Json(count);
        }
        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(AddressModel model)
        {
            TempData.Set("Address", model);
            return RedirectToAction("Index", "Payment");
        }

    }

    
}


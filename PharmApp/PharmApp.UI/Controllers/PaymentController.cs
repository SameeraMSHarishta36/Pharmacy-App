using Microsoft.AspNetCore.Mvc;
using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Services.Interfaces;
using PharmApp.UI.Helpers;

namespace PharmApp.UI.Controllers
{
    public class PaymentController : Controller
    {
        IConfiguration _configuration;
        IPaymentService _paymentService;
        IUserAccessor _userAccessor;
        IOrderService _orderService;
        UserModel CurrentUser
        {
            get
            {
                return _userAccessor.GetUser();
            }
        }

        public PaymentController(IConfiguration configuration, IPaymentService paymentService,IUserAccessor userAccessor, IOrderService _orderService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            _userAccessor = userAccessor;
        }
        public IActionResult Index()
        {
            PaymentModel payment = new PaymentModel();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
                payment.GrandTotal = Math.Round(cart.GrandTotal);
                payment.Currency = "INR";
                string items = "";
                foreach (var item in cart.Items)
                {
                    items += item.Name + ",";
                }
                payment.Description = items;
                payment.RazorpayKey = _configuration["Razorpay:Key"];
                payment.Receipt = Guid.NewGuid().ToString();
                payment.OrderId = _paymentService.CreateOrder(payment.GrandTotal * 100, payment.Currency, payment.Receipt);
            }
            return View(payment);
        }
        public IActionResult Status(IFormCollection form)
        {
            try
            {
                if (form.Keys.Count > 0 && !string.IsNullOrWhiteSpace(form["rzp_paymentid"]))
                {
                    string paymentId = form["rzp_paymentid"];
                    string orderId = form["rzp_orderid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currency"];

                    var payment = _paymentService.GetPaymentDetails(paymentId);
                    bool IsSignVerified = _paymentService.VerifySignature(signature, orderId, paymentId);

                    if (IsSignVerified && payment != null)
                    {
                        CartModel cart = TempData.Get<CartModel>("Cart");
                        PaymentDetail model = new PaymentDetail();

                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.GrandTotal = cart.GrandTotal;

                        model.Status = payment.Attributes["status"]; //captured
                        model.TransactionId = transactionId;
                        model.Currency = payment.Attributes["currency"];
                        model.Email = payment.Attributes["email"];
                        model.Id = paymentId;
                        model.UserId = CurrentUser.Id;

                        int status = _paymentService.SavePaymentDetails(model);
                        if (status > 0)
                        {
                            Response.Cookies.Append("CId", ""); //resettingg cartId in cookie

                            AddressModel address = TempData.Get<AddressModel>("Address");
                            

                            //TO DO: Send email
                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                        else
                        {
                            ViewBag.Message = "Although, due to some technical issues it's not get updated in our side. We will contact you soon..";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.Message = "Your payment has been failed. You can contact us at support@dotnettricks.com.";
            return View();
        }

         public IActionResult Receipt()
        {
            PaymentDetail model = TempData.Peek<PaymentDetail>("PaymentDetails");
            return View(model);
        }
    }
    }








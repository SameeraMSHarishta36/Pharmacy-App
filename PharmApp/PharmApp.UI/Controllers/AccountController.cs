using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Services.Interfaces;
using PharmApp.UI.Models;
using System.Security.Claims;
using System.Text.Json;

namespace PharmApp.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model,string returnUrl)
        {
            UserModel user = _authService.ValidateUser(model.Email, model.Password);
            if(user!=null)
            {
                GenerateAuthTicket(user);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                if (user.Roles.Contains("User"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                }
                else if (user.Roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
            }
            return View();
        }

        private async void GenerateAuthTicket(UserModel user)
        {
            string strData = JsonSerializer.Serialize(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData,strData),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,String.Join(",",user.Roles))
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                AllowRefresh = true,
            });
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    Name=model.Name,
                    Email=model.Email,
                    Password=model.Password,
                    PhoneNumber=model.PhoneNumber,
                    CreatedDate=DateTime.Now

                };
                bool result = _authService.CreateUser(user, "User");
                if(result)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        public IActionResult UnAuthorize()
        {
            return View();
        }
    }
}

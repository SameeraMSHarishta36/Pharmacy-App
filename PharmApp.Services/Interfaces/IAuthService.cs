using PharmApp.DAL.Entities;
using PharmApp.Models;


namespace PharmApp.Services.Interfaces
{
    public interface IAuthService
    {
        UserModel ValidateUser(string Email, string Password);
        bool CreateUser(User user, string Role);
    }
}

using PharmApp.DAL.Entities;
using PharmApp.Models;

namespace PharmApp.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        UserModel ValidateUser(string Email, string Password);
        bool CreateUser(User user, string Role);
    }
}

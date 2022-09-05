using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Repositories.Interfaces;
using PharmApp.Services.Interfaces;


namespace PharmApp.Services.Implementations
{
    public class AuthService: IAuthService
    {
        IUserRepository _userRepo;
        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public bool CreateUser(User user, string Role)
        {
            return _userRepo.CreateUser(user, Role);
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            return _userRepo.ValidateUser(Email, Password);
        }
    }
}

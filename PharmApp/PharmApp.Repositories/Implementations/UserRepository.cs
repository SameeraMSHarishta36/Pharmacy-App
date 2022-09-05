using Microsoft.EntityFrameworkCore;
using PharmApp.DAL;
using PharmApp.DAL.Entities;
using PharmApp.Models;
using PharmApp.Repositories.Interfaces;
using BC = BCrypt.Net.BCrypt;


namespace PharmApp.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
         AppDbContext Context 
        { 
            get
            {
                return _db as AppDbContext;
            }
        }
        public UserRepository(AppDbContext db): base(db)
        {

        }
        public bool CreateUser(User user, string Role)
        {
            try
            {
                user.Password = BC.HashPassword(user.Password);
            Role role = Context.Roles.Where(r => r.Name == Role).FirstOrDefault();
            user.Roles.Add(role);

            Context.Users.Add(user);
            Context.SaveChanges();
            return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            User user = Context.Users.Include(u=>u.Roles).Where(u => u.Email == Email).FirstOrDefault();
            if (user != null)
            {
                bool isVerified = BC.Verify(Password, user.Password);
                if (isVerified)
                {
                    UserModel model = new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = user.Roles.Select(r => r.Name).ToArray()
                    };
                    return model;
                }
            }
            return null;
        }
    }
}

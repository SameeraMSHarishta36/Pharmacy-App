using PharmApp.Models;

namespace PharmApp.UI.Helpers
{
    public interface IUserAccessor
    {
        UserModel GetUser();
    }
}

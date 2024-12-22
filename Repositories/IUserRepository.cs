using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public interface IUserRepository
    {
        int AddUser(User user);
        List<User> GetAllUsers();
        string GetHashedPass(string email);
        User GetUserByEmail(string email, string password);
        User GetUserByID(int ID);
        User GetUserByPhone(string phone);
        List<User> GetUsersByName(string name);
    }
}
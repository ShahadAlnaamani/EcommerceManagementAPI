using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Services
{
    public interface IUserService
    {
        int AddAdmin(UserInDTO Newadmin);
        int AddUser(UserInDTO Newuser);
        List<UserOutDTO> GetAllUsers();
        UserOutDTO GetMyDetails(int ID);
        UserOutDTO GetUserByID(int ID);
        List<UserOutDTO> GetUserByName(string Name);
        UserOutDTO GetUserByPhoneNo(string phone);
        User Login(string email, string password);
        string PassHasher(string password);
    }
}
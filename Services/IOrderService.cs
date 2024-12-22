using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Services
{
    public interface IOrderService
    {
        List<Order> GetMyOrders(int userID);
        Order GetOrderByID(int userID, int OrderID);
    }
}
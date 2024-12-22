using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        List<Order> GetAllOrdersByUserID(int ID);
        Order GetOrdersByOrderID(int UserID, int OrderID);
    }
}
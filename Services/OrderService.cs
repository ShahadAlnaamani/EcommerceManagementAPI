using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;
using EcommerceManagementAPI.Repositories;

namespace EcommerceManagementAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderrepository;

        public OrderService(IOrderRepository orderrepo)
        {
            _orderrepository = orderrepo;
        }

        public List<Order> GetMyOrders(int userID)
        {
            return _orderrepository.GetAllOrdersByUserID(userID);
        }
        public Order GetOrderByID(int userID, int OrderID)
        {
            return _orderrepository.GetOrdersByOrderID(userID, OrderID);
        }
    }
}

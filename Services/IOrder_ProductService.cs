using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Services
{
    public interface IOrder_ProductService
    {
        bool AddNewProduct_Order(int OrederID, int productID, int Qty);
        List<Order_Product> GetOrderProdsByOrderID(int orderID);
        List<Order_Product> ViewOrderProds();
    }
}
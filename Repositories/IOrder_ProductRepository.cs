﻿using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public interface IOrder_ProductRepository
    {
        bool AddProduct_Order(Order_Product Ord_Prod);
        List<Order_Product> GetAllOrderProds();
        List<Order_Product> GetOrderProdsByOrderID(int orderID);
    }
}
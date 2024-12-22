﻿using EcommerceManagementAPI.Models;
using EcommerceManagementAPI.Repositories;

namespace EcommerceManagementAPI.Services
{
    public class Order_ProductService : IOrder_ProductService
    {
        private readonly IOrder_ProductRepository _order_productrepository;

        public Order_ProductService(IOrder_ProductRepository orderprodrepo)
        {
            _order_productrepository = orderprodrepo;
        }

        public bool AddNewProduct_Order(int OrederID, int productID, int Qty)
        {
            var orderprod = new Order_Product
            {
                OrderID = OrederID,
                ProductID = productID,
                Quantity = Qty
            };

            return _order_productrepository.AddProduct_Order(orderprod);
        }

        public List<Order_Product> ViewOrderProds()
        {
            return _order_productrepository.GetAllOrderProds();
        }

        public List<Order_Product> GetOrderProdsByOrderID(int orderID)
        {
            return _order_productrepository.GetOrderProdsByOrderID(orderID);
        }
    }
}

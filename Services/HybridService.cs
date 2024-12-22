using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;
using EcommerceManagementAPI.Repositories;

namespace EcommerceManagementAPI.Services
{
    public class HybridService : IHybridService
    {
        private readonly IOrderRepository _orderrepository;
        private readonly IProductService _productservice;
        private readonly IOrder_ProductService _orderproductservice;

        public HybridService(IOrderRepository orderrepo,IProductService productService, IOrder_ProductService order_ProductService)
        {
            _orderrepository = orderrepo;
            _productservice = productService;
            _orderproductservice = order_ProductService;
        }

        public OrderReceiptDTO NewOrder(List<OrderInDTO> orders, int userID)
        {
            decimal TotalAmount = 0;
            var Allproducts = new List<Product>();
            //check valid product 
            foreach (var order in orders)
            {
                int prodID = _productservice.GetProductByName(order.productName);
                Product product = _productservice.GetFullProductByID(prodID);
                int availableStock = product.Stock;
                //Validation of request
                if (order.productName == null)
                { throw new Exception("<!>One or more of the products in the order are invalid<!>"); }
                if (availableStock >= order.quantity)
                {
                    Allproducts.Add(product);
                    TotalAmount = TotalAmount + order.quantity * product.Price;
                }
                else { throw new Exception("<!>There is not enough stock to complete your order<!>"); }
            }
            var Neworder = new Order
            {
                UserID = userID,
                OrderDate = DateTime.Now,
                TotalAmount = TotalAmount,
            };
            int orderID = _orderrepository.AddOrder(Neworder);
            for (int i = 0; i < Allproducts.Count; i++)
            {
                var updatedProd = new ProductInDTO
                {
                    Stock = Allproducts[i].Stock - orders[i].quantity,
                };
                bool complete = _productservice.UpdateAfterOrder(updatedProd, Allproducts[i].PID);
                _orderproductservice.AddNewProduct_Order(orderID, Allproducts[i].PID, orders[i].quantity);
            }
            //make it a transaction dont update anything until after everything goes through
            var Reciept = new OrderReceiptDTO
            {
                OID = orderID,
                OrderDate = DateTime.Now,
                TotalAmount = TotalAmount,
            };
            return Reciept;
        }
    }
}

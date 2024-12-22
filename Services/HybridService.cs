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
        private readonly IOrderService _orderservice;
        private readonly IReviewService _reviewservice;


        public HybridService(IOrderRepository orderrepo,IProductService productService, IOrder_ProductService order_ProductService, IOrderService orderservice, IReviewService reviewservice)
        {
            _orderrepository = orderrepo;
            _productservice = productService;
            _orderproductservice = order_ProductService;
            _orderservice = orderservice;
            _reviewservice = reviewservice;
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

        public int AddReview(ReviewInDTO review, int userID)
        {
            int prodID = _productservice.GetProductByName(review.ProductName);
            if (prodID == 0 || prodID == null)
            {
                throw new Exception("<!>The product name inputted does not exist<!>");
            }
            var pastOrders = _orderservice.GetMyOrders(userID);
            bool valid = false;
            if (pastOrders != null)
            {
                foreach (var order in pastOrders)
                {
                    var OrdProds = _orderproductservice.GetOrderProdsByOrderID(order.OID);
                    foreach (var ordprod in OrdProds)
                    {
                        if (ordprod.ProductID == prodID)
                        {
                            valid = true;
                        }
                    }
                }
            }
            if (valid)
            {
                var pastReview = _reviewservice.CheckNewProdReview(userID, prodID);
                if (pastReview == null)
                {
                    var NewReview = new Review
                    {
                        UserID = userID,
                        ProductID = prodID,
                        Rating = review.Rating,
                        Comment = review.Comment,
                        ReviewDate = DateTime.Now,
                    };
                    return _reviewservice.AddReview(NewReview);
                }
                else throw new Exception("<!>It looks like you have already made a review on this product before<!>");
            }
            else throw new Exception("<!>It looks like you have not ordered this product, you may only review products you have previously ordered<!>");
        }


        public List<ReviewOutDTO> GetAllReviewsByProdID(int Page, int PageSize, int prodID)
        {
            var userRevs = _reviewservice.GetAllReviewsByProdID(Page, PageSize, prodID);
            var OutReviews = new List<ReviewOutDTO>();
            foreach (var ur in userRevs)
            {
                string name = _productservice.GetProductNameByID(ur.ProductID);
                var review = new ReviewOutDTO
                {
                    RID = ur.RID,
                    UserID = ur.UserID,
                    ProductName = name,
                    Rating = ur.Rating,
                    Comment = ur.Comment,
                    ReviewDate = ur.ReviewDate,
                };
                OutReviews.Add(review);
            }
            return OutReviews;
        }
    }
}

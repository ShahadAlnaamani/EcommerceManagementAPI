using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public interface IProductRepository
    {
        int AddProduct(Product product);
        Product GetProductByID(int ID);
        int GetProductByName(string Name);
        string GetProductNameByID(int ID);
        List<Product> GetProducts(int page, int pageSize);
        List<Product> GetProducts(int page, int pageSize, string cat);
        List<Product> GetProducts(int page, int pageSize, string cat, int rating);
        List<Product> GetProductsByName(int page, int pageSize, string prodName);
        List<Product> GetProductsByRating(int page, int pageSize, int rating);
        bool UpdateAfterOrder(ProductInDTO newprod, int ProdID);
        bool UpdateProduct(ProductInDTO newprod, int ProdID, int AdminID);
        Product GetFullProductsByName(string Name);
        decimal UpdateProductRating(int prodID);
    }
}
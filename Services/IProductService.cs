using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Services
{
    public interface IProductService
    {
        int AddProduct(ProductInDTO product, int AdminID);
        List<Product> CheckFilters(ProductFilterDTO filter);
        List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter);
        Product GetFullProductByID(int ID);
        ProductOutDTO GetProductByID(int ID);
        int GetProductByName(string name);
        bool UpdateAfterOrder(ProductInDTO product, int ID);
        int UpdateProduct(ProductInDTO product, int AdminID);
        string GetProductNameByID(int ID);

    }
}
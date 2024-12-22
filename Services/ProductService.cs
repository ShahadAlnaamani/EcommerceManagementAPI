using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;
using EcommerceManagementAPI.Repositories;

namespace EcommerceManagementAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;

        public ProductService(IProductRepository productrepo)
        {
            _productrepository = productrepo;
        }

        public int AddProduct(ProductInDTO product, int AdminID)
        {
            var NewProd = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Category = product.CategoryName,
                Price = product.Price,
                Stock = product.Stock,
                ProductActive = true,
                Modifiedt = DateTime.Now,
                ModifiedBy = AdminID,
            };
            return _productrepository.AddProduct(NewProd);
        }

        public int UpdateProduct(ProductInDTO product, int AdminID)
        {
            int ProdID = _productrepository.GetProductByName(product.Name);

            if (ProdID != 0 || ProdID != null)
            {
                bool updated = _productrepository.UpdateProduct(product, ProdID, AdminID);
                if (updated) return 0; //successful no errors
                else return 1; //not updated error occured 
            }
            else return 2; //improper category inputted 
        }

        //Add pagination and filtering (order by price, order by category, get by category)
        public List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter)
        {
            var products = CheckFilters(filter);
            var outProds = new List<ProductOutDTO>();

            //Mapping prod -> prodOutDTO
            foreach (var prod in products)
            {
                string CategoryName = " ";
                var output = new ProductOutDTO
                {
                    PID = prod.PID,
                    Name = prod.Name,
                    Price = prod.Price,
                    Description = prod.Description,
                    Category = CategoryName,
                };
                outProds.Add(output);
            }
            return outProds;
        }
        public List<Product> CheckFilters(ProductFilterDTO filter)
        {
            int rating = 0;
            string catName = null;
            string prodName = null;
            var products = new List<Product>();
            if (filter.rating != 0 && filter.rating != null)
            {
                rating = filter.rating;
                if (filter.CategoryName != null)
                {
                    catName = filter.CategoryName;
                    products = _productrepository.GetProducts(filter.Page, filter.PageSize, catName, rating);
                }
                products = _productrepository.GetProductsByRating(filter.Page, filter.PageSize, rating);
            }
            else if (filter.CategoryName != null)
            {
                catName = filter.CategoryName;
                products = _productrepository.GetProducts(filter.Page, filter.PageSize, catName);
            }
            else
            { products = _productrepository.GetProducts(filter.Page, filter.PageSize); }
            return products;
        }

        public ProductOutDTO GetProductByID(int ID)
        {
            var product = _productrepository.GetProductByID(ID);
            var output = new ProductOutDTO
            {
                PID = product.PID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
            };
            return output;
        }


        public int GetProductByName(string name)
        {
            //gets product id by name 
            return _productrepository.GetProductByName(name);
        }

        public Product GetFullProductByID(int ID)
        {
            return _productrepository.GetProductByID(ID);
        }


        public bool UpdateAfterOrder(ProductInDTO product, int ID)
        {
            return _productrepository.UpdateAfterOrder(product, ID);
        }

        public string GetProductNameByID(int ID)
        {
            return _productrepository.GetProductNameByID(ID);
        }

        public Product GetFullProductByName(string name)
        {
            return _productrepository.GetFullProductsByName(name);
        }
    }
}

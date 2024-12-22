﻿using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Adds a new product [returns product id]
        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.PID;
        }

        //Update product details [returns false product not found, true successful update]
        public bool UpdateProduct(ProductInDTO newprod, int ProdID, int AdminID)
        {
            var oldprod = _context.Products.FirstOrDefault(p => p.PID == ProdID);

            //Updating details on the original product with new details
            if (oldprod != null)
            {
                oldprod.Name = newprod.Name;
                oldprod.Description = newprod.Description;
                oldprod.Price = newprod.Price;
                oldprod.Category = newprod.CategoryName;
                oldprod.Stock = newprod.Stock;
                oldprod.ProductActive = true;
                oldprod.Modifiedt = DateTime.Now;
                oldprod.ModifiedBy = AdminID;

                _context.Products.Update(oldprod);
                _context.SaveChanges();
                return true; //Updated successfully
            }

            else return false; //Product not found 
        }

        public bool UpdateAfterOrder(ProductInDTO newprod, int ProdID)
        {
            var oldprod = _context.Products.FirstOrDefault(p => p.PID == ProdID);

            //Updating details on the original product with new details
            if (oldprod != null)
            {
                oldprod.Stock = newprod.Stock;
                oldprod.Modifiedt = DateTime.Now;

                _context.Products.Update(oldprod);
                _context.SaveChanges();
                return true; //Updated successfully
            }

            else return false; //Product not found 
        }

        //[returns new prod rating]
        public decimal UpdateProductRating(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product.Rating; //Updated successfully
        }

        public string GetProductNameByID(int ID)
        {
            var product = GetProductByID(ID);
            return product.Name;
        }

        //------------------------------------Searching products--------------------------------------//

        //Get all products [returns list of products]
        public List<Product> GetProducts(int page, int pageSize)
        {
            int size = pageSize;
            int number = pageSize * page;
            return _context.Products.Where(p => p.ProductActive == true).OrderByDescending(p => p.Name).Skip(number).Take(pageSize).ToList();
        }

        //Gets all products filtering by name 
        public List<Product> GetProductsByName(int page, int pageSize, string prodName)
        {
            int size = pageSize;
            int number = pageSize * page;
            return _context.Products.Where(p => p.ProductActive == true && p.Name.Contains(prodName)).OrderByDescending(p => p.Rating).Skip(number).Take(pageSize).ToList();
        }

        public List<Product> GetProducts(int page, int pageSize, string cat)
        {
            int size = pageSize;
            int number = pageSize * page;
            return _context.Products.Where(p => p.ProductActive == true && p.Category == cat).OrderByDescending(p => p.Rating).Skip(number).Take(pageSize).ToList();
        }

        public List<Product> GetProductsByRating(int page, int pageSize, int rating)
        {
            int size = pageSize;
            int number = pageSize * page;
            return _context.Products.Where(p => p.ProductActive == true && p.Rating == rating).OrderByDescending(p => p.Name).Skip(number).Take(pageSize).ToList();
        }

        public List<Product> GetProducts(int page, int pageSize, string cat, int rating)
        {
            int size = pageSize;
            int number = pageSize * page;
            return _context.Products.Where(p => p.ProductActive == true && p.Category == cat && p.Rating == rating).OrderByDescending(p => p.Name).Skip(number).Take(pageSize).ToList();
        }


        //Search for product by ID 
        public Product GetProductByID(int ID)
        {
            return _context.Products.Where(p => p.PID == ID).FirstOrDefault();
        }

        //Gets product ID by name 
        public int GetProductByName(string Name)
        {
            var prod = _context.Products.Where(p => p.Name == Name).FirstOrDefault();
            return prod.PID;
        }
    }
}

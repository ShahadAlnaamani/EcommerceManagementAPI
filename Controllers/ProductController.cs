using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceManagementAPI.Controllers
{
    [Authorize(Roles = "Admin")]//Ensures that specific functions are only used by admins 
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpPost("ADMIN: AddProduct")]
        public IActionResult AddProduct(ProductInDTO product)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin
            try
            {
                return Ok(_productService.AddProduct(product, int.Parse(userID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("ADMIN: Update product")]
        public IActionResult UpdateProduct(ProductInDTO update)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin
             return Ok(_productService.UpdateProduct(update, int.Parse(userID)));
        }

        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts(int page = 1, int pagesize = 1, int rating = 0, string category = null)
        {
            try
            {
                ProductFilterDTO filters = new ProductFilterDTO
                {
                    Page = page,
                    PageSize = pagesize,
                    rating = rating,
                    CategoryName = category

                };

                return Ok(_productService.GetAllProducts(filters));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetProductByID {ID}")]
        public IActionResult GetProductByID(int ID)
        {
            try
            {
                return Ok(_productService.GetProductByID(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

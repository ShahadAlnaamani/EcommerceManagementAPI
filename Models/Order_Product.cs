using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManagementAPI.Models
{
    [PrimaryKey(nameof(OrderID), nameof(ProductID))]
    public class Order_Product
    {
        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}

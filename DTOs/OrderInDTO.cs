using System.ComponentModel.DataAnnotations;

namespace EcommerceManagementAPI.DTOs
{
    public class OrderInDTO
    {
        [Required]
        public string productName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int quantity { get; set; }
    }
}

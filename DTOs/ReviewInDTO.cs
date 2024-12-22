using System.ComponentModel.DataAnnotations;

namespace EcommerceManagementAPI.DTOs
{
    public class ReviewInDTO
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}

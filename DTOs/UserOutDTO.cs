using System.ComponentModel.DataAnnotations;

namespace EcommerceManagementAPI.DTOs
{
    public class UserOutDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }

        [Required]
        //[RegularExpression(@"^\\+?[1-9][0-9]{7,14}$")]
        public string PhoneNumber { get; set; }
    }
}

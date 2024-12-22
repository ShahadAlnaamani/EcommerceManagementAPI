using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceManagementAPI.DTOs
{
    public class ProductFilterDTO
    {
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int Page { get; set; }

        [Range(1, int.MaxValue)]
        [DefaultValue(1000)]
        public int PageSize { get; set; }
        public string CategoryName { get; set; }

        [Range(1, 5)]
        public int rating { get; set; }
    }
}

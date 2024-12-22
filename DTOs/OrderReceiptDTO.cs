namespace EcommerceManagementAPI.DTOs
{
    public class OrderReceiptDTO
    {
        public int OID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}

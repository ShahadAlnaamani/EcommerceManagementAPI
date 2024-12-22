using EcommerceManagementAPI.DTOs;

namespace EcommerceManagementAPI.Services
{
    public interface IHybridService
    {
        OrderReceiptDTO NewOrder(List<OrderInDTO> orders, int userID);
    }
}
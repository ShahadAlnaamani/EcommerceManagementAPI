using EcommerceManagementAPI.DTOs;

namespace EcommerceManagementAPI.Services
{
    public interface IHybridService
    {
        OrderReceiptDTO NewOrder(List<OrderInDTO> orders, int userID);
        int AddReview(ReviewInDTO review, int userID);
        List<ReviewOutDTO> GetAllReviewsByProdID(int Page, int PageSize, int prodID);
    }
}
using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Services
{
    public interface IReviewService
    {
        int AddReview(Review NewReview);
        Review CheckNewProdReview(int userID, int prodID);
        int DeleteReview(int userID, int ReviewID);
        int UpdateReview(int userID, int reviewID, ReviewInDTO review);
        List<Review> GetAllReviewsByProdID(int Page, int PageSize, int prodID);
        decimal AverageReviewCalculator(int productID);
    }
}
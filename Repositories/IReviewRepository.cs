using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public interface IReviewRepository
    {
        int AddReview(Review review);
        Review CheckNewProdReview(int UserID, int prodID);
        void DeleteReview(Review review);
        List<Review> GetAllReviewsByID(int page, int pageSize, int ProdID);
        Review GetReviewByProductID(int prodID);
        Review GetReviewByRID(int reviewID);
        Review Getspecific(int userID, int prodID);
        void UpdateReview(Review review);
    }
}
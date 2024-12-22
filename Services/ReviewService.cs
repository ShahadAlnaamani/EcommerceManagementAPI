using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;
using EcommerceManagementAPI.Repositories;

namespace EcommerceManagementAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewrepository;
        public ReviewService(IReviewRepository reviewrepo)
        {
            _reviewrepository = reviewrepo;
        }
        public int AddReview(Review NewReview)
        {
            return _reviewrepository.AddReview(NewReview);
        }

        public int DeleteReview(int userID, int ReviewID)
        {
            var review = _reviewrepository.GetReviewByRID(ReviewID);
            if (review.UserID == userID)
            {
                _reviewrepository.DeleteReview(review);
                return 1;
            }
            else throw new Exception("<!>You did not write this review, you can only delete your own reviews<!>");
        }

        public int UpdateReview(int userID, int reviewID, ReviewInDTO review)
        {
            var OriginalReview = _reviewrepository.GetReviewByRID(reviewID);
            if (OriginalReview.UserID == userID)
            {
                OriginalReview.Rating = review.Rating;
                OriginalReview.Comment = review.Comment;
                _reviewrepository.UpdateReview(OriginalReview);
                return 1;
            }
            else throw new Exception("<!>You did not write this review, you can only edit your own reviews<!>");
        }

        public Review CheckNewProdReview(int userID, int prodID)
        {
            return _reviewrepository.CheckNewProdReview(userID, prodID);
        }

        public List<Review> GetAllReviewsByProdID(int Page, int PageSize, int prodID)
        {
            return _reviewrepository.GetAllReviewsByID(Page, PageSize, prodID);
        }

    }
}

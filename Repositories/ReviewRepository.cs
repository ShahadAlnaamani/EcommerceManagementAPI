using EcommerceManagementAPI.Models;

namespace EcommerceManagementAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review.RID;
        }

        public List<Review> GetAllReviewsByID(int page, int pageSize, int ProdID)
        {
            int size = pageSize;
            int number = pageSize * page;
            return _context.Reviews.Where(r => r.RID == ProdID).Skip(number).Take(pageSize).ToList();
        }

        public Review CheckNewProdReview(int UserID, int prodID)
        {
            return _context.Reviews.FirstOrDefault(r => r.UserID == UserID && r.ProductID == prodID);
        }

        public Review GetReviewByRID(int reviewID)
        {
            return _context.Reviews.FirstOrDefault(r => r.RID == reviewID);
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }

        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public Review Getspecific(int userID, int prodID)
        {
            return _context.Reviews.FirstOrDefault(r => r.UserID == userID && r.ProductID == prodID);
        }

        public Review GetReviewByProductID(int prodID)
        {
            return _context.Reviews.FirstOrDefault(r => r.ProductID == prodID);
        }
    }
}

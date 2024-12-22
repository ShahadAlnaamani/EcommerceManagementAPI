using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IHybridService _hybridService;

        public ReviewController(IReviewService reviewService, IHybridService hybridService)
        {
            _reviewService = reviewService;
            _hybridService = hybridService;
        }


        [HttpPost("AddReview")]
        public IActionResult AddReview(ReviewInDTO review)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_hybridService.AddReview(review, int.Parse(userID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllReviewsByProduct")]
        public IActionResult GetProductReviews(int productID, int PageSize = 0, int page = 100)
        {
            try
            {
                return Ok(_reviewService.GetAllReviewsByProdID(PageSize, page, productID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Update Review")]
        public IActionResult UpdateReview(int reviewID, ReviewInDTO newRev)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  

            try
            {
                return Ok(_reviewService.UpdateReview(int.Parse(userID), reviewID, newRev));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete Review")]
        public IActionResult DeleteReview(int reviewID)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  

            try
            {
                return Ok(_reviewService.DeleteReview(int.Parse(userID), reviewID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

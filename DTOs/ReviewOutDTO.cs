﻿namespace EcommerceManagementAPI.DTOs
{
    public class ReviewOutDTO
    {
        public int RID { get; set; }

        public int UserID { get; set; }

        public string ProductName { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}

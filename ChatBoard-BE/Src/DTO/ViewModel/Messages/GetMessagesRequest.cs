using System;
namespace DTO.ViewModel.Messages
{
    public class GetMessagesRequest
    {
        //public long UserId { get; set; } // Logged-in user ID
        public long OtherUserId { get; set; } // Other user's ID
        public int PageNumber { get; set; } // Current page number
        public int PageSize { get; set; } // Items per page
    }

}


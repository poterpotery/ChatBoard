using System;
namespace DTO.ViewModel.Messages
{
    public class MessageDto
    {
        public long MessageId { get; set; }
        public string Content { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsMine { get; set; } // True if message is sent by the current user
    }

}


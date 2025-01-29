using System;
namespace DTO.Model
{
	public class Messages: CommonDbProps
	{
        public long SenderId { get; set; } // UserId of the sender
        public long ReceiverId { get; set; } // UserId of the receiver
        public string Content { get; set; } // Message text/content
        public DateTime SentAt { get; set; } // Timestamp of when the message was sent
        public bool IsRead { get; set; } // Whether the message has been read by the receiver
    }
}


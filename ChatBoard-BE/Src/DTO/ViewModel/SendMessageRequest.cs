using System;
namespace DTO.ViewModel
{
    public class SendMessageRequest
    {
        public long ReceiverId { get; set; }
        public string Content { get; set; }
    }

}


using System;
using System.ComponentModel.DataAnnotations.Schema;
using DTO.Enums;

namespace DTO.Model
{
    public class AccountConfirmation: CommonDbProps
    {
        public AccountConfirmation()
        {
        }
        public string Code { get; set; }
        public DateTime? ExpireAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedAt { get; set; }
        public EAccountConfirmationType Type { get; set; }
        public long? AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}


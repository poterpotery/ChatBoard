using System;
using System.Collections.Generic;

namespace DTO.ViewModel.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string HashAccountId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Location { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileBannerImage { get; set; }
        public string Bio { get; set; }
        public string AccountType { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPrivacy { get; set; }
        public bool IsVerifiedAccount { get; set; }
        public string kycStatus { get; set; }
        public bool IsExistKyc { get; set; }
        public AccountBlockEnum UserBlockedStatus { get; set; }
    }
}

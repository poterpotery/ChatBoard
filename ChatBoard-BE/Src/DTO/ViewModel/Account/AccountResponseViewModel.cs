using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Account
{
    public class AccountResponseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Location { get; set; }
        public string ProfileImage { get; set; }
        public string BannerThumbnail {  get; set; }
        public string ProfileBannerImage { get; set; }
        public string Bio { get; set; }
        public string AccountType { get; set; }
        public bool IsBlock { get; set; }
        public bool IsEmailVerified { get; set; }
        public string KycStatus {  get; set; }
        public bool IsPrivacy { get; set; }
        public bool IsVerifiedAccount { get; set; }
        public bool IsNotificationEnabled { get; set; } 
        public bool IsMessageNotificationEnabled { get; set; }
        public string Degree {  get; set; }
        public List<DegreeResponse> DegreeList {  get; set; }
    }
    public class DegreeResponse {
        public string DegreeName { get; set; }
        public string DegreeFile { get; set; }
    }

}

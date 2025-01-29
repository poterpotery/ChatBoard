using System;
using System.Collections.Generic;
using DTO.Enums;

namespace DTO.Model
{
    public partial class Account: CommonDbProps
    {
        public Account()
        {
            AccountConfirmations = new HashSet<AccountConfirmation>();
            DeviceActivities = new HashSet<DeviceActivity>();
            UserDevices = new HashSet<UserDevice>();


        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EGenderType EGender { get; set; }
        public DateTime? DOB { get; set; }

        public bool IsEmailVerified { get; set; }
        public bool IsVerifiedAccount { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }

        public string ProfileImage { get; set; }
        public string ProfileBannerImage { get; set; }
        public string Bio { get; set; }

        public EAccountType AccountType { get; set; }
        public virtual ICollection<AccountConfirmation> AccountConfirmations { get; set; }
        public virtual ICollection<DeviceActivity> DeviceActivities { get; set; }
        public virtual ICollection<UserDevice> UserDevices { get; set; }
    }
}
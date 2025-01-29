using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Account
{
    public class UpdatePrivacyRequest
    {
        public bool IsPrivacy { get; set; }
        public EGenderType? gender { get; set; } = EGenderType.Male;
        public string FirstName { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using DTO.Enums;

namespace DTO.ViewModel.Account
{
    public class AccountSignUpRequestModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [StringLength(100, ErrorMessage = "Email length cannot exceed 100 characters.")]
        [RegularExpression(@"^\S+@\S+.\S+$", ErrorMessage = "Invalid email format. The email should contain a valid domain, e.g., example@example.com.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", ErrorMessage = "Password must meet the requirements.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(EGenderType), ErrorMessage = "Invalid gender.")]
        public EGenderType Gender { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Privacy check is required.")]
        public bool IsPrivacyCheck { get; set; }
        public EAccountType AccountType { get; set; }
    }

}
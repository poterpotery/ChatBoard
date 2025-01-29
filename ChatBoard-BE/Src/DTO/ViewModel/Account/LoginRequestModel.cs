using DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.ViewModel.Account
{
	public class LoginRequestModel
	{
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email length cannot exceed 100 characters.")]
        [RegularExpression(@"^\S+@\S+.\S+$", ErrorMessage = "Invalid email format. The email should contain a valid domain, e.g., example@example.com.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public EAccountType AccountType { get; set; }
    }
}


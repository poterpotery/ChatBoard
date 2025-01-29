using System.ComponentModel.DataAnnotations;
using DTO.Enums;

namespace DTO.ViewModel.Account
{
    public class ForgotPasswordRequestModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email length cannot exceed 100 characters.")]
        public string Email { get; set; }
        public EAccountType AccountType { get; set; }
    }
}

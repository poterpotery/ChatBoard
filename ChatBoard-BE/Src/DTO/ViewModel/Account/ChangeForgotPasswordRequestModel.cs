using System.ComponentModel.DataAnnotations;

namespace DTO.ViewModel.Account
{
    public class ChangeForgotPasswordRequestModel
    {
        public string Token { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", ErrorMessage = "Password must meet the requirements.")]
        public string NewPassword { get; set; }
    }
}

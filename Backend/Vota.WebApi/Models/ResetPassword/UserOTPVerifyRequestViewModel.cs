using System.ComponentModel.DataAnnotations;

namespace Vota.WebApi.Models.ResetPassword
{
    /// <summary>
    /// User otp verify request model.
    /// </summary>
    public class UserOTPVerifyRequestViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// OTP
        /// </summary>
        [Required(ErrorMessage = "OTP is required")]
        public string OTP { get; set; }
    }
}

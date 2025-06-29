namespace Vota.WebApi.Models.ResetPassword
{
    /// <summary>
    /// User otp verify rsponse model.
    /// </summary>
    public class UserOTPVerifyResponseViewModel
    {
        /// <summary>
        /// User identifire.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Reset token
        /// </summary>
        public string ResetToken { get; set; }
    }
}

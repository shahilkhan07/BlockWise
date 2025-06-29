using System.ComponentModel.DataAnnotations;

namespace Vota.WebApi.Models.Auth
{
    /// <summary>
    /// Reset password request view model
    /// </summary>
    public class ResetPasswordRequestViewModel
    {
        /// <summary>
        /// Phone number or email or username.
        /// </summary>
        [Required(ErrorMessage = "Phone number or username or email required!")]
        public string Identifier{ get; set; }

        /// <summary>
        /// Old password 
        /// </summary>
        [Required(ErrorMessage = "Old password required!")]
        [DataType(DataType.Password)]
        public string OldPassword{ get; set; }

        /// <summary>
        /// New pasword
        /// </summary>
        [Required(ErrorMessage = "New password required!")]
        [DataType(DataType.Password)]
        public string NewPassword{ get; set; }
    }
}

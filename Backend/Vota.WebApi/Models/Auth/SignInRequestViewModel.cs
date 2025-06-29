using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vota.WebApi.Models.Auth
{
    /// <summary>
    /// Sign in request view model
    /// </summary>
    public class SignInRequestViewModel
    {
        /// <summary>
        /// Email or Phone number or username
        /// </summary>
        [Required(ErrorMessage = "Identifier is required!")]
        [DataType(DataType.EmailAddress)]
        public string Identifier { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}

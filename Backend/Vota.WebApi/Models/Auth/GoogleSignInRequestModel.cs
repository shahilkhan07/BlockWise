using System.ComponentModel.DataAnnotations;

namespace Vota.WebApi.Models.Auth
{
    /// <summary>
    /// Google sign in request model
    /// </summary>
    public class GoogleSignInRequestModel
    {
        /// <summary>
        /// Id token
        /// </summary>
        [Required(ErrorMessage = "Google Id Token is required")]
        public string IdToken { get; set; } = string.Empty;
    }
}

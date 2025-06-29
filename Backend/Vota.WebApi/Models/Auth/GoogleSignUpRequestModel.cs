using System.ComponentModel.DataAnnotations;

namespace Vota.WebApi.Models.Auth
{
    /// <summary>
    /// Google sign up request model
    /// </summary>
    public class GoogleSignUpRequestModel
    {
        /// <summary>
        /// Id Token
        /// </summary>
        [Required(ErrorMessage = "Google Id Token is required")]
        public string IdToken { get; set; } = string.Empty;

        /// <summary>
        /// First name
        /// </summary>
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last Name
        /// </summary>
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        ///// <summary>
        ///// Email address
        ///// </summary>
        //[Required(ErrorMessage = "Email is required")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Zip code
        /// </summary>
        public string ZipCode { get; set; } = string.Empty;
    }
}

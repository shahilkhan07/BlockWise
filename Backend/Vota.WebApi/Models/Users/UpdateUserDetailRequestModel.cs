using Microsoft.AspNetCore.Http;

namespace Vota.WebApi.Models.Users
{
    /// <summary>
    /// Update user detail request model
    /// </summary>
    public class UpdateUserDetailRequestModel
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Zip code
        /// </summary>
        public string ZipCode { get; set; } = string.Empty;

        /// <summary>
        /// Profile picture
        /// </summary>
        public IFormFile ProfilePic { get; set; }
    }
}

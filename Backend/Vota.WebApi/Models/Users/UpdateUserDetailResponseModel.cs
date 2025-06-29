namespace Vota.WebApi.Models.Users
{
    /// <summary>
    /// Update user response model.
    /// </summary>
    public class UpdateUserDetailResponseModel
    {
        /// <summary>
        /// User identity
        /// </summary>
        public int UserId { get; set; }

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
        public string ProfilePic { get; set; }
    }
}

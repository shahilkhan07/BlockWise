using System;

namespace Vota.WebApi.Models.Auth
{
    /// <summary>
    /// Sign in response view model
    /// </summary>
    public class SignInResponseViewModel
    {
        /// <summary>
        /// User identifire.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Expiration
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Created date.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}

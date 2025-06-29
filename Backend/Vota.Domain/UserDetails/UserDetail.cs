using Microsoft.AspNetCore.Identity;
using Vota.Domain.Base;
using Vota.Domain.Users;

namespace Vota.Domain.UserDetails
{
    public class UserDetail : BaseModel
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Profile bio.
        /// </summary>
        public string ProfileBio { get; set; }
        ///summary
        /// Profile image url.
        /// </summary>
        public string ProfileImageUrl { get; set; }
        /// <summary>
        /// User identifier.
        /// </summary>
        public int UserId { get; set; }
        public virtual IdentityUser<int> User { get; set; }
    }
}

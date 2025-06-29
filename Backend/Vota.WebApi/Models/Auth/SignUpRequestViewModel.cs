using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vota.Domain.Enums;
using Vota.Domain.Roles;

namespace Vota.WebApi.Models.Auth
{
    /// <summary>
    /// User sign up request model
    /// </summary>
    public class SignUpRequestViewModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required(ErrorMessage = "User name is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "User name must be alphanumeric (letters and numbers only).")]
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Role enum
        /// </summary>
        [DefaultValue(RoleEnum.User)]
        public RoleEnum Role { get; set; } = RoleEnum.User;
    }
}

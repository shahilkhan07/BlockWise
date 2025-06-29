using System.ComponentModel;

namespace Vota.Domain.Users
{
    /// <summary>
    /// User query
    /// </summary>
    public class UserQuery
    {
        /// <summary>
        /// Search string
        /// </summary>
        [DefaultValue("")]
        public string SearchString { get; set; } = "";

        /// <summary>
        /// Is active
        /// </summary>
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Page number
        /// </summary>
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}

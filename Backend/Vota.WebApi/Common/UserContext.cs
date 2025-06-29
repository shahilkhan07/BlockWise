namespace Vota.WebApi.Common
{
    /// <summary>
    /// User context.
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// User identity
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Role { get; set; }
    }

}

using Vota.Domain.Base;
using System.Threading.Tasks;

namespace Vota.Domain.UserDetails
{
    /// <summary>
    /// User detail service interface
    /// </summary>
    public interface IUserDetailService : IBaseService<UserDetail>
    {

        /// <summary>
        /// Get user by identifier
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <returns>UserDetail</returns>
        public Task<UserDetail> GetUserByIdentifier(string identifier);

        /// <summary>
        /// Get user exists with any of these identifier.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userName">User name</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>UserDetail</returns>
        public Task<UserDetail> GetUserByIdentifier(string email, string phoneNumber);

        /// <summary>
        /// Get user details by user identity
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>UserDetail</returns>
        public Task<UserDetail> GetUserDetails(int userId);

        /// <summary>
        /// Add user role.
        /// </summary>
        /// <param name="userId">User identity.</param>
        /// <param name="roleId">Role identity.</param>
        /// <returns>Mesaage</returns>
        public Task<string> UpsertUserRole(int userId, int roleId);

    }
}

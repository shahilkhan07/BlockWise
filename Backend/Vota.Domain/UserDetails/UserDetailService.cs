using Microsoft.AspNetCore.Identity;
using Vota.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vota.Domain.UserDetails
{
    /// <summary>
    /// User detail service
    /// </summary>
    public class UserDetailService : BaseService<UserDetail>, IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;

        /// <summary>
        /// Creates user detail service
        /// </summary>
        /// <param name="userDetailRepository">User detail repository</param>
        public UserDetailService(IUserDetailRepository userDetailRepository) : base(userDetailRepository)
        {
            _userDetailRepository = userDetailRepository;
        }

        /// <summary>
        /// Get user by identifier
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <returns>User</returns>
        public async Task<UserDetail> GetUserByIdentifier(string identifier)
        {
            return await _userDetailRepository.GetUserByIdentifier(identifier);
        }

        /// <summary>
        /// Get user exists with any of these identifier.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userName">User name</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>UserDetail</returns>
        public async Task<UserDetail> GetUserByIdentifier(string email, string phoneNumber)
        {
            return await _userDetailRepository.GetUserByIdentifier(email, phoneNumber);
        }

        /// <summary>
        /// Get user details by user identity
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>UserDetail</returns>
        public async Task<UserDetail> GetUserDetails(int userId)
        {
            return await _userDetailRepository.GetUserDetails(userId);
        }

        /// <summary>
        /// Add user role.
        /// </summary>
        /// <param name="userId">User identity.</param>
        /// <param name="roleId">Role identity.</param>
        /// <returns>Mesaage</returns>
        public async Task<string> UpsertUserRole(int userId, int roleId)
        {
            return await _userDetailRepository.UpsertUserRole(userId, roleId);
        }
    }
}

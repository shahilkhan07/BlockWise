using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vota.Domain.UserDetails;
using Vota.EF.Base;

namespace Vota.EF.Repositories
{
    /// <summary>
    /// User detail repository.
    /// </summary>
    public class UserDetailRepository : BaseRepository<UserDetail, VotaDbContext>, IUserDetailRepository
    {
        private readonly VotaDbContext _context;

        /// <summary>
        /// Creates user detail repository. 
        /// </summary>
        /// <param name="context">context</param>
        public UserDetailRepository(VotaDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get user by identifier
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <returns>User</returns>
        public async Task<UserDetail> GetUserByIdentifier(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return null;

            identifier = identifier.ToLower();
            var query = GetQuery();
            var result = await query
            .FirstOrDefaultAsync(x =>
                    (x.User != null && !string.IsNullOrEmpty(x.User.Email) && x.User.Email.ToLower() == identifier) ||
                    (x.User != null && !string.IsNullOrEmpty(x.User.UserName) && x.User.UserName.ToLower() == identifier) ||
                    (x.User != null && !string.IsNullOrEmpty(x.User.PhoneNumber) && x.User.PhoneNumber == identifier)
                );
            return result;
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
            //string userNameLower = userName.ToLower();
            string emailLower = email.ToLower();

            var query = _context.UserDetails    
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Include(x => x.User)
                .Where(x =>
                    (emailLower != null && x.User.Email == emailLower) ||
                    //(userNameLower != null && x.User.UserName.ToLower() == userNameLower) ||
                    (phoneNumber != null && x.User.PhoneNumber == phoneNumber)
                );

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get user details by user identity
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>UserDetail</returns>
        public async Task<UserDetail> GetUserDetails(int userId)
        {
            return await _context.UserDetails.AsNoTracking().Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        /// <summary>
        /// Add user role.
        /// </summary>
        /// <param name="userId">User identity.</param>
        /// <param name="roleId">Role identity.</param>
        /// <returns>Mesaage</returns>
        public async Task<string> UpsertUserRole(int userId, int roleId)
        {
            var existingUserRoles = await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
            if (existingUserRoles.Any(ur => ur.RoleId == roleId))
            {
                return "User role already existed!";
            }
            if (existingUserRoles != null && existingUserRoles.Count() > 0)
            {
                _context.UserRoles.RemoveRange(existingUserRoles);
            }
            var addUserRole = new IdentityUserRole<int>() { UserId = userId, RoleId = roleId };
            await _context.UserRoles.AddAsync(addUserRole);
            await _context.SaveChangesAsync();
            return existingUserRoles != null && existingUserRoles.Count() > 0 ? "User role updated!" : "User role created successfully!";
        }

        protected override IQueryable<UserDetail> GetQuery()
        {
            return _context.UserDetails
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.IsActive);
        }

        //Donot remove the below code GetQuery() for future use.

        //protected override IQueryable<UserDetail> GetQuery()
        //{
        //    //var aspUserDictionary = _context.Users.AsNoTracking().ToDictionary(aspUser => aspUser.Id);

        //    var query =
        //        _context.UserDetails
        //        .AsNoTracking()
        //        .Where(x => x.IsActive == true)
        //        .Include(x => x.AspnetUser)
        //        .Select(user => new UserDetail
        //        {
        //            Id = user.Id,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            CreatedDate = user.CreatedDate,
        //            ModifiedDate = user.ModifiedDate,
        //            IsActive = user.IsActive,
        //            AspnetUser =  
        //                        new ApplicationUser
        //                        {
        //                            Id = user.AspnetUser.Id,
        //                            UserName = user.AspnetUser.UserName,
        //                            NormalizedUserName = user.AspnetUser.NormalizedUserName,
        //                            Email = user.AspnetUser.Email,
        //                            NormalizedEmail = user.AspnetUser.NormalizedEmail,
        //                            EmailConfirmed = user.AspnetUser.EmailConfirmed,
        //                            PasswordHash = user.AspnetUser.PasswordHash,
        //                            SecurityStamp = user.AspnetUser.SecurityStamp,
        //                            ConcurrencyStamp = user.AspnetUser.ConcurrencyStamp,
        //                            PhoneNumber = user.AspnetUser.PhoneNumber,
        //                            PhoneNumberConfirmed = user.AspnetUser.PhoneNumberConfirmed,
        //                            TwoFactorEnabled = user.AspnetUser.TwoFactorEnabled,
        //                            LockoutEnd = user.AspnetUser.LockoutEnd,
        //                            LockoutEnabled = user.AspnetUser.LockoutEnabled,
        //                            AccessFailedCount = user.AspnetUser.AccessFailedCount
        //                        } 
        //        });

        //    return query;
        //}

    }

}

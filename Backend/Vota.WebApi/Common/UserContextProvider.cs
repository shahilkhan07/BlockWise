using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Vota.WebApi.Common
{
    /// <summary>
    /// User context provider
    /// </summary>
    public class UserContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserContext _userContext;

        /// <summary>
        /// User context provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userContext"></param>
        public UserContextProvider(IHttpContextAccessor httpContextAccessor, UserContext userContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;

            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    _userContext.UserId = userId;
                }

                _userContext.Role = user.FindFirst(ClaimTypes.Role)?.Value;
            }
        }
    }

}

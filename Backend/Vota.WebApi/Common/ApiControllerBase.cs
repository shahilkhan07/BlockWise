using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Vota.WebApi.Common
{
    /// <summary>
    /// Controller base.
    /// </summary>
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private string _userId;
        private string _role;

        /// <summary>
        /// User identity
        /// </summary>
        protected int? UserId
        {
            get
            {
                if (!string.IsNullOrEmpty(_userId) && int.TryParse(_userId, out int cachedUserId))
                {
                    return cachedUserId;
                }

                var userIdStr = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
                {
                    _userId = userIdStr;
                    return userId;
                }

                _userId = null;
                return null;
            }
        }

        /// <summary>
        /// Role
        /// </summary>
        protected string Role
        {
            get
            {
                if (string.IsNullOrEmpty(_role))
                {
                    var roleStr = User.FindFirst(ClaimTypes.Role)?.Value;
                    if (string.IsNullOrEmpty(roleStr))
                    {
                        _role = null;
                    }
                    else
                    {
                        _role = roleStr;
                    }
                }
                return _role;
            }
        }
    }
}

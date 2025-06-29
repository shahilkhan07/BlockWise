using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vota.Domain.UserDetails;
using Vota.WebApi.Common;
using Vota.WebApi.Models.Users;
using System.Threading.Tasks;

namespace Vota.WebApi.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [ApiController]
    [Route("/v{Version:ApiVersion}/[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;

        /// <summary>
        /// User controller constructor 
        /// </summary>
        /// <param name="userDetailService">User detail service</param>
        /// <param name="userManager">User manager</param>
        /// <param name="mapper">Mapper</param>
        public UserController(
            IUserDetailService userDetailService,
            UserManager<IdentityUser<int>> userManager,
            IMapper mapper
            )
        {
            _userDetailService = userDetailService;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Update user details 
        /// </summary>
        /// <param name="requestModel">Request model</param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException">Business logiv exception</exception>
        /// <exception cref="ResourceNotFoundException">Resource not found exception</exception>
        /// <exception cref="IdentityValidationException">Identity validation exception.</exception>



        //Future use: uncomment this for update user details endpoint

        //[Authorize]
        //[HttpPut]
        //[ProducesResponseType(typeof(UpdateUserDetailRequestModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> UpdateUserDetails([FromForm] UpdateUserDetailRequestModel requestModel)
        //{
        //    if (requestModel == null)
        //        throw new BusinessLogicException("Request model is empty!");

        //    var user = await _userManager.FindByIdAsync(UserId.ToString()) ??
        //        throw new ResourceNotFoundException("User not found");

        //    var userDetails = await _userDetailService.GetUserDetails((int)UserId)
        //            ?? throw new ResourceNotFoundException("User details not found");

        //    bool userDetailsUpdated = false;


        //    if (requestModel.FirstName is { Length: > 0 } && requestModel.FirstName.ToLower() != userDetails.FirstName.ToLower()) 
        //    { 
        //        userDetails.FirstName = requestModel.FirstName; 
        //        userDetailsUpdated = true; 
        //    }
        //    if (requestModel.LastName is { Length: > 0 } && requestModel.LastName.ToLower() != userDetails.LastName.ToLower()) 
        //    { 
        //        userDetails.LastName = requestModel.LastName; 
        //        userDetailsUpdated = true; 
        //    }

        //    if (requestModel.PhoneNumber is { Length: > 0 } && requestModel.PhoneNumber.ToLower() != userDetails.User.PhoneNumber.ToLower()) 
        //    { 
        //        userDetails.User.PhoneNumber = requestModel.PhoneNumber; 
        //        userDetailsUpdated = true; 

        //    }

        //    if (requestModel.Email is { Length: > 0 } && requestModel.Email.ToLower() != userDetails.User.Email.ToLower()) 
        //    { 
        //        userDetails.User.Email = requestModel.Email;
        //        userDetails.User.NormalizedEmail = requestModel.Email.ToUpper();
        //        userDetailsUpdated = true; 
        //    }

        //    string url = null;

        //    if (requestModel.ProfilePic?.Length > 0)
        //    {
        //        // Generate URL and update in DB (if needed)
        //        //url = "";
        //        //userDetailsUpdated = true;
        //    }

        //    // Only update if any details are changed
        //    if (userDetailsUpdated)
        //        await _userDetailService.ChangeAsync(userDetails);

        //    var response = new UpdateUserDetailResponseModel()
        //    {
        //        UserId = user.Id,
        //        FirstName = userDetails.FirstName,
        //        LastName = userDetails.LastName,
        //        Email = userDetails.User.Email,
        //        PhoneNumber = userDetails.User.PhoneNumber,
        //        ProfilePic = url ?? ""
        //    };
        //    return Ok(response);
        //}
    }
}
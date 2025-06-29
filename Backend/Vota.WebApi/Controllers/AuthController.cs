using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Vota.Domain.Roles;
using Vota.Domain.UserDetails;
using Vota.WebApi.Common;
using Vota.WebApi.Models.Auth;
using Vota.WebApi.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vota.Domain.Enums;

namespace Vota.WebApi.Controllers
{
    /// <summary>
    /// Auth controller.
    /// </summary>
    [ApiController]
    [Route("/v{version:ApiVersion}/[controller]")]

    public class AuthController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserDetailService _userDetailService;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly CryptoUtils _cryptoUtils;


        /// <summary>
        /// Creates auth controller.
        /// </summary>
        /// <param name="mapper">mapper</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="userDetailService">User detail service</param>
        /// <param name="userManager">User manager</param>
        /// <param name="cryptoUtils">Crypto utils</param>
        public AuthController(
        
            IMapper mapper,
            IConfiguration configuration,
            IUserDetailService userDetailService,
            UserManager<IdentityUser<int>> userManager,
            CryptoUtils cryptoUtils
        )
        {
            _mapper = mapper;
            _configuration = configuration;
            _userDetailService = userDetailService;
            _userManager = userManager;
            _cryptoUtils = cryptoUtils;
            
        }

        /// <summary>
        /// Sign up account.
        /// </summary>
        /// <param name="model">Sign up request view model</param>
        /// <returns>SignInResponseViewModel</returns>
        [HttpPost("signUp")]
        [ProducesResponseType(typeof(SignInResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp(SignUpRequestViewModel model)
        {
            if (model == null)
                throw new BusinessLogicException("Request model is empty!");

            // Check for existing user
            await ValidateUserDoesNotExist(model);

            // Create user account
            var user = CreateIdentityUser(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new IdentityValidationException("User creation failed!", result.Errors.Select(e => e.Description));

            // Assign role and create profile

            int roleId = 0;
            if (model.Role == RoleEnum.Admin)
                roleId = 1;
            else if (model.Role == RoleEnum.Influencer)
                roleId = 2;
            else
                roleId = 3;

                await _userDetailService.UpsertUserRole(user.Id, roleId);

            string logoUrl = "";
            DateTime createdAt;
            
            var userDetails = await HandleUserProfile(user.Id, model);
            createdAt = userDetails.CreatedAt;

            // Generate token and create response
            var token = GenerateAuthToken(user, model, roleId);

            return Ok(new SignInResponseViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,

                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RoleName = GetRoleName(roleId),
                CreatedAt = createdAt,
            });
        }

        /// <summary>
        /// Sign in account.
        /// </summary>
        /// <param name="signInRequestViewModel">Sign in request view model</param>
        /// <returns>Sign in response view model</returns>
        [HttpPost("signIn")]
        [ProducesResponseType(typeof(SignInResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> SignIn(SignInRequestViewModel signInRequestViewModel)
        {
            
            if (signInRequestViewModel == null)
                throw new BusinessLogicException("Request model is empty!");

            var user = await _userDetailService.GetUserByIdentifier(signInRequestViewModel.Identifier);   

            if (user == null)
                throw new UnauthorizedResourceAccessException("Invalid email, phone number, or username!");

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user.User, signInRequestViewModel.Password);

            if (!isPasswordCorrect)
                throw new UnauthorizedResourceAccessException("Incorrect password!");

            return Ok(await GenerateAuthResponse(user));
        }


        /// <summary>
        /// 
        /// Reset new password while login with old password.
        /// 
        /// </summary>
        /// <param name="resetPasswordRequestViewModel">Reset password request view model</param>
        /// <returns>ResponseViewModel</returns>
        [Authorize]
        [HttpPut("resetPassword")]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestViewModel resetPasswordRequestViewModel)
        {
            if (resetPasswordRequestViewModel == null)
                throw new BusinessLogicException("Request model is empty!");

            var userDetails = await _userDetailService.GetUserByIdentifier(resetPasswordRequestViewModel.Identifier);
            if (userDetails == null)
                throw new ResourceNotFoundException("Please enter valid email or phone number or username!");

            bool isValidPassword = await _userManager.CheckPasswordAsync(userDetails.User, resetPasswordRequestViewModel.OldPassword);
            if (!isValidPassword)
                throw new UnauthorizedAccessException("Please enter valid old password");

            var changePasswordResult = await _userManager.ChangePasswordAsync(userDetails.User, resetPasswordRequestViewModel.OldPassword, resetPasswordRequestViewModel.NewPassword);
            if (!changePasswordResult.Succeeded)
                throw new IdentityValidationException(
                    "Password reset failed!",
                    changePasswordResult.Errors.Select(e => e.Description)
                );

            return Ok(new ResponseViewModel
            {
                Code = HttpStatusCode.OK,
                Message = "Password reset successful!"
            });
        }

        private async Task ValidateUserDoesNotExist(SignUpRequestViewModel model)
        {
            var existingUser = await _userDetailService.GetUserByIdentifier(model.Email);
            if (existingUser != null)
            {
                if (!string.IsNullOrEmpty(model.Email) && existingUser.User.Email == model.Email)
                    throw new DuplicateResourceException("User exists with same email!");

                //if (existingUser.User.PhoneNumber == model.PhoneNumber)
                //    throw new DuplicateResourceException("User exists with same phone number!");
            }
        }
        private IdentityUser<int> CreateIdentityUser(SignUpRequestViewModel model)
        {
            return new IdentityUser<int>
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };
        }
        
        private async Task<UserDetail> HandleUserProfile(int userId, SignUpRequestViewModel model)
        {
            var userDetail = new UserDetail
            {
                UserId = userId,
            };
            return await _userDetailService.AddAsync(userDetail);
        }
        private JwtSecurityToken GenerateAuthToken(IdentityUser<int> user, SignUpRequestViewModel model, int roleId)
        {
            string roleName = GetRoleName(roleId);
            var authClaims = SharedUtils.GetTokenClaims(
                model.Email,
                user.Id,
                roleName
            );

            return SharedUtils.GetJWTToken(
                authClaims,
                _configuration["JWT:Secret"],
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                _configuration["JWT:JWTExpiryDays"]
            );
        }
        private async Task<SignInResponseViewModel> GenerateAuthResponse(dynamic user)
        {
            List<string> roles = await _userManager.GetRolesAsync(user.User);
            var authClaims = SharedUtils.GetTokenClaims(user.User.Email, user.UserId, roles.FirstOrDefault());

            var token = SharedUtils.GetJWTToken(
                authClaims,
                _configuration["JWT:Secret"],
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                _configuration["JWT:JWTExpiryDays"]
            );

            var viewModel = _mapper.Map<SignInResponseViewModel>(user);
            viewModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            viewModel.Expiration = token.ValidTo;
            viewModel.RoleName = roles.FirstOrDefault();
            viewModel.CreatedAt = user.CreatedAt;
            return viewModel;
        }
        private static string GetRoleName(int id)
        {
            switch (id)
            {
                case var _ when id == RoleConst.Admin:
                    return "Admin";

                case var _ when id == RoleConst.Influencer:
                    return "Influencer";

                default:
                    return "User";
            }
        }

    }
}

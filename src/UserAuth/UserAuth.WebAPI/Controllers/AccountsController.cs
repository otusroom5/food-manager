using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserAuth.WebApi.Utils;
using UserAuth.WebApi.Contracts;
using UserAuth.WebApi.Extensions;
using UserAuth.BusinessLogic.Abstractions;
using UserAuth.BusinessLogic.Dto;
using UserAuth.WebApi.Exceptions;
using UserAuth.WebApi.Models;

namespace UserAuth.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IUserVerificationService _userService;
        private readonly Options.AuthenticationOptions _options;
        public AccountsController(ILogger<AccountsController> logger,
            IUserVerificationService userService,
            IOptions<Options.AuthenticationOptions> options)
        {
            _logger = logger;
            _options = options.Value;
            _userService = userService;
        }

        /// <summary>
        /// Verify user and return response jwt token
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Token, Role or error in message property</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/v1/Accounts/Login
        ///     {
        ///        "UserName": "test",
        ///        "HashedPassword": "hashed_password",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Token is generated</response>
        /// <response code="400">If the user is not valid</response>
        [HttpPost("Login")]
        public IActionResult Login(UserLoginModel userModel)
        {
            _logger.LogTrace($"Attempt login {userModel.UserName}");

            try
            {
                if (!_userService.TryVerifyUser(userModel.UserName, userModel.HashedPassword, out VerifiedUserDto? user))
                {
                    throw new NotValidUserException($"{user.UserName} is not valid");
                }

                if (user is null)
                {
                    throw new NullReferenceException($"{userModel.UserName} is null");
                }


                string token = JwtTokenUtils.GenerateToken(_options, user.UserName, user.Role);

                _logger.LogDebug($"Generated token: {token}");
                
                return Ok(new LoginActionResponse()
                {
                    Token = token,
                    Role = user.Role.ConvertToString(),
                    Message = String.Empty
                });
            } 
            catch (Exception ex) 
            {
                return BadRequest(new LoginActionResponse()
                {
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Change password of;'\ ' user by username
        /// </summary>
        /// <param name="userModel"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/v1/Accounts/ChangePassword
        ///     {
        ///        "UserName": "test",
        ///        "HashedPassword": "hashed_password",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Password is changed</response>
        /// <response code="400">If password is not changed then return error</response>
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(UserLoginModel userModel)
        {
            return Ok();
        }
    }
}

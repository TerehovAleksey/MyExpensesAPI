using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyExpensesAPI.Attributes;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Infrastructure;
using MyExpensesAPI.Models;
using MyExpensesAPI.Models.Account;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyExpensesAPI.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route(WebApiRoutes.API_ACCOUNT)]
    public class AccountController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager,
            SignInManager<User> signInManager, IJwtAuthManager jwtAuthManager, IEmailSender emailSender)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthManager = jwtAuthManager;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var status = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            if (status.RequiresTwoFactor)
            {
                return Unauthorized(ErrorDetails.Create(401, "Необходимо подверждение"));
            }

            if (status.IsLockedOut)
            {
                return Unauthorized(ErrorDetails.Create(401, "Заблокирован"));
            }

            if (status.IsNotAllowed)
            {
                return Unauthorized(ErrorDetails.Create(401, "Не разрешено"));
            }

            if (status.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var claims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var jwtResult = _jwtAuthManager.GenerateTokens(user.UserName, claims.ToArray(), DateTime.Now);
                _logger.LogInformation($"User [{user.UserName}] logged in the system.");

                return Ok(new LoginResult(user.UserName, roles.First(), jwtResult.AccessToken, jwtResult.RefreshToken.TokenString));
            }

            return Unauthorized(ErrorDetails.Create(401, "Не найдено"));
        }

        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCurrentUser()
        {
            return Ok(new LoginResult(User.Identity.Name, User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                string.Empty, string.Empty));
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Logout()
        {
            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            _logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok();
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");

                return Ok(new LoginResult(userName, User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    jwtResult.AccessToken, jwtResult.RefreshToken.TokenString));
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // TODO: подтверждение не используется
            var user = new User { UserName = request.UserName, Email = request.Email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                // TODO: возможно стоит возвращать LoginResult, чтобы после регистрации сразу можно было бы работать
                return Ok();
            }

            // TODO: возможно стоит указать причину ошибки регистрации
            return BadRequest();
        }
    }
}

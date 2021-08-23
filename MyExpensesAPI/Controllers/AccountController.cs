using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExpensesAPI.Attributes;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Models.Models.Account;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route(WebApiRoutes.API_ACCOUNT)]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, UserManager<User> userManager)
        {
            _logger = logger;
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost("type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountTypeApiModel>> CreateAccountTypeAsync([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogInformation($"User [{User.Identity.Name}] tried to create a account type with name [{name}].");
                return BadRequest();
            }

            var result = await _accountService.CteateAccountTypeAsync(name);
            _logger.LogInformation($"User [{User.Identity.Name}] created a currency type [{result.Name}].");
            return Ok(result);
        }

        [HttpGet("type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<AccountTypeApiModel>> GetAccountTypesAsync()
        {
            var result = await _accountService.GetAccountTypesAsync();
            return result;
        }

        [HttpGet("type/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountTypeApiModel>> GetAccountTypesAsync(Guid id)
        {
            var result = await _accountService.GetAccountTypeByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountApiModel>> GetUserAccountAsync(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            // userId чтоб не получить случайно доступ к чужому счёту
            var result = await _accountService.GetUserAccountByIdAsync(id, new Guid(userId));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<AccountApiModel>>> GetUserAccountsAsync()
        {
            var userId = _userManager.GetUserId(User);
            var result = await _accountService.GetUserAccountsAsync(new Guid(userId));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountApiModel>> CreateUserAccountAsync([FromBody] AccountCreateApiModel model)
        {
            var result = await _accountService.CteateUserAccountAsync(model);
            return Ok(result);
        }

        [HttpPost("movement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountApiModel>> AccountMovementAsync([FromBody] AccountMovementApiModel model)
        {
            AccountApiModel result;

            if (model.IsRefill)
            {
                result = await _accountService.RefillAccountAsync(model);
            }
            else
            {
                result = await _accountService.WithdrawalAccountAsync(model);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

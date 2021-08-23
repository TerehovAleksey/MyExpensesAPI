using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExpensesAPI.Attributes;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Models.Models.Currency;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyExpensesAPI.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route(WebApiRoutes.API_CURRENCY)]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyService _currencyService;
        private readonly UserManager<User> _userManager;

        public CurrencyController(ILogger<CurrencyController> logger, ICurrencyService currencyService, UserManager<User> userManager)
        {
            _logger = logger;
            _currencyService = currencyService;
            _userManager = userManager;
        }

        [HttpPost("type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurrencyTypeApiModel>> CreateCurrencyTypeAsync([FromBody]string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogInformation($"User [{User.Identity.Name}] tried to create a currency type with name [{name}].");
                return BadRequest();
            }

            var result = await _currencyService.CreateCurrencyTypeAsync(name);
            _logger.LogInformation($"User [{User.Identity.Name}] created a currency type [{result.Name}].");
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurrencyApiModel>> GetUserCurrenciesAsync()
        {
            var userId = _userManager.GetUserId(User);
            var result = await _currencyService.GetUserCurrenciesAsync(new Guid(userId));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CurrencyApiModel>> CreateUserCurrency([FromBody]CurrencyCreateApiModel model)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _currencyService.CreateUserCurrencyAsync(model, new Guid(userId));
            _logger.LogInformation($"User [{User.Identity.Name}] created a user currency with id [{result.Id}].");
            return Ok(result);
        }
    }
}

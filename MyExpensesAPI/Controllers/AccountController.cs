using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExpensesAPI.Attributes;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Services.Interfaces;

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
    }
}

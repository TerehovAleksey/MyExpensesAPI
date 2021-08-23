using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExpensesAPI.Attributes;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Models.Models.Journal;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route(WebApiRoutes.API_JOURNAL)]
    public class JournalController : ControllerBase
    {
        private readonly ILogger<JournalController> _logger;
        private readonly IJournalService _journalService;
        private readonly UserManager<User> _userManager;

        public JournalController(ILogger<JournalController> logger, IJournalService journalService, UserManager<User> userManager)
        {
            _logger = logger;
            _journalService = journalService;
            _userManager = userManager;
        }

        [HttpGet("refill/{dateFrom}/{dateTo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<JournalRecordApiModel>> GetRefillJournal(DateTime dateFrom, DateTime dateTo)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _journalService.GetRefillRecords(new Guid(userId), dateFrom, dateTo);
            return result;
        }

        [HttpGet("withdrawal/{dateFrom}/{dateTo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<JournalRecordApiModel>> GetWithdrawalJournal(DateTime dateFrom, DateTime dateTo)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _journalService.GetWithdrawalRecords(new Guid(userId), dateFrom, dateTo);
            return result;
        }
    }
}

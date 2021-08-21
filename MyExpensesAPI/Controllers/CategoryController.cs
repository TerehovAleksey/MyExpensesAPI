using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExpensesAPI.Attributes;
using MyExpensesAPI.Domain;
using MyExpensesAPI.Models.Models.Category;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route(WebApiRoutes.API_CATEGORY)]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<User> _userManager;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService,
            UserManager<User> userManager)
        {
            _logger = logger;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [HttpGet("expense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<CategoryApiModel>>> GetExpenseCategoriesAsync()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _categoryService.GetExpenseCategoriesAsync(new Guid(userId));
            return Ok(categories);
        }

        [HttpPost("expense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryApiModel>> CreateExpenseCategoryAsync([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            var userId = _userManager.GetUserId(User);
            var result = await _categoryService.CreateExpenseCategoryAsync(new Guid(userId), name);
            _logger.LogInformation($"User [{User.Identity.Name}] created a category [{result.Name}].");
            return Ok(result);
        }

        [HttpPut("expense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryApiModel>> UpdateExpenseCategoryAsync([FromBody] CategoryApiModel model)
        {
            var result = await _categoryService.UpdateExpenseCategoryAsync(model);
            if (result)
            {
                _logger.LogInformation($"User [{User.Identity.Name}] updated a category [{model.Name}].");
                return Ok(model);
            }

            _logger.LogInformation($"User [{User.Identity.Name}] tried to update a category with id [{model.Id}].");
            return NotFound();
        }

        [HttpGet("income")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<CategoryApiModel>>> GetIncomeCategoriesAsync()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _categoryService.GetIncomeCategoriesAsync(new Guid(userId));
            return Ok(categories);
        }

        [HttpPost("income")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryApiModel>> CreateIncomeCategoryAsync([FromBody] string name)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _categoryService.CreateIncomeCategoryAsync(new Guid(userId), name);
            _logger.LogInformation($"User [{User.Identity.Name}] created a category [{result.Name}].");
            return Ok(result);
        }

        [HttpPut("income")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryApiModel>> UpdateIncomeCategoryAsync([FromBody] CategoryApiModel model)
        {
            var result = await _categoryService.UpdateIncomeCategoryAsync(model);
            if (result)
            {
                _logger.LogInformation($"User [{User.Identity.Name}] updated a category [{model.Name}].");
                return Ok(model);
            }

            _logger.LogInformation($"User [{User.Identity.Name}] tried to update a category with id [{model.Id}].");
            return NotFound();
        }
    }
}

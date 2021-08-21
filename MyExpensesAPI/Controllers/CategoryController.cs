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
        public async Task<ActionResult<IEnumerable<CategoryApiModel>>> GetExpenseCategoriesAsync()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _categoryService.GetExpenseCategoriesAsync(new Guid(userId));
            return Ok(categories);
        }

        [HttpPost("expense")]
        public async Task<ActionResult<CategoryApiModel>> CreateExpenseCategoryAsync([FromBody]string name)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _categoryService.CreateExpenseCategoryAsync(new Guid(userId), name);
            return Ok(result);
        }
    }
}

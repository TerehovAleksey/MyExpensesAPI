using MyExpensesAPI.Models.Models.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryApiModel>> GetExpenseCategoriesAsync(Guid userId);
        public Task<CategoryApiModel> CreateExpenseCategoryAsync(Guid userId, string name);
        public Task<bool> UpdateExpenseCategoryAsync(CategoryApiModel category);
        public Task<bool> DeleteExpenseCategoryAsync(Guid categoryId);
        public Task<IEnumerable<CategoryApiModel>> GetIncomeCategoriesAsync(Guid userId);
        public Task<CategoryApiModel> CreateIncomeCategoryAsync(Guid userId, string name);
        public Task<bool> UpdateIncomeCategoryAsync(CategoryApiModel category);
        public Task<bool> DeleteIncomeCategoryAsync(Guid categoryId);
    }
}

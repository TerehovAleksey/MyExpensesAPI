using Microsoft.EntityFrameworkCore;
using MyExpensesAPI.Domain;
using MyExpensesAPI.EfDal;
using MyExpensesAPI.Models.Models.Category;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryApiModel> CreateExpenseCategoryAsync(Guid userId, string name)
        {
            var category = new ExpenseCategory
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId,
                DateOfCreation = DateTime.Now,
                IsDeleted = false
            };

            _context.ExpenseCategories.Add(category);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new CategoryApiModel(userId, name);
        }

        public async Task<CategoryApiModel> CreateIncomeCategoryAsync(Guid userId, string name)
        {
            var category = new IncomeCategory
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId,
                DateOfCreation = DateTime.Now,
                IsDeleted = false
            };

            _context.IncomeCategories.Add(category);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new CategoryApiModel(userId, name);
        }

        public async Task<bool> DeleteExpenseCategoryAsync(Guid categoryId)
        {
            var category = await _context.ExpenseCategories.FindAsync(categoryId).ConfigureAwait(false);

            if (category == null)
            {
                return false;
            }

            _context.ExpenseCategories.Remove(category);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeleteIncomeCategoryAsync(Guid categoryId)
        {
            var category = await _context.IncomeCategories.FindAsync(categoryId).ConfigureAwait(false);

            if (category == null)
            {
                return false;
            }

            _context.IncomeCategories.Remove(category);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<IEnumerable<CategoryApiModel>> GetExpenseCategoriesAsync(Guid userId)
        {
            return await _context.ExpenseCategories.Where(x => x.UserId == userId || x.UserId == null)
                .OrderBy(x => x.Name)
                .Select(x => new CategoryApiModel(userId, x.Name))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<CategoryApiModel>> GetIncomeCategoriesAsync(Guid userId)
        {
            return await _context.IncomeCategories.Where(x => x.UserId == userId || x.UserId == null)
                .OrderBy(x => x.Name)
                .Select(x => new CategoryApiModel(userId, x.Name))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateExpenseCategoryAsync(CategoryApiModel category)
        {
            var item = await _context.ExpenseCategories.FindAsync(category.Id).ConfigureAwait(false);

            if (item == null)
            {
                return false;
            }

            item.Name = category.Name;
            item.DateOfChange = DateTime.Now;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> UpdateIncomeCategoryAsync(CategoryApiModel category)
        {
            var item = await _context.IncomeCategories.FindAsync(category.Id).ConfigureAwait(false);

            if (item == null)
            {
                return false;
            }

            item.Name = category.Name;
            item.DateOfChange = DateTime.Now;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}

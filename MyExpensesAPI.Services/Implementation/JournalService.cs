using Microsoft.EntityFrameworkCore;
using MyExpensesAPI.EfDal;
using MyExpensesAPI.Models.Models.Journal;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Implementation
{
    public class JournalService : IJournalService
    {
        private readonly AppDbContext _context;

        public JournalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JournalRecordApiModel>> GetRefillRecords(Guid userId, DateTime dateFrom, DateTime dateTo)
        {
            return await _context.IncomeJournals
                .Include(x => x.IncomeCategory)
                .Where(x => x.IsDeleted == false && x.UsersAccount.UsersCurrency.UserId == userId && x.DateOfCreation >= dateFrom && x.DateOfCreation <= dateTo)
                .Select(x => new JournalRecordApiModel
                {
                    Id = x.Id,
                    DateTime = x.DateOfCreation,
                    Description = x.Description,
                    Value = x.Value,
                    CategoryName = x.IncomeCategory.Name
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<JournalRecordApiModel>> GetWithdrawalRecords(Guid userId, DateTime dateFrom, DateTime dateTo)
        {
            return await _context.ExpenseJournals
                .Include(x => x.ExpenseCategory)
                .Where(x => x.IsDeleted == false && x.UsersAccount.UsersCurrency.UserId == userId && x.DateOfCreation >= dateFrom && x.DateOfCreation <= dateTo)
                .Select(x => new JournalRecordApiModel
                {
                    Id = x.Id,
                    DateTime = x.DateOfCreation,
                    Description = x.Description,
                    Value = x.Value,
                    CategoryName = x.ExpenseCategory.Name
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MyExpensesAPI.Domain;
using MyExpensesAPI.EfDal;
using MyExpensesAPI.Models.Models.Account;
using MyExpensesAPI.Models.Models.Currency;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        #region AccountType

        public async Task<AccountTypeApiModel> CteateAccountTypeAsync(string name)
        {
            var accountType = new AccountType
            {
                DateOfCreation = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Name = name
            };

            _context.AccountTypes.Add(accountType);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new AccountTypeApiModel(accountType.Id, name);
        }

        public async Task<IEnumerable<AccountTypeApiModel>> GetAccountTypesAsync()
        {
            return await _context.AccountTypes
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Name)
                .Select(x => new AccountTypeApiModel(x.Id, x.Name))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<AccountTypeApiModel> GetAccountTypeByIdAsync(Guid id)
        {
            var result = await _context.AccountTypes
                .Where(x => x.IsDeleted == false && x.Id == id)
                .OrderBy(x => x.Name)
                .Select(x => new AccountTypeApiModel(x.Id, x.Name))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (result == null)
            {
                return null;
            }    

            return new AccountTypeApiModel(result.Id, result.Name);
        }

        #endregion

        #region UserAccount

        public async Task<AccountApiModel> CteateUserAccountAsync(AccountCreateApiModel model)
        {
            var userAccount = new UsersAccount
            {
                AccountTypeId = model.AccountTypeId,
                Balance = model.Balance,
                DateOfCreation = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                UsersCurrencyId = model.UserCurrencyId
            };

            _context.UsersAccounts.Add(userAccount);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return await GetUserAccountByIdAsync(userAccount.Id);
        }

        public async Task<AccountApiModel> GetUserAccountByIdAsync(Guid id, Guid userId)
        {
            var result = await _context.UsersAccounts
                .Include(x => x.AccountType)
                .Include(x => x.UsersCurrency)
                .ThenInclude(x => x.CurrencyType)
                .Where(x => x.Id == id && x.IsDeleted == false && x.UsersCurrency.UserId == userId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return Convert(result);
        }

        private async Task<AccountApiModel> GetUserAccountByIdAsync(Guid id)
        {
            var result = await _context.UsersAccounts
                .Include(x => x.AccountType)
                .Include(x => x.UsersCurrency)
                .ThenInclude(x => x.CurrencyType)
                .Where(x => x.Id == id && x.IsDeleted == false)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return Convert(result);
        }

        public async Task<IEnumerable<AccountApiModel>> GetUserAccountsAsync(Guid userId)
        {
            var result = await _context.UsersAccounts
                .Include(x => x.AccountType)
                .Include(x => x.UsersCurrency)
                .ThenInclude(x => x.CurrencyType)
                .Where(x => x.UsersCurrency.UserId == userId && x.IsDeleted == false)
                .ToListAsync()
                .ConfigureAwait(false);

            return result.Select(Convert);
        }

        #endregion

        #region Movement

        public async Task<AccountApiModel> RefillAccountAsync(AccountMovementApiModel model)
        {
            var account = await _context.UsersAccounts
                .Where(x => x.Id == model.UserAccountId && x.IsDeleted == false)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (account == null)
            {
                throw new ArgumentException(nameof(model.UserAccountId));
            }

            if (model.IsRefill)
            {
                account.Balance += model.Value;
            }
            else
            {
                throw new ArgumentException(nameof(model.IsRefill));
            }

            var journalRecord = new IncomeJournal
            {
                DateOfCreation = DateTime.Now,
                Description = model.Description,
                Id = Guid.NewGuid(),
                IncomeCategoryId = model.CategoryId,
                IsDeleted = false,
                UsersAccountId = model.UserAccountId,
                Value = model.Value
            };

            _context.IncomeJournals.Add(journalRecord);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return await GetUserAccountByIdAsync(model.UserAccountId);
        }

        public async Task<AccountApiModel> WithdrawalAccountAsync(AccountMovementApiModel model)
        {
            var account = await _context.UsersAccounts
                 .Where(x => x.Id == model.UserAccountId && x.IsDeleted == false)
                 .FirstOrDefaultAsync()
                 .ConfigureAwait(false);

            if (account == null)
            {
                throw new ArgumentException(nameof(model.UserAccountId));
            }

            if (!model.IsRefill)
            {
                account.Balance -= model.Value;
            }
            else
            {
                throw new ArgumentException(nameof(model.IsRefill));
            }

            var journalRecord = new ExpenseJournal
            {
                DateOfCreation = DateTime.Now,
                Description = model.Description,
                Id = Guid.NewGuid(),
                ExpenseCategoryId = model.CategoryId,
                IsDeleted = false,
                UsersAccountId = model.UserAccountId,
                Value = model.Value
            };

            _context.ExpenseJournals.Add(journalRecord);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return await GetUserAccountByIdAsync(model.UserAccountId);
        }

        #endregion

        private static AccountApiModel Convert(UsersAccount account)
        {
            if (account == null)
            {
                return null;
            }

            return new AccountApiModel
            {
                Id = account.Id,
                Balance = account.Balance,
                AccountType = new AccountTypeApiModel(account.AccountType.Id, account.AccountType.Name),
                UserCurrency = new CurrencyApiModel
                {
                    Id = account.UsersCurrency.Id,
                    Default = account.UsersCurrency.IsDefault,
                    Rate = account.UsersCurrency.Rate,
                    Type = new CurrencyTypeApiModel(account.UsersCurrency.CurrencyType.Id, account.UsersCurrency.CurrencyType.Name)
                }
            };
        }
    }
}

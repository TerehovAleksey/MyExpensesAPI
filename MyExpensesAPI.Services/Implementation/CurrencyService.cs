using Microsoft.EntityFrameworkCore;
using MyExpensesAPI.Domain;
using MyExpensesAPI.EfDal;
using MyExpensesAPI.Models.Models.Currency;
using MyExpensesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Implementation
{
    public class CurrencyService : ICurrencyService
    {
        private readonly AppDbContext _context;

        public CurrencyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CurrencyTypeApiModel> CreateCurrencyTypeAsync(string name)
        {
            var currencyType = new CurrencyType
            {
                DateOfCreation = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Name = name
            };

            _context.CurrencyTypes.Add(currencyType);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new CurrencyTypeApiModel(currencyType.Id, name);
        }

        public async Task<CurrencyApiModel> CreateUserCurrencyAsync(CurrencyCreateApiModel model, Guid userId)
        {
            var userCurrency = new UsersCurrency
            {
                DateOfCreation = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDefault = model.IsDefault,
                IsDeleted = false,
                Rate = model.Rate,
                UserId = userId,
                CurrencyTypeId = model.CurrencyTypeId
            };

            _context.UsersCurrencies.Add(userCurrency);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return await GetUserCurrencyByIdAsync(userCurrency.Id);
        }

        public async Task<IEnumerable<CurrencyApiModel>> GetUserCurrenciesAsync(Guid userId)
        {
            var result = await _context.UsersCurrencies
                .Include(x => x.CurrencyType)
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .OrderBy(x => x.IsDefault)
                .ThenBy(x => x.CurrencyType.Name)
                .Select(x => new CurrencyApiModel
                {
                    Id = x.Id,
                    Default = x.IsDefault,
                    Rate = x.Rate,
                    Type = new CurrencyTypeApiModel(x.CurrencyType.Id, x.CurrencyType.Name)
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return result;
        }

        public async Task<CurrencyApiModel> GetUserCurrencyByIdAsync(Guid id)
        {
            var result = await _context.UsersCurrencies
                .Include(x => x.CurrencyType)
                .Where(x => x.Id == id && x.IsDeleted == false)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return new CurrencyApiModel
            {
                Id = result.Id,
                Default = result.IsDefault,
                Rate = result.Rate,
                Type = new CurrencyTypeApiModel(result.CurrencyType.Id, result.CurrencyType.Name)
            };
        }
    }
}

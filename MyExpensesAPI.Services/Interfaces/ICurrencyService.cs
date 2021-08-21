using MyExpensesAPI.Models.Models.Currency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Interfaces
{
    public interface ICurrencyService
    {
        public Task<CurrencyTypeApiModel> CreateCurrencyTypeAsync(string name);

        public Task<CurrencyApiModel> CreateUserCurrencyAsync(CurrencyCreateApiModel model, Guid userId);
        public Task<IEnumerable<CurrencyApiModel>> GetUserCurrenciesAsync(Guid userId);
        public Task<CurrencyApiModel> GetUserCurrencyByIdAsync(Guid id);
    }
}

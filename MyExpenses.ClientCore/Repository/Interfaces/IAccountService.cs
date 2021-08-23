using MyExpensesAPI.Models.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.ClientCore.Repository.Interfaces
{
    public interface IAccountService
    {
        public Task<List<AccountTypeApiModel>> GetAccountTypesAsync();
    }
}

using MyExpensesAPI.Models.Models.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpensesAPI.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountTypeApiModel> CteateAccountTypeAsync(string name);
        public Task<IEnumerable<AccountTypeApiModel>> GetAccountTypesAsync();
        public Task<AccountTypeApiModel> GetAccountTypeByIdAsync(Guid id);

        public Task<AccountApiModel> CteateUserAccountAsync(AccountCreateApiModel model);
        public Task<AccountApiModel> GetUserAccountByIdAsync(Guid id, Guid userId);
        public Task<IEnumerable<AccountApiModel>> GetUserAccountsAsync(Guid userId);


        public Task<AccountApiModel> RefillAccountAsync(AccountMovementApiModel model);
        public Task<AccountApiModel> WithdrawalAccountAsync(AccountMovementApiModel model);
    }
}

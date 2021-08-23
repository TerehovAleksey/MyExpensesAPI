using MyExpenses.ClientCore.Helpers;
using MyExpenses.ClientCore.Repository.Interfaces;
using MyExpensesAPI.Models;
using MyExpensesAPI.Models.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.ClientCore.Repository.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly HttpHelper _httpHelper;

        public AccountService(IHttpInitData httpInit)
        {
            _httpHelper = new HttpHelper(httpInit);
        }

        public async Task<List<AccountTypeApiModel>> GetAccountTypesAsync() =>
            await _httpHelper.GetAsync<List<AccountTypeApiModel>>(WebApiRoutes.API_ACCOUNT + "/type");
    }
}

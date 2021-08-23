using MyExpenses.ClientCore.Helpers;
using MyExpenses.ClientCore.Repository.Interfaces;
using MyExpensesAPI.Models;
using MyExpensesAPI.Models.User;
using System.Threading.Tasks;

namespace MyExpenses.ClientCore.Repository.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpHelper _httpHelper;

        public UserService(IHttpInitData httpInit)
        {
            _httpHelper = new HttpHelper(httpInit);
        }

        public async Task<LoginResult> Login(LoginRequest loginRequest) =>
            await _httpHelper.PostAsync<LoginRequest, LoginResult>(WebApiRoutes.API_USER + "/login", loginRequest);
    }
}

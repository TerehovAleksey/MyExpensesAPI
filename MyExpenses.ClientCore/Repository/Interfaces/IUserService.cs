using MyExpensesAPI.Models.User;
using System.Threading.Tasks;

namespace MyExpenses.ClientCore.Repository.Interfaces
{
    public interface IUserService
    {
        Task<LoginResult> Login(LoginRequest loginRequest);
    }
}

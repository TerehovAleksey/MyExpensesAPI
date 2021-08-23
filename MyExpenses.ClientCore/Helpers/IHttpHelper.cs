using System.Threading.Tasks;

namespace MyExpenses.ClientCore.Helpers
{
    public interface IHttpHelper
    {
        Task<TResult> DeleteAsync<TResult>(string controller);
        Task<TResult> GetAsync<TResult>(string controller);
        Task<TResult> PostAsync<TRequest, TResult>(string controller, TRequest body);
        Task<TResult> UpdateAsync<TRequest, TResult>(string controller, TRequest body);
    }
}
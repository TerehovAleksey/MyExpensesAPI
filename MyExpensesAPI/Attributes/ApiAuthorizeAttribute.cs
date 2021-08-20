using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MyExpensesAPI.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}

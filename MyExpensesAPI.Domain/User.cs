using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MyExpensesAPI.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLoginDate { get; set; }


        public virtual ICollection<UsersCurrency> UsersCurrencies {  get; set; }
        public virtual ICollection<ExpenceCategory> ExpenceCategories {  get; set; }
        public virtual ICollection<IncomeCategory> IncomeCategories {  get; set; }
    }
}

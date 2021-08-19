using System.Collections.Generic;

namespace MyExpensesAPI.Domain
{
    public class AccountType : BaseEntity
    {
        public string Name { get; set; }


        public virtual ICollection<UsersAccount> UsersAccounts { get; set; }
    }
}

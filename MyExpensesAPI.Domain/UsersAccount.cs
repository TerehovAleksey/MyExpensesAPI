using System;
using System.Collections.Generic;

namespace MyExpensesAPI.Domain
{
    public class UsersAccount : BaseEntity
    {
        public Guid AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
        public Guid UsersCurrencyId { get; set; }
        public virtual UsersCurrency UsersCurrency { get; set; }

        public decimal Balance { get; set; }

        public virtual ICollection<ExpenseJournal> ExpenseJournals { get; set; }
    }
}

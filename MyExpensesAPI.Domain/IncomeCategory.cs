using System;
using System.Collections.Generic;

namespace MyExpensesAPI.Domain
{
    public class IncomeCategory : BaseEntity
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }


        public virtual ICollection<IncomeJournal> IncomeJournals { get; set; }
    }
}

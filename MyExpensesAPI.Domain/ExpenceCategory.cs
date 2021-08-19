using System;
using System.Collections.Generic;

namespace MyExpensesAPI.Domain
{
    public class ExpenceCategory : BaseEntity
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public ICollection<ExpenceJournal> ExpenceJournals { get; set; }
    }
}

using System;

namespace MyExpensesAPI.Domain
{
    public class ExpenceJournal : BaseEntity
    {
        public Guid UsersAccountId { get; set; }
        public virtual UsersAccount UsersAccount { get; set; }
        public Guid ExpenceCategoryId { get; set; }
        public virtual ExpenceCategory ExpenceCategory { get; set; }

        public decimal Value { get; set; }
        public string? Description { get; set; }
    }
}

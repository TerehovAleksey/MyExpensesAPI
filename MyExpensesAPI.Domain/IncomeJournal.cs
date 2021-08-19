using System;

namespace MyExpensesAPI.Domain
{
    public class IncomeJournal : BaseEntity
    {
        public Guid UsersAccountId { get; set; }
        public virtual UsersAccount UsersAccount { get; set; }
        public Guid IncomeCategoryId { get; set; }
        public virtual IncomeCategory IncomeCategory { get; set; }

        public decimal Value { get; set; }
        public string? Description { get; set; }
    }
}

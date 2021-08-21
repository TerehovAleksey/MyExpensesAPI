using System;

namespace MyExpensesAPI.Domain
{
    public class ExpenseJournal : BaseEntity
    {
        public Guid UsersAccountId { get; set; }
        public virtual UsersAccount UsersAccount { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }

        public decimal Value { get; set; }
        public string? Description { get; set; }
    }
}

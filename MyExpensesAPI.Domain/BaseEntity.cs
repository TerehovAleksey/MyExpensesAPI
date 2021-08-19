using System;

namespace MyExpensesAPI.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfChange { get; set; }
        public bool IsDeleted { get; set; }
    }
}

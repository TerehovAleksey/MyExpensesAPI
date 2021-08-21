using MyExpensesAPI.Domain.Interfaces;
using System;

namespace MyExpensesAPI.Domain
{
    public class BaseEntity : IFakeDelete
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfChange { get; set; }
        public bool IsDeleted { get; set; }
    }
}

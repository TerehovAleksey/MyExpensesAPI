using System;

namespace MyExpensesAPI.Domain
{
    public class UsersCurrency : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal Rate { get; set; }
        public bool IsDefault {  get; set; }

        public Guid CurrencyTypeId { get; set; }
        public virtual CurrencyType CurrencyType {  get; set; }
    }
}

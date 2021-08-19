using System.Collections.Generic;

namespace MyExpensesAPI.Domain
{
    public class CurrencyType : BaseEntity
    {
        public string Name { get; set; }


        public virtual ICollection<UsersCurrency> UsersCurrencies {  get; set; }
    }
}

using System;

namespace MyExpenses.ClientCore.Exceptions
{
    public class MyExpensesException : Exception
    {
        public MyExpensesException(string message) : base(message)
        {
        }

        public MyExpensesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MyExpensesException()
        {
        }
    }
}

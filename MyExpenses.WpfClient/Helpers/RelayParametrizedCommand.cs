using System;
using System.Windows.Input;

namespace MyExpenses.WpfClient.Helpers
{
    public class RelayParametrizedCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<object> predicate;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayParametrizedCommand(Action<object> execute, Predicate<object> canExecute)
        {
            action = execute;
            predicate = canExecute;
        }

        public bool CanExecute(object parameter) => predicate == null || predicate(parameter);

        public void Execute(object parameter) => action?.Invoke(parameter);
    }
}

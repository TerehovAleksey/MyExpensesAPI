using System;
using System.Windows.Input;

namespace MyExpenses.WpfClient.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action action;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute)
        {
            action = execute;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action?.Invoke();
    }
}

using MyExpenses.ClientCore.Repository.Interfaces;
using MyExpenses.WpfClient.Helpers;
using MyExpenses.WpfClient.Services;
using MyExpensesAPI.Models.Models.Account;
using MyExpensesAPI.Models.User;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyExpenses.WpfClient.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ISettingsService _settingsService;

        public ICommand GetAccountTypesCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ObservableCollection<AccountTypeApiModel> AccountTypes { get; private set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public MainWindowViewModel(IAccountService accountService, IUserService userService, ISettingsService settingsService)
        {
            _accountService = accountService;
            _userService = userService;
            _settingsService = settingsService;

            LoginCommand = new RelayCommand(LoginExecute);
            GetAccountTypesCommand = new RelayCommand(GetAccountTypesExecute);
            AccountTypes = new ObservableCollection<AccountTypeApiModel>();
        }

        private async void LoginExecute()
        {
            var loginRequest = new LoginRequest(Login, Password);
            var result = await _userService.Login(loginRequest);
            _settingsService.Current.LoginResult = result;
            if (result == null)
            {
                throw new Exception(nameof(result));
            }
        }

        private async void GetAccountTypesExecute()
        {
            var result = await _accountService.GetAccountTypesAsync();
            foreach (var item in result)
            {
                AccountTypes.Add(item);
            }
        }
    }
}

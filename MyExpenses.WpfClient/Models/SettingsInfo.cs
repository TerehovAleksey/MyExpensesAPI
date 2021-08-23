using MyExpensesAPI.Models.User;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace MyExpenses.WpfClient.Models
{
    public class SettingsInfo
    {
        /// <summary>
        /// Флаг, означающий, что текущие настройки изменились
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public bool IsSettingsChanged { get; set; }

        #region Global

        private bool _general_firstRun = true;

        /// <summary>
        /// Приложение было запущено первый раз
        /// </summary>
        public bool General_FirstRun
        {
            get => _general_firstRun;
            set
            {
                if (value == _general_firstRun)
                    return;

                _general_firstRun = value;
                IsSettingsChanged = true;
            }
        }

        #endregion

        private string _webApiUrl = "https://localhost:44365";

        /// <summary>
        /// Адрес WebAPI-сервера
        /// </summary>
        public string WebApiUrl
        {
            get => _webApiUrl;
            set
            {
                if (value == _webApiUrl)
                {
                    return;
                }
                _webApiUrl = value;
                IsSettingsChanged = true;
            }
        }

        private LoginResult _loginResult;
        public LoginResult LoginResult
        {
            get => _loginResult;
            set
            {
                if (value == _loginResult)
                {
                    return;
                }
                _loginResult = value;
                IsSettingsChanged = true;
            }
        }
    }
}

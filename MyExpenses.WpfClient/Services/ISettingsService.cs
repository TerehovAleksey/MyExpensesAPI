using MyExpenses.WpfClient.Models;

namespace MyExpenses.WpfClient.Services
{
    /// <summary>
    /// Интерфейс работы с настройками приложения
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Текущие настройки приложения
        /// </summary>
        SettingsInfo Current { get; }

        /// <summary>
        /// Путь к папке логов приложения (проверяет наличие папки и при необходимости создаёт её)
        /// </summary>
        string GetLogsFolderLocation();

        /// <summary>
        /// Файл настроек
        /// </summary>
        /// <returns></returns>
        string GetSettingsFileName();

        /// <summary>
        /// Полный путь к фвйлу настроек
        /// </summary>
        /// <returns></returns>
        string GetSettingsFilePath();

        /// <summary>
        /// Путь к папке настроек приложения (проверяет наличие папки и при необходимости создаёт её)
        /// </summary>
        /// <returns></returns>
        string GetSettingsFolderLocation();

        /// <summary>
        /// Инициализация настроек по умолчанию
        /// </summary>
        void InitDefault();

        /// <summary>
        /// Загрузка сохранённых настроек (при неудачной загрузке настройки будут инициализированы настройками по умолчанию)
        /// </summary>
        void Load();

        /// <summary>
        /// Сброс настроек на настройки по умолчанию
        /// </summary>
        void Reset();

        /// <summary>
        /// Сохранение текущих настроек
        /// (не проверяет было ли изменение!)
        /// </summary>
        void Save();
    }
}

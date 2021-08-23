using MyExpenses.WpfClient.Models;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MyExpenses.WpfClient.Services
{
    /// <summary>
    /// Менеждер настроек приложения
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private const string LOGS_FOLDER_NAME = "Logs";
        private const string SETTINGS_FOLDER_NAME = "Settings";
        private const string SETTINGS_FILE_NAME = "Settings";
        private const string SETTINGS_FILE_EXTENSIONS = ".xml";

        private readonly IAssemblyService _assembly;

        /// <summary>
        /// Текущие настройки приложения
        /// </summary>
        public SettingsInfo Current { get; private set; }

        public SettingsService(IAssemblyService assembly)
        {
            _assembly = assembly;
            Load();
        }

        #region Init, Reset

        /// <summary>
        /// Инициализация настроек по умолчанию
        /// </summary>
        public void InitDefault()
        {
            Current = new SettingsInfo
            {
                IsSettingsChanged = true
            };
        }

        /// <summary>
        /// Сброс настроек на настройки по умолчанию
        /// </summary>
        public void Reset()
        {
            InitDefault();
        }

        #endregion

        #region Location

        /// <summary>
        /// Путь к папке настроек приложения (проверяет наличие папки и при необходимости создаёт её)
        /// </summary>
        /// <returns></returns>
        public string GetSettingsFolderLocation()
        {
            //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _assembly.Name, SETTINGS_FOLDER_NAME);
            string path = SETTINGS_FOLDER_NAME;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// Путь к папке логов приложения (проверяет наличие папки и при необходимости создаёт её)
        /// </summary>
        /// <returns></returns>
        public string GetLogsFolderLocation()
        {
            string path = LOGS_FOLDER_NAME;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        #endregion

        #region FileName, FilePath

        /// <summary>
        /// Файл настроек
        /// </summary>
        /// <returns></returns>
        public string GetSettingsFileName() => $"{SETTINGS_FILE_NAME}{SETTINGS_FILE_EXTENSIONS}";

        /// <summary>
        /// Полный путь к файлу настроек
        /// </summary>
        /// <returns></returns>
        public string GetSettingsFilePath() => Path.Combine(GetSettingsFolderLocation(), GetSettingsFileName());

        #endregion

        #region Load, Save

        /// <summary>
        /// Загрузка сохранённых настроек (при неудачной загрузке настройки будут инициализированы настройками по умолчанию)
        /// </summary>
        public void Load()
        {
            var filePath = GetSettingsFilePath();
            if (File.Exists(filePath))
            {
                try
                {
                    Current = DeserializeFromXmlFile(filePath);
                }
                catch (Exception)
                {
                    InitDefault();
                }
            }
            else
            {
                InitDefault();
            }
            Current.IsSettingsChanged = true;
        }

        /// <summary>
        /// Сохранение текущих настроек 
        /// (не проверяет было ли изменение!)
        /// </summary>
        public void Save()
        {
            // проверяем директорию
            var location = GetSettingsFolderLocation();
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }

            // сохраняем
            SerializeToXmlFile(GetSettingsFilePath());

            // сбрасываем флаг, что настройки изменились
            Current.IsSettingsChanged = false;
        }

        private static SettingsInfo DeserializeFromXmlFile(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(SettingsInfo));

            using var filestream = new FileStream(filePath, FileMode.Open);
            using var xmlReader = XmlReader.Create(filestream);
            SettingsInfo settingsInfo = (SettingsInfo)xmlSerializer.Deserialize(xmlReader);

            return settingsInfo;

        }

        private void SerializeToXmlFile(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(SettingsInfo));

            using var filestream = new FileStream(filePath, FileMode.Create);
            xmlSerializer.Serialize(filestream, Current);

        }

        #endregion
    }
}

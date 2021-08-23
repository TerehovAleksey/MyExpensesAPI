using System;

namespace MyExpenses.WpfClient.Services
{
    /// <summary>
    /// Интерфейс работы с основными данными сборки
    /// </summary>
    public interface IAssemblyService
    {
        /// <summary>
        /// Расположение приложения
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Название приложения
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Текущая версия приложения
        /// </summary>
        Version Version { get; }
    }
}

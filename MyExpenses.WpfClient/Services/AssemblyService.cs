using System;
using System.Reflection;

namespace MyExpenses.WpfClient.Services
{
    public class AssemblyService : IAssemblyService
    {
        public Version Version { get; }
        public string Location { get; }
        public string Name { get; }

        public AssemblyService()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var name = assembly.GetName();

            Version = name?.Version;
            Location = assembly?.Location;
            Name = name?.Name;
        }
    }
}

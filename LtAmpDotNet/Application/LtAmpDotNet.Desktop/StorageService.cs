using LtAmpDotNet.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace LtAmpDotNet.Desktop
{
    public class StorageService : IStorageService
    {
        internal static readonly string ConfigurationFolder = "LtAmpDotNet";
        internal static readonly string ConfigurationResourcePath = "LtAmpDotNet.Assets";
        internal static readonly string ConfigurationPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConfigurationFolder);

        public async Task<bool> SaveAsync<T>(string fileName, T? data)
        {
            try
            {
                if (!Directory.Exists(ConfigurationPath))
                {
                    Directory.CreateDirectory(ConfigurationPath);
                }
                string filePath = Path.Join(ConfigurationPath, fileName);
                await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(data));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<T?> LoadAsync<T>(string fileName)
        {
            string filePath = Path.Join(ConfigurationPath, fileName);
            return File.Exists(filePath)
                ? JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(filePath))
                : await LoadDefaultConfigAsync<T>(fileName);
        }

        private static async Task<T> LoadDefaultConfigAsync<T>(string fileName)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ISettingsService))!;
            string resourceName = $"{ConfigurationResourcePath}.{fileName}";
            using (TextReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)!))
            {
                return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync())!;
            }
        }
    }
}
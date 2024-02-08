using Commons.Music.Midi;
using LtAmpDotNet.Lib.Model.Profile;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli
{
    internal class Configuration
    {
        [JsonProperty("midiCommands")]
        public List<MidiCommand>? MidiCommands { get; set; }

        internal static string ConfigurationPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LtAmpDotNet");
        internal static string ConfigurationFileName = "config.json";
        internal static string ConfigurationResourceName = "LtAmpDotNet.Cli.config.json";

        public static void Load()
        {
            string configString = LoadLocalConfig() ?? LoadUserConfig() ?? LoadDefaultConfig();
            Program.Configuration = JsonConvert.DeserializeObject<Configuration>(configString);
        }

        public static string? LoadUserConfig()
        {
            return File.Exists(Path.Join(ConfigurationPath, ConfigurationFileName))
                ? File.ReadAllText(Path.Join(ConfigurationPath, ConfigurationFileName))
                : null;
        }

        public static string? LoadLocalConfig()
        {
            return File.Exists(Path.Join(Path.GetDirectoryName(AppContext.BaseDirectory), ConfigurationFileName))
                ? File.ReadAllText(Path.Join(Path.GetDirectoryName(AppContext.BaseDirectory), ConfigurationFileName))
                : null;
        }
        public static string LoadDefaultConfig()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(ConfigurationResourceName)!)
            {
                using (StreamReader reader = new(stream))
                {
                    string result = reader.ReadToEnd();
                    return result;

                }
            }
        }

        public static bool WriteDefaultConfigToUsersHomeDirectory()
        {
            if (!File.Exists(Path.Join(ConfigurationPath, ConfigurationFileName)))
            {
                Directory.CreateDirectory(ConfigurationPath);
                File.WriteAllText(Path.Join(ConfigurationPath, ConfigurationFileName), LoadDefaultConfig());
            }
            return false;
        }
    }

    public class MidiCommand
    {
        [JsonProperty("commandType")]
        public byte CommandType { get; set; }

        [JsonProperty("command")]
        public int? Command { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; } = "";
    }

}

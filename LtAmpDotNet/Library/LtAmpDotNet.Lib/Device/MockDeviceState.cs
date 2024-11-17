using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;
using System.Reflection;

namespace LtAmpDotNet.Lib.Device
{
    public class MockDeviceState
    {
        public List<string>? initializationStrings { get; set; }
        public string? firmwareVersion { get; set; }
        public string? productId { get; set; }
        public uint[]? qaSlots { get; set; }
        public float? usbGain { get; set; }
        public List<string>? Presets { get; set; }
        public ModalContext? modalContext { get; set; }

        public int CurrentPresetIndex { get; set; }

        public int CurrentLoadedPreset { set => CurrentPresetIndex = value; }
        public int CurrentDisplayedPreset { set => CurrentPresetIndex = value; }

        public static MockDeviceState Load()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "LtAmpDotNet.Lib.Device.mockAmpState.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
            {
                using (StreamReader reader = new(stream))
                {
                    string result = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<MockDeviceState>(result);
                }
            }
        }
    }
}
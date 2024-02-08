using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;
using System.Reflection;

namespace LtAmpDotNet.Base
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

        public int CurrentPresetIndex { get; set; } = 1;

        public static MockDeviceState Load()
        {
            string filePath = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "mockAmpState.json");
            return JsonConvert.DeserializeObject<MockDeviceState>(File.ReadAllText(filePath));

        }
    }
}
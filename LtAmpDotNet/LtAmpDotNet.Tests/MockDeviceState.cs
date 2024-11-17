using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;

namespace LtAmpDotNet.Tests
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

        public static MockDeviceState Load()
        {
            string filePath = Path.Join(Environment.CurrentDirectory, "mockAmpState.json");
            return JsonConvert.DeserializeObject<MockDeviceState>(File.ReadAllText(filePath));

        }
    }
}
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System.Security.Cryptography.X509Certificates;

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
            var filePath = Path.Join(Environment.CurrentDirectory, "mockAmpState.json");
            return JsonConvert.DeserializeObject<MockDeviceState>(File.ReadAllText(filePath));

        }
    }
}
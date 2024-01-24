using LtDotNet.Lib;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System.Security.Cryptography.X509Certificates;

namespace LtDotNet.Tests
{
    public class MockDeviceState
    {
        public string firmwareVersion { get; set; }
        public string productId { get; set; }
        public uint[] qaSlots { get; set; }
        public float usbGain { get; set; }
        //public ModalContext modalContext { get; set; }

        public static MockDeviceState Load()
        {
            var filePath = Path.Join(Environment.CurrentDirectory, "mockAmpState.json");
            return JsonConvert.DeserializeObject<MockDeviceState>(File.ReadAllText(filePath));

        }
    }
}
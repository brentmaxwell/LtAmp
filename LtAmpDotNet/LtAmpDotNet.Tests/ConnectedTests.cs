using LtAmpDotNet.Lib;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System.Security.Cryptography.X509Certificates;

namespace LtAmpDotNet.Tests
{
    public class ConnectedTests
    {
        private LtAmpDevice amp;
        private ExpectedValues expectedValues;

        public delegate void TestCallback(bool isConnected);

        [SetUp]
        public void Setup()
        {
            expectedValues = ExpectedValues.Load();
            amp = new LtAmpDevice();
        }

        public void Open(TestCallback callback)
        {
            amp.DeviceConnected += (sender, eventArgs) => { callback(true); };
            amp.Open();
        }

        [Test]
        public void OpenAndConnect()
        {
            if (!amp.IsOpen)
            {
                var wait = new AutoResetEvent(false);
                Open(callback =>
                {
                    wait.Set();
                });
                Assert.IsTrue(wait.WaitOne(TimeSpan.FromSeconds(5)));
            }
            else
            {
                Assert.IsTrue(amp.IsOpen);
            }

        }

        [Test]
        public void LoadDspUnits()
        {
            if (!amp.IsOpen)
            {
                var wait = new AutoResetEvent(false);
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
            }
            var dspUnits = LtAmpDevice.DspUnitDefinitions.GroupBy(x => x.Info.SubCategory).ToDictionary(y => y.Key, y => y.ToList());
            foreach (var unitType in dspUnits)
            {
                Console.WriteLine($"{unitType.Key}: {unitType.Value.Count}");
            }
            Assert.IsTrue(LtAmpDevice.DspUnitDefinitions.Count > 0);
        }

        [Test]
        public void GetFirmwareVersion()
        {
            var wait = new AutoResetEvent(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }

            string messageVersion = "";
            amp.FirmwareVersionStatusMessageReceived += (message) =>
            {
                messageVersion = message.Version;
                wait.Set();
            };
            amp.GetFirmwareVersion();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Firmware Version: {LtAmpDevice.DeviceInfo.FirmwareVersion}");
            Console.WriteLine($"Message data: {messageVersion}");
            Assert.That(messageVersion, Is.EqualTo(LtAmpDevice.DeviceInfo.FirmwareVersion));
            Assert.That(messageVersion, Is.EqualTo(expectedValues.firmwareVersion));
        }

        [Test]
        public void GetProductInformationVersion()
        {
            var wait = new AutoResetEvent(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }

            string messageId = "";
            amp.ProductIdentificationStatusMessageReceived += (message) =>
            {
                messageId = message.Id;
                wait.Set();
            };
            amp.GetProductIdentification();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"ProductId: {LtAmpDevice.DeviceInfo.ProductId}");
            Console.WriteLine($"Message data: {messageId}");
            Assert.That(messageId, Is.EqualTo(LtAmpDevice.DeviceInfo.ProductId));
            Assert.That(messageId, Is.EqualTo(expectedValues.productId));
        }

        [Test]
        public void SetFootswitch()
        {
            var wait = new AutoResetEvent(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }

            uint slotA = 0;
            uint slotB = 0;
            amp.QASlotsStatusMessageReceived += (message) =>
            {
                slotA = message.Slots[0];
                slotB = message.Slots[1];
                wait.Set();
            };
            amp.SetQASlots(new uint[] { expectedValues.slotA, expectedValues.slotB });
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"Slot A: {expectedValues.slotA}, {slotA}");
            Console.Write($"Slot B: {expectedValues.slotB}, {slotB}");
            Assert.That(slotA, Is.EqualTo(expectedValues.slotA));
            Assert.That(slotB, Is.EqualTo(expectedValues.slotB));
        }

        [Test]
        public void SetUsbGain()
        {
            var wait = new AutoResetEvent(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }

            float valueDb = 0;
            amp.UsbGainStatusMessageReceived += (message) =>
            {
                valueDb = message.ValueDB;
                wait.Set();
            };
            amp.SetUsbGain(expectedValues.usbGain);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"USB Gain: {expectedValues.usbGain}");
            Assert.That(valueDb, Is.InRange(expectedValues.usbGain - 0.01, expectedValues.usbGain + 0.01));
        }

        [Test]
        public void Tuner()
        {
            var wait = new AutoResetEvent(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }

            int modalContextReceived = -1;
            int modalStateReceived = -1;
            amp.ModalStatusMessageMessageReceived += (message) =>
            {
                modalContextReceived = (int)message.Context;
                modalStateReceived = (int)message.State;
                wait.Set();
            };
            amp.SetModalState(ModalContext.TunerEnable);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"{(ModalContext)modalContextReceived}: {(ModalState)modalStateReceived}");
            Assert.That((ModalContext)modalContextReceived, Is.EqualTo(ModalContext.TunerEnable));
            Assert.That((ModalState)modalStateReceived, Is.EqualTo(ModalState.Ok));
            Thread.Sleep(5000);
            wait.Reset();
            amp.SetModalState(ModalContext.TunerDisable);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"{(ModalContext)modalContextReceived}: {(ModalState)modalStateReceived}");
            Assert.That((ModalContext)modalContextReceived, Is.EqualTo(ModalContext.TunerDisable));
            Assert.That((ModalState)modalStateReceived, Is.EqualTo(ModalState.Ok));


        }
    }


    public class ExpectedValues
    {
        public string firmwareVersion { get; set; }
        public string productId { get; set; }
        public uint slotA { get; set; }
        public uint slotB { get; set; }
        public float usbGain { get; set; }

        public static ExpectedValues Load()
        {
            var filePath = Path.Join(Environment.CurrentDirectory, "expectedValues.json");
            return JsonConvert.DeserializeObject<ExpectedValues>(File.ReadAllText(filePath));

        }
    }

}
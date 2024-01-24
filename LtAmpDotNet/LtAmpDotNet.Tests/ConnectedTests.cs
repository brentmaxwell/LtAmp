using LtAmpDotNet.Lib;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System.Security.Cryptography.X509Certificates;

namespace LtAmpDotNet.Tests
{
    [TestFixture]
    public class ConnectedTests
    {
        private LtAmpDevice amp;
        private MockDeviceState mockDeviceState;

        public delegate void TestCallback(bool isConnected);

        [OneTimeSetUp]
        public void Setup()
        {
            mockDeviceState = MockDeviceState.Load();
            amp = new LtAmpDevice();
        }
        
        [OneTimeTearDown]
        public void Teardown()
        {
            if(amp.IsOpen)
            {
                amp.Close();
            }
            amp.Dispose();
        }

        public void Open(TestCallback callback)
        {
            amp.DeviceConnected += (sender, eventArgs) => { callback(true); };
            amp.Open();
        }
        
        
        [Test]
        [Category("Initialization")]
        public void LoadDspUnits()
        {
            var dspUnits = LtAmpDevice.DspUnitDefinitions.GroupBy(x => x.Info.SubCategory).ToDictionary(y => y.Key, y => y.ToList());
            foreach (var unitType in dspUnits)
            {
                Console.WriteLine($"{unitType.Key}: {unitType.Value.Count}");
            }
            Assert.IsTrue(LtAmpDevice.DspUnitDefinitions.Count > 0);
        }

        [Test, Explicit]
        [Category("Initialization")]
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

        [Test, Explicit]
        [Category("Device Information")]
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
            Assert.That(messageVersion, Is.EqualTo(mockDeviceState.firmwareVersion));
        }

        [Test, Explicit]
        [Category("Device Information")]
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
            Assert.That(messageId, Is.EqualTo(mockDeviceState.productId));
        }

        [Test, Explicit]
        [Category("Device Settings")]
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

            uint[] slots = {0, 0};
            amp.QASlotsStatusMessageReceived += (message) =>
            {
                slots = message.Slots.ToArray();
                wait.Set();
            };
            amp.SetQASlots(mockDeviceState.qaSlots);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"Slot A: {mockDeviceState.qaSlots[0]}, {slots[0]}");
            Console.Write($"Slot A: {mockDeviceState.qaSlots[1]}, {slots[1]}");
            Assert.That(slots, Is.EqualTo(mockDeviceState.qaSlots));
        }

        [Test, Explicit]
        [Category("Device Settings")]
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
            amp.SetUsbGain(mockDeviceState.usbGain);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"USB Gain: {mockDeviceState.usbGain}");
            Assert.That(valueDb, Is.InRange(mockDeviceState.usbGain - 0.01, mockDeviceState.usbGain + 0.01));
        }

        [Test, Explicit]
        [Category("Device Settings")]
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
}
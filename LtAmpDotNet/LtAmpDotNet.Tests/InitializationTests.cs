using HidSharp;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Tests.Mock;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System.Security.Cryptography.X509Certificates;

namespace LtAmpDotNet.Tests
{
    [TestFixture]
    public class InitializationTests
    {
        private LtAmpDevice amp;
        private MockDeviceState mockDeviceState;

        public delegate void TestCallback(bool isConnected);

        [OneTimeSetUp]
        public void Setup()
        {
            mockDeviceState = MockDeviceState.Load();
            amp = new LtAmpDevice(new MockHidDevice(mockDeviceState));
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
        [Order(1)]
        public void OpenAndConnect()
        {
            var wait = new AutoResetEvent(false);
            List<FenderMessageLT> messages = new List<FenderMessageLT>();
            amp.MessageReceived += (message) =>
            {
                if(!(message.TypeCase == FenderMessageLT.TypeOneofCase.ModalStatusMessage && message.ModalStatusMessage.Context == ModalContext.SyncBegin))
                {
                    messages.Add(message);
                }
                wait.Set();
            };

            Open(callback =>
            {
                wait.Set();
            });
            for (var i = 0; i < 5; i++)
            {
                wait.WaitOne(TimeSpan.FromSeconds(5));
                Assert.That(messages[i].ToString(), Is.EqualTo(mockDeviceState?.initializationStrings![i]));
            }
        }

        [Test]
        [Category("Initialization")]
        [Order(2)]
        public void LoadDspUnits()
        {
            var dspUnits = LtAmpDevice.DspUnitDefinitions?.GroupBy(x => x.Info.SubCategory).ToDictionary(y => y.Key, y => y.ToList());
            foreach (var unitType in dspUnits!)
            {
                Console.WriteLine($"{unitType.Key}: {unitType.Value.Count}");
            }
            Assert.That(LtAmpDevice.DspUnitDefinitions?.Count, Is.GreaterThan(0));
        }

        [Test]
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
            Console.WriteLine($"Message data: {messageVersion}");
            Assert.That(messageVersion, Is.EqualTo(mockDeviceState.firmwareVersion));
        }

        [Test]
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
            Console.WriteLine($"Message data: {messageId}");
            Assert.That(messageId, Is.EqualTo(mockDeviceState.productId));
        }

        [Test]
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
            amp.SetQASlots(mockDeviceState?.qaSlots!);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"Slot A: {mockDeviceState?.qaSlots![0]}, {slots[0]}");
            Console.Write($"Slot A: {mockDeviceState?.qaSlots![1]}, {slots[1]}");
            Assert.That(slots, Is.EqualTo(mockDeviceState?.qaSlots));
        }

        [Test]
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
            amp.SetUsbGain(mockDeviceState.usbGain.GetValueOrDefault());
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"USB Gain: {mockDeviceState.usbGain}");
            Assert.That(valueDb, Is.InRange(mockDeviceState.usbGain - 0.01, mockDeviceState.usbGain + 0.01));
        }


        [Test, Explicit]
        [Category("Device Information")]
        public void GetPreset([Range(1,60)] int index)
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

            int slotIndex = 0;
            Preset? preset = null;
            amp.PresetJSONMessageReceived += (message) =>
            {
                slotIndex = message.SlotIndex;
                preset = JsonConvert.DeserializeObject<Preset>(message.Data);
                wait.Set();
            };
            amp.GetPreset(index);
            Preset? expectedPreset = JsonConvert.DeserializeObject<Preset>(mockDeviceState?.Presets![slotIndex - 1]!);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Slot Index: {slotIndex}");
            Console.WriteLine(JsonConvert.SerializeObject(preset, Formatting.Indented));
            Assert.That(index, Is.EqualTo(slotIndex));
            Assert.That(preset, Is.Not.Null.And.InstanceOf<Preset>());
            Assert.That(preset.Info.DisplayName, Is.EqualTo(expectedPreset?.Info.DisplayName));
        }

        [Test]
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
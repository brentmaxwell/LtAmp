using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;
using LtAmpDotNet.Tests.Mock;
using Newtonsoft.Json;

namespace LtAmpDotNet.Tests
{
    [TestFixture]
    public class InitializationTests
    {
        private LtAmplifier amp;
        private MockDeviceState mockDeviceState;

        public delegate void TestCallback(bool isConnected);

        [OneTimeSetUp]
        public void Setup()
        {
            mockDeviceState = MockDeviceState.Load();
            amp = new LtAmplifier(new MockHidDevice(mockDeviceState));
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            if (amp.IsOpen)
            {
                amp.Close();
            }
            amp.Dispose();
        }

        public void Open(TestCallback callback)
        {
            amp.AmplifierConnected += (sender, eventArgs) => { callback(true); };
            amp.Open();
        }


        [Test]
        [Category("Initialization")]
        [Order(1)]
        public void OpenAndConnect()
        {
            AutoResetEvent wait = new AutoResetEvent(false);
            List<FenderMessageLT> messages = [];
            amp.MessageReceived += (sender, eventArgs) =>
            {
                if (!(eventArgs.MessageType == FenderMessageLT.TypeOneofCase.ModalStatusMessage && eventArgs.Message?.ModalStatusMessage.Context == ModalContext.SyncBegin))
                {
                    messages.Add(eventArgs.Message);
                }
                wait.Set();
            };
            Open(callback =>
            {
                wait.Set();
            });
            wait.WaitOne(TimeSpan.FromSeconds(5));
            for (int i = 0; i < 5; i++)
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
            Dictionary<string, List<Lib.Model.Profile.DspUnitDefinition>>? dspUnits = LtAmplifier.DspUnitDefinitions?.GroupBy(x => x.Info!.SubCategory!).ToDictionary(y => y.Key, y => y.ToList());
            foreach (KeyValuePair<string, List<Lib.Model.Profile.DspUnitDefinition>> unitType in dspUnits!)
            {
                Console.WriteLine($"{unitType.Key}: {unitType.Value.Count}");
            }
            Assert.That(LtAmplifier.DspUnitDefinitions.Where(x => x.Info.SubCategory == "amp").Count, Is.EqualTo(20));
            Assert.That(LtAmplifier.DspUnitDefinitions.Where(x => x.Info.SubCategory == "stomp").Count, Is.EqualTo(11));
            Assert.That(LtAmplifier.DspUnitDefinitions.Where(x => x.Info.SubCategory == "mod").Count, Is.EqualTo(7));
            Assert.That(LtAmplifier.DspUnitDefinitions.Where(x => x.Info.SubCategory == "delay").Count, Is.EqualTo(3));
            Assert.That(LtAmplifier.DspUnitDefinitions.Where(x => x.Info.SubCategory == "reverb").Count, Is.EqualTo(5));
            Assert.That(LtAmplifier.DspUnitDefinitions.Where(x => x.Info.SubCategory == "utility").Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Device Information")]
        public void GetFirmwareVersion()
        {
            AutoResetEvent wait = new AutoResetEvent(false);
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
            amp.FirmwareVersionStatusMessageReceived += (sender, eventArgs) =>
            {
                messageVersion = eventArgs.Message.FirmwareVersionStatus.Version;
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
            AutoResetEvent wait = new AutoResetEvent(false);
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
            amp.ProductIdentificationStatusMessageReceived += (sender, eventArgs) =>
            {
                messageId = eventArgs.Message.ProductIdentificationStatus.Id;
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
            AutoResetEvent wait = new AutoResetEvent(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }

            uint[] slots = { 0, 0 };
            amp.QASlotsStatusMessageReceived += (sender, eventArgs) =>
            {
                slots = eventArgs.Message.QASlotsStatus.Slots.ToArray();
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
            AutoResetEvent wait = new AutoResetEvent(false);
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
            amp.UsbGainStatusMessageReceived += (sender, eventArgs) =>
            {
                valueDb = eventArgs.Message.UsbGainStatus.ValueDB;
                wait.Set();
            };
            amp.SetUsbGain(mockDeviceState.usbGain.GetValueOrDefault());
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"USB Gain: {mockDeviceState.usbGain}");
            Assert.That(valueDb, Is.InRange(mockDeviceState.usbGain - 0.01, mockDeviceState.usbGain + 0.01));
        }


        [Test]
        [Category("Presets")]
        public void GetPreset([Range(1, 60)] int index)
        {
            AutoResetEvent wait = new AutoResetEvent(false);
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
            amp.PresetJSONMessageReceived += (sender, eventArgs) =>
            {
                slotIndex = eventArgs.Message.PresetJSONMessage.SlotIndex;
                preset = JsonConvert.DeserializeObject<Preset>(eventArgs.Message.PresetJSONMessage.Data);
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
        [Category("Device Features")]
        public void Tuner()
        {
            AutoResetEvent wait = new AutoResetEvent(false);
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
            amp.ModalStatusMessageMessageReceived += (sender, eventArgs) =>
            {
                modalContextReceived = (int)eventArgs.Message.ModalStatusMessage.Context;
                modalStateReceived = (int)eventArgs.Message.ModalStatusMessage.State;
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
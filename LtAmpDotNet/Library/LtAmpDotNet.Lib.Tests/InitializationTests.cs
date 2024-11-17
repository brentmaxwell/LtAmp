using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;

namespace LtAmpDotNet.Lib.Tests
{
    [TestFixture]
    public class InitializationTests
    {
        private const bool useMock = true;
        private LtAmplifier amp;
        private MockDeviceState mockDeviceState;

        public delegate void TestCallback(bool isConnected);

        [OneTimeSetUp]
        public void Setup()
        {
            mockDeviceState = MockDeviceState.Load();
            amp = useMock ? new LtAmplifier(new MockHidDevice(mockDeviceState))
                : new LtAmplifier(new UsbAmpDevice());
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

        private void Open(TestCallback callback)
        {
            amp.AmplifierConnected += (sender, eventArgs) => { callback(true); };
            amp.Open();
        }

        [Test]
        [Category("Initialization")]
        [Category("Physical Amp Tests")]
        [Order(2)]
        public void OpenAndConnect()
        {
            AutoResetEvent wait = new(false);
            List<FenderMessageLT> messages = [];
            amp.MessageReceived += (sender, eventArgs) =>
            {
                if (!(eventArgs.MessageType == FenderMessageLT.TypeOneofCase.ModalStatusMessage && eventArgs.Message?.ModalStatusMessage.Context == ModalContext.SyncBegin))
                {
                    messages.Add(eventArgs.Message!);
                }
                wait.Set();
            };
            Open(callback =>
            {
                wait.Set();
            });
            wait.WaitOne(TimeSpan.FromSeconds(5));
            for (int i = 0; i < mockDeviceState?.initializationStrings!.Count; i++)
            {
                wait.WaitOne(TimeSpan.FromSeconds(5));
                Assert.That(messages[i].ToString(), Is.EqualTo(mockDeviceState?.initializationStrings![i]));
            }
        }

        [Test]
        [Category("Initialization")]
        [Category("Library")]
        [Order(1)]
        public void LoadDspUnits()
        {
            Dictionary<string, List<Model.Profile.DspUnitDefinition>>? dspUnits = LtAmplifier.DspUnitDefinitions?.GroupBy(x => x.Info!.SubCategory!).ToDictionary(y => y.Key, y => y.ToList());
            foreach (KeyValuePair<string, List<Model.Profile.DspUnitDefinition>> unitType in dspUnits!)
            {
                Console.WriteLine($"{unitType.Key}: {unitType.Value.Count}");
            }
            Assert.That(LtAmplifier.DspUnitDefinitions!.Where(x => x.Info?.SubCategory == "amp").Count, Is.EqualTo(20));
            Assert.That(LtAmplifier.DspUnitDefinitions!.Where(x => x.Info?.SubCategory == "stomp").Count, Is.EqualTo(11));
            Assert.That(LtAmplifier.DspUnitDefinitions!.Where(x => x.Info?.SubCategory == "mod").Count, Is.EqualTo(7));
            Assert.That(LtAmplifier.DspUnitDefinitions!.Where(x => x.Info?.SubCategory == "delay").Count, Is.EqualTo(3));
            Assert.That(LtAmplifier.DspUnitDefinitions!.Where(x => x.Info?.SubCategory == "reverb").Count, Is.EqualTo(5));
            Assert.That(LtAmplifier.DspUnitDefinitions!.Where(x => x.Info?.SubCategory == "utility").Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Device Information")]
        [Category("Physical Amp Tests")]
        [Order(3)]
        public void GetFirmwareVersion()
        {
            AutoResetEvent wait = new(false);
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
                messageVersion = eventArgs.Message!.FirmwareVersionStatus.Version;
                wait.Set();
            };
            amp.GetFirmwareVersion();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Message data: {messageVersion}");
            Assert.That(messageVersion, Is.EqualTo(mockDeviceState.firmwareVersion));
        }

        [Test]
        [Category("Device Information")]
        [Category("Physical Amp Tests")]
        [Order(4)]
        public void GetProductInformationVersion()
        {
            AutoResetEvent wait = new(false);
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
                messageId = eventArgs.Message!.ProductIdentificationStatus.Id;
                wait.Set();
            };
            amp.GetProductIdentification();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Message data: {messageId}");
            Assert.That(messageId, Is.EqualTo(mockDeviceState.productId));
        }

        [Test]
        [Category("Device Settings")]
        [Order(5)]
        public void SetFootswitch()
        {
            AutoResetEvent wait = new(false);
            if (!amp.IsOpen)
            {
                Open(callback =>
                {
                    wait.Set();
                });
                wait.WaitOne(TimeSpan.FromSeconds(5));
                wait.Reset();
            }
            uint[] slots = new uint[2];
            amp.QASlotsStatusMessageReceived += (sender, eventArgs) =>
            {
                slots = [.. eventArgs.Message!.QASlotsStatus.Slots ];
                wait.Set();
            };
            amp.GetQASlots();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            uint[] originalSlots = slots;
            amp.SetQASlots(mockDeviceState?.qaSlots!);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Slot A: {mockDeviceState?.qaSlots![0]}, {slots[0]}");
            Console.WriteLine($"Slot B: {mockDeviceState?.qaSlots![1]}, {slots[1]}");
            Assert.That(slots, Is.EqualTo(mockDeviceState?.qaSlots));
            amp.SetQASlots(originalSlots);
        }

        [Test]
        [Category("Device Settings")]
        [Order(6)]
        public void SetUsbGain()
        {
            AutoResetEvent wait = new(false);
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
                valueDb = eventArgs.Message!.UsbGainStatus.ValueDB;
                wait.Set();
            };
            amp.SetUsbGain(mockDeviceState.usbGain.GetValueOrDefault());
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.Write($"USB Gain: {mockDeviceState.usbGain}");
            Assert.That(valueDb, Is.InRange(mockDeviceState.usbGain! - 0.01, mockDeviceState.usbGain! + 0.01));
            amp.SetUsbGain(0);
        }

        [Test]
        [Category("Presets")]
        [Order(7)]
        public void GetPreset([Range(1, 60)] int index)
        {
            AutoResetEvent wait = new(false);
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
                slotIndex = eventArgs.Message!.PresetJSONMessage.SlotIndex;
                preset = JsonConvert.DeserializeObject<Preset>(eventArgs.Message.PresetJSONMessage.Data);
                wait.Set();
            };
            amp.GetPreset(index);
            Preset? expectedPreset = JsonConvert.DeserializeObject<Preset>(mockDeviceState?.Presets![index-1]!);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Slot Index: {slotIndex}");
            Console.WriteLine(JsonConvert.SerializeObject(preset, Formatting.Indented));
            Assert.Multiple(() =>
            {
                Assert.That(index, Is.EqualTo(slotIndex));
                Assert.That(preset, Is.Not.Null.And.InstanceOf<Preset>());
                if (useMock)
                {
                    Assert.That(preset!.Info.DisplayName, Is.EqualTo(expectedPreset?.Info?.DisplayName));
                }
            });
        }

        [Test]
        [Category("Device Features")]
        [Order(8)]
        public void Tuner()
        {
            AutoResetEvent wait = new(false);
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
                modalContextReceived = (int)eventArgs.Message!.ModalStatusMessage.Context;
                modalStateReceived = (int)eventArgs.Message.ModalStatusMessage.State;
                wait.Set();
            };
            amp.SetModalState(ModalContext.TunerEnable);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"{(ModalContext)modalContextReceived}: {(ModalState)modalStateReceived}");
            Assert.Multiple(() =>
            {
                Assert.That((ModalContext)modalContextReceived, Is.EqualTo(ModalContext.TunerEnable));
                Assert.That((ModalState)modalStateReceived, Is.EqualTo(ModalState.Ok));
            });
            Thread.Sleep(5000);
            wait.Reset();
            amp.SetModalState(ModalContext.TunerDisable);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            Console.WriteLine($"{(ModalContext)modalContextReceived}: {(ModalState)modalStateReceived}");
            Assert.Multiple(() =>
            {
                Assert.That((ModalContext)modalContextReceived, Is.EqualTo(ModalContext.TunerDisable));
                Assert.That((ModalState)modalStateReceived, Is.EqualTo(ModalState.Ok));
            });
        }
    }
}
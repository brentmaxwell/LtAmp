using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UsbipDevice;

namespace LtAmpVirtualUsb
{
    internal class FenderMustangLT25 : IDisposable
    {

        Usbip _device;

        byte[] _reportDescriptor;
        bool _hasReportId = false;

        string _manufacturer = "Mustang LT 25";

        byte[] report = new byte[]
        { 0x06, 0x00, 0xff, 0x09, 0x01, 0xa1, 0x01, 0x09, 0x02, 0x15, 0x80, 0x25, 0x7f, 0x95, 0x40, 0x75, 0x08, 0x81, 0x02, 0x09, 0x03, 0x15, 0x80, 0x25, 0x7f, 0x95, 0x40, 0x75, 0x08, 0x91, 0x02, 0xc0 };

        public bool Connected
        {
            get { return _device.Connected; }
        }
        public FenderMustangLT25(Usbip device, byte[] reportDescriptor)
        {
            _device = device;

            _reportDescriptor = reportDescriptor;
            _hasReportId = ReportDescEnumerator.HasReportId(_reportDescriptor);
        }

        public byte GetReportId()
        {
            if (_hasReportId == false)
            {
                return 0;
            }

            return KeyboardMouseDescriptors.KeyboardReportId;
        }

        byte[] GetBuffer(byte reportId)
        {
            int bufferSize = ReportDescEnumerator.GetReportSize(_reportDescriptor, reportId);
            if (bufferSize > 8)
            {
                bufferSize = 8;
            }

            byte[] buffer = new byte[bufferSize];

            buffer[0] = reportId;

            return buffer;
        }

        private string ReadCommand(string txt, ref int index)
        {
            if (txt.IndexOf(">", index) == -1)
            {
                return null;
            }

            List<char> cmd = new List<char>();

            index++;

            while (txt.Length > index)
            {
                if (txt[index] == '>')
                {
                    break;
                }

                cmd.Add(txt[index]);
                index++;
            }

            return new string(cmd.ToArray());
        }

        public USB_DEVICE_DESCRIPTOR deviceDescriptor = new USB_DEVICE_DESCRIPTOR
        {
            bLength = 18,
            bDescriptorType = 0x01,
            bcdUSB = 0x0200,
            bDeviceClass = 0x00,
            bDeviceSubClass = 0,
            bDeviceProtocol = 0,
            bMaxPacketSize0 = 64,
            idVendor = 0x1ed8,
            idProduct = 0x0037,
            bcdDevice = 0x0200,
            iManufacturer = 1,
            iProduct = 2,
            iSerialNumber = 3,
            bNumConfigurations = 1
        };

        public USB_CONFIGURATION_DESCRIPTOR configurationDescriptor = new USB_CONFIGURATION_DESCRIPTOR
        {
            bLength = 9,
            bDescriptorType = 0x02,
            wTotalLength = 132,
            bNumInterfaces = 3,
            bConfigurationValue = 1,
            iConfiguration = 0,
            bmAttributes = 0xc0,
            bMaxPower = 50
        };

        public USB_INTERFACE_DESCRIPTOR interfaceDescriptor1 = new USB_INTERFACE_DESCRIPTOR
        {
            bLength = 9,
            bDescriptorType = 0x04,
            bInterfaceNumber = 0,
            bAlternateSetting = 0,
            bNumEndpoints = 2,
            bInterfaceClass = 0x03,
            bInterfaceSubClass = 0x00,
            bInterfaceProtocol = 0x00,
            iInterface = 0
        };

        public USB_HID_DESCRIPTOR hidDescriptor = new USB_HID_DESCRIPTOR
        {
            bLength = 9,
            bDescriptorType = 0x21,
            bcdHID = 0x0111,
            bCountryCode = 0x00,
            bNumDescriptors = 1,
            bRPDescriptorType = 0x22,
            wRPDescriptorLength = 32
        };

        public USB_ENDPOINT_DESCRIPTOR endpointDescriptor1 = new USB_ENDPOINT_DESCRIPTOR
        {
            bLength = 7,
            bDescriptorType = 0x05,
            bEndpointAddress = 0x81,
            bmAttributes = 0x03,
            wMaxPacketSize = 64,
            bInterval = 1
        };

        public USB_ENDPOINT_DESCRIPTOR endpointDescriptor2 = new USB_ENDPOINT_DESCRIPTOR
        {
            bLength = 7,
            bDescriptorType = 0x05,
            bEndpointAddress = 0x01,
            bmAttributes = 0x03,
            wMaxPacketSize = 64,
            bInterval = 1
        };

        public USB_INTERFACE_DESCRIPTOR interfaceDescriptor2 = new USB_INTERFACE_DESCRIPTOR
        {
            bLength = 9,
            bDescriptorType = 0x04,
            bInterfaceNumber = 2,
            bAlternateSetting = 0,
            bNumEndpoints = 0,
            bInterfaceClass = 0x01,
            bInterfaceSubClass = 0x01,
            bInterfaceProtocol = 0x00,
            iInterface = 0
        };

        //Class-specific Audio Control Interface Descriptor: Header Descriptor
        //    bLength: 9
        //    bDescriptorType: 0x24 (audio class interface)
        //    Subtype: Header Descriptor(0x01)
        //    Version: 1.00
        //    Total length: 3
        //    Total number of interfaces: 1
        //    Interface number: 1
        public byte[] audioControlInterfaceDescriotor_HeaderDescriptor =
            new byte[] { 0x09, 0x24, 0x01, 0x00, 0x01, 0x1E, 0x00, 0x01, 0x01 };
        

        //Class-specific Audio Control Interface Descriptor: Input terminal descriptor
        //    bLength: 12
        //    bDescriptorType: 0x24 (audio class interface)
        //    Subtype: Input terminal descriptor(0x02)
        //    Terminal ID: 1
        //    Terminal Type: Input Undefined(0x0200)
        //    Assoc Terminal: 0
        //    Number Channels: 2
        //    Channel Config: 0x0003, Left Front, Right Front
        //    Channel Names: 0
        //    String descriptor index: 0
        public byte[] audioControlInterfaceDescriotor_InputTerminalDescriptor =
            new byte[] { 0x0c, 0x24, 0x02, 0x01, 0x00, 0x02, 0x00, 0x02, 0x03, 0x00, 0x00, 0x00 };
        

        //Class-specific Audio Control Interface Descriptor: Output terminal descriptor
        //    bLength: 9
        //    bDescriptorType: 0x24 (audio class interface)
        //    Subtype: Output terminal descriptor(0x03)
        //    Terminal ID: 2
        //    Terminal Type: USB Streaming(0x0101)
        //    Assoc Terminal: 0
        //    Source ID: 1
        //    String descriptor index: 0
        public byte[] audioControlInterfaceDescriotor_OutputTerminalDescriptor =
            new byte[] { 0x09, 0x24, 0x03, 0x02, 0x01, 0x01, 0x00, 0x01, 0x00 };
        
        public USB_INTERFACE_DESCRIPTOR interfaceDescriptor3 = new USB_INTERFACE_DESCRIPTOR
        {
            bLength = 9,
            bDescriptorType = 0x04,
            bInterfaceNumber = 1,
            bAlternateSetting = 0,
            bNumEndpoints = 0,
            bInterfaceClass = 0x01,
            bInterfaceSubClass = 0x02,
            bInterfaceProtocol = 0x00,
            iInterface = 0
        };

        public USB_INTERFACE_DESCRIPTOR interfaceDescriptor4 = new USB_INTERFACE_DESCRIPTOR
        {
            bLength = 9,
            bDescriptorType = 0x04,
            bInterfaceNumber = 1,
            bAlternateSetting = 1,
            bNumEndpoints = 1,
            bInterfaceClass = 0x01,
            bInterfaceSubClass = 0x02,
            bInterfaceProtocol = 0x00,
            iInterface = 0
        };

        //Class-specific Audio Streaming Interface Descriptor: General AS Descriptor
        //    bLength: 7
        //    bDescriptorType: 0x24 (audio class interface)
        //    Subtype: General AS Descriptor(0x01)
        //    Connected Terminal ID: 2
        //    Interface delay in frames: 1
        //    Format: PCM(0x0001)
        public byte[] audioStreamingInterfaceDescriptor_GeneralAsDescriptor =
            new byte[] { 0x07, 0x24, 0x01, 0x02, 0x01, 0x01, 0x00 };

        //Class-specific Audio Streaming Interface Descriptor: Format type descriptor
        //    bLength: 11
        //    bDescriptorType: 0x24 (audio class interface)
        //    Subtype: Format type descriptor(0x02)
        //    FormatType: 1
        //    Number Channels: 2
        //    Subframe Size: 2
        //    Bit Resolution: 16
        //    Samples Frequence Type: 1
        //    Samples Frequence: 48000
        public byte[] audioStreamingInterfaceDescriptor_FormatTypeDescriptor =
            new byte[] { 0x0b, 0x24, 0x02, 0x01, 0x02, 0x02, 0x10, 0x01, 0x80, 0xbb, 0x00 };        

        //ENDPOINT DESCRIPTOR
        //    bLength: 9
        //    bDescriptorType: 0x05 (ENDPOINT)
        //    bEndpointAddress: 0x82  IN Endpoint:2
        //    bmAttributes: 0x05
        //    wMaxPacketSize: 196
        //    bInterval: 1
        //    bRefresh: 0
        //    bSynchAddress: 0
        public byte[] endpointDescriptor3 =
            new byte[] { 0x09, 0x05, 0x82, 0x05, 0xc4, 0x00, 0x01, 0x00, 0x00 };

        //Class-specific Audio Streaming Endpoint Descriptor
        //    bLength: 7
        //    bDescriptorType: 0x25 (audio class endpoint)
        //    Subtype: General Descriptor(0x01)
        //    Attributes: 0x00
        //    Lock Delay Units: Undefined(0)
        //    Lock Delay: 0
        public byte[] audioStreamingEndpointDescriptor =
            new byte[] { 0x07, 0x25, 0x01, 0x00, 0x00, 0x00, 0x00 };

        

        public void Dispose()
        {
            _device.Dispose();
        }

    }
}

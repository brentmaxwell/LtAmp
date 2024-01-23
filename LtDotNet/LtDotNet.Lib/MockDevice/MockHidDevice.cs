using HidSharp;
using HidSharp.Reports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtDotNet.Lib.MockDevice
{
    internal class MockHidDevice : HidDevice
    {
        public MockHidDevice()
        {
        }

        public override string DevicePath => "MOCKDEVICE";

        public override bool CanOpen => true;

        public override int ProductID => LtDeviceInfo.PRODUCT_ID;

        public override int ReleaseNumberBcd => throw new NotImplementedException();

        public override int VendorID => LtDeviceInfo.VENDOR_ID;

        public override string GetDeviceString(int index)
        {
            throw new NotImplementedException();
        }

        public override string GetFileSystemName()
        {
            throw new NotImplementedException();
        }

        public override string GetFriendlyName()
        {
            throw new NotImplementedException();
        }

        public override string GetManufacturer()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxFeatureReportLength()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxInputReportLength()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxOutputReportLength()
        {
            throw new NotImplementedException();
        }

        public override string GetProductName()
        {
            throw new NotImplementedException();
        }

        public override string GetSerialNumber()
        {
            throw new NotImplementedException();
        }

        protected override DeviceStream OpenDeviceDirectly(OpenConfiguration openConfig)
        {
            throw new NotImplementedException();
        }

        public ReportDescriptor GetReportDescriptor()
        {
            return new ReportDescriptor(GetRawReportDescriptor());
        }

        public virtual byte[] GetRawReportDescriptor()
        {
            throw new NotSupportedException();
        }

        public virtual string[] GetSerialPorts()
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            string text = "(unnamed manufacturer)";
            try
            {
                text = GetManufacturer();
            }
            catch
            {
            }

            string text2 = "(unnamed product)";
            try
            {
                text2 = GetProductName();
            }
            catch
            {
            }

            string text3 = "(no serial number)";
            try
            {
                text3 = GetSerialNumber();
            }
            catch
            {
            }

            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} (VID {3}, PID {4}, version {5})", text, text2, text3, VendorID, ProductID, ReleaseNumber);
        }

        public bool TryOpen(out HidStream stream)
        {
            return TryOpen(null, out stream);
        }

        public bool TryOpen(OpenConfiguration openConfig, out HidStream stream)
        {
            DeviceStream stream2;
            bool result = TryOpen(openConfig, out stream2);
            stream = (HidStream)stream2;
            return result;
        }

        public override bool HasImplementationDetail(Guid detail)
        {
            if (!base.HasImplementationDetail(detail))
            {
                return detail == ImplementationDetail.HidDevice;
            }

            return true;
        }
    }
}

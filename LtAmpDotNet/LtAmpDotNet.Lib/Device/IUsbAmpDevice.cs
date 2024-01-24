using HidSharp.Reports;
using HidSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LtAmpDotNet.Lib.LtAmpDevice;

namespace LtAmpDotNet.Lib.Device
{
    public interface IUsbAmpDevice : IDisposable
    {
        public bool IsOpen { get; }
        public int? ReportLength { get; }

        
        public event EventHandler? Closed;
        public event MessageReceivedEventHandler? MessageReceived;
        public event MessageSentEventHandler? MessageSent;
        public void Open();
        public void Close();

        //public void Read();
        //public void Write(byte[] buffer);

        public void Write(FenderMessageLT message);

    }
}

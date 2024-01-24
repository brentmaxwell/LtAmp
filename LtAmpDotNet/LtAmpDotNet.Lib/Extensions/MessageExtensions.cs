using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Extensions
{
    public static class MessageExtensions
    {
        public static byte[][] ToUsbMessage(this IMessage message)
        {
            byte[] data = message.ToByteArray();
            var chunks = data.Split(61).ToList();
            byte[][] packets = new byte[chunks.Count()][];
            for (int i = 0; i < chunks.Count(); i++)
            {
                packets[i] = new byte[65];
                if (i + 1 == chunks.Count())
                {
                    packets[i][1] = 0x35;
                }
                else if (i == 0 && i + 1 < chunks.Count())
                {
                    packets[i][1] = 0x33;
                }
                else
                {
                    packets[i][1] = 0x34;
                }
                packets[i][2] = Convert.ToByte(chunks[i].Count());
                chunks[i].ToArray().CopyTo(packets[i], 3);
            }
            return packets;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] arr, int size)
        {
            return arr.Select((s, i) => arr.Skip(i * size).Take(size)).Where(a => a.Any());
        }
    }
}

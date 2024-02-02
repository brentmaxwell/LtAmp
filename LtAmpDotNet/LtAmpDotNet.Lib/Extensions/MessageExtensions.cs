using Google.Protobuf;
using System.Data;

namespace LtAmpDotNet.Lib.Extensions
{
    public static class MessageExtensions
    {
        public static byte[][] ToUsbMessage(this IMessage message, int packetLength = 65)
        {
            packetLength -= 4;
            byte[] data = message.ToByteArray();
            List<byte[]> chunks = data.Split(packetLength).ToList();
            byte[][] packets = new byte[chunks.Count()][];
            for (int i = 0; i < chunks.Count(); i++)
            {
                packets[i] = new byte[packetLength];
                packets[i][1] = i + 1 == chunks.Count() ? (byte)0x35 : i == 0 && i + 1 < chunks.Count() ? (byte)0x33 : (byte)0x34;
                packets[i][2] = Convert.ToByte(chunks[i].Count());
                chunks[i].ToArray().CopyTo(packets[i], 3);
            }
            return packets;
        }

        public static T[][] Split<T>(this T[] arr, int chunkSize)
        {
            return arr.Select((s, i) => arr.Skip(i * chunkSize).Take(chunkSize).ToArray()).Where(a => a.Any()).ToArray();

        }
    }
}

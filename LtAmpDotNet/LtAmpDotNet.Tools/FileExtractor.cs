using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Tools
{
    public class FileExtractor
    {
        private byte[] data;
        public FileExtractor(string path)
        {
            using (var file = File.OpenRead(path))
            {
                data = new byte[file.Length];
                file.Read(data, 0, data.Length);
                file.Close();
            }
        }

        public string[] Extract(string fileExt)
        {
            var locations = Locate(data, Encoding.ASCII.GetBytes($".{fileExt}"));
            foreach (var location in locations)
            {
                var cursor = location;
                cursor += $"{fileExt}".Length;
                cursor += data[cursor + 5];
                var startPos = cursor;
                while (data[startPos] != 0x0a)
                {
                    startPos--;
                }
                var tags = new byte[] { 0x12,0x1a,0x22,0x2a,0x32,0x3a,0x42,0x4a,0x50,0x58,0x62 };
                if (tags.Contains(data[cursor])){
                    continue;
                }
                while(cursor < data.Length && tags.Contains(data[cursor]){
                    tags = tags[tags.ToList().IndexOf(data[cursor])];

                }

            }
        }

        private int[] Locate(byte[] self, byte[] candidate)
        {
            if (IsEmptyLocate(self, candidate))
                return null;

            var list = new List<int>();

            for (int i = 0; i < self.Length; i++)
            {
                if (!IsMatch(self, i, candidate))
                    continue;

                list.Add(i);
            }

            return list.Count == 0 ? null : list.ToArray();
        }

        private bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > (array.Length - position))
                return false;

            for (int i = 0; i < candidate.Length; i++)
                if (array[position + i] != candidate[i])
                    return false;

            return true;
        }

        private bool IsEmptyLocate(byte[] array, byte[] candidate)
        {
            return array == null
                || candidate == null
                || array.Length == 0
                || candidate.Length == 0
                || candidate.Length > array.Length;
        }
    }
}

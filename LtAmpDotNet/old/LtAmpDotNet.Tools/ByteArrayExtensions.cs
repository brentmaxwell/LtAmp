namespace LtAmpDotNet.Tools
{
    public static class ByteArrayExtensions
    {
        public static int[]? Locate(this byte[] self, byte[] candidate)
        {
            if (IsEmptyLocate(self, candidate))
            {
                return null;
            }

            List<int> list = [];

            for (int i = 0; i < self.Length; i++)
            {
                if (!IsMatch(self, i, candidate))
                {
                    continue;
                }

                list.Add(i);
            }

            return list.Count == 0 ? null : list.ToArray();
        }

        private static bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > (array.Length - position))
            {
                return false;
            }

            for (int i = 0; i < candidate.Length; i++)
            {
                if (array[position + i] != candidate[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsEmptyLocate(byte[] array, byte[] candidate)
        {
            return array == null
                || candidate == null
                || array.Length == 0
                || candidate.Length == 0
                || candidate.Length > array.Length;
        }

        public static long FindPosition(this Stream stream, byte[] byteSequence, long start = 0)
        {
            if (byteSequence.Length > stream.Length || start > stream.Length)
            {
                return -1;
            }

            byte[] buffer = new byte[byteSequence.Length];

            BufferedStream bufStream = new(stream, byteSequence.Length);

            bufStream.Seek(start, SeekOrigin.Begin);
            int i;
            while ((i = bufStream.Read(buffer, 0, byteSequence.Length)) == byteSequence.Length)
            {
                if (byteSequence.SequenceEqual(buffer))
                {
                    return bufStream.Position - byteSequence.Length;
                }
                else
                {
                    bufStream.Position -= byteSequence.Length - PadLeftSequence(buffer, byteSequence);
                }
            }
            return -1;
        }

        private static int PadLeftSequence(byte[] bytes, byte[] seqBytes)
        {
            int i = 1;
            while (i < bytes.Length)
            {
                int n = bytes.Length - i;
                byte[] aux1 = new byte[n];
                byte[] aux2 = new byte[n];
                Array.Copy(bytes, i, aux1, 0, n);
                Array.Copy(seqBytes, aux2, n);
                if (aux1.SequenceEqual(aux2))
                {
                    return i;
                }

                i++;
            }
            return i;
        }
    }
}

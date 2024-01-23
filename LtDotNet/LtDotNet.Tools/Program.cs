﻿using Google.Protobuf;
using LtDotNet.Lib;
using LtDotNet.Lib.Model;
using System.Text;

namespace LtDotNet.Tools
{
    internal class Program
    {
        private static LtDevice amp = new LtDevice();
        static void Main(string[] args)
        {
            var input = args[0];
            var output = args[1];
            DecodeTestStrings(input, output);
        }

        public static void DecodeTestStrings(string inputFilename, string outputFilename)
        {
            
            var inputFile = File.OpenText(inputFilename);
            IEnumerable<byte[]> incomingData = new List<byte[]>();
            using (var outputFile = new StreamWriter(outputFilename))
            {
                while (!inputFile.EndOfStream)
                {
                    var line = inputFile.ReadLine().Split("\t");
                    var data = Convert.FromHexString(line[6]);
                    byte messageNum;
                    if (data.Length > 0)
                    {
                        if (data[0] == 0x00)
                        {
                            incomingData = incomingData.Append(data);
                        }
                        else
                        {
                            byte[] newData = new byte[65];
                            data.CopyTo(newData, 1);
                            data = newData;
                            incomingData = incomingData.Append(newData.ToArray());
                        }
                        if (data[1] == 0x35)
                        {
                            int finallength = incomingData.Sum(x => x[4]);
                            List<byte> finalByteArray = new List<byte>();
                            for (var i = 0; i < incomingData.Count(); i++)
                            {
                                finalByteArray.AddRange(incomingData.ToArray()[i].Skip(3).Take(incomingData.ToArray()[i][2]));
                            }
                            var message = FenderMessageLT.Parser.ParseFrom(finalByteArray.ToArray());

                            Console.WriteLine(message.ToString());
                            outputFile.WriteLine($"{line[0]}\t{(line[2] == "host" ? ">>" : "<<")}\t{message.ToString()}");
                            incomingData = new List<byte[]>();
                        }
                    }
                }
            }
        }
    }
}
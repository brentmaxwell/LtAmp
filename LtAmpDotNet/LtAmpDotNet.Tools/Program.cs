using Google.Protobuf;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text;

namespace LtAmpDotNet.Tools
{
    internal class Program
    {
        private static LtAmplifier amp = new LtAmplifier();
        static void Main(string[] args)
        {
            ExtractData(
                "C:\\Users\\Brent Maxwell\\OneDrive\\Projects\\FenderTone\\fenderTone\\Fender Tone LT Desktop.exe",
                "C:\\Users\\Brent Maxwell\\OneDrive\\Projects\\FenderTone\\fenderTone\\extracts");
            //amp = new Lib.LtAmplifier();
            //amp.MessageReceived += Amp_MessageReceived;
            //amp.MessageSent += Amp_MessageSent;
            ////amp.DeviceConnected += Amp_DeviceConnected;
            //amp.Open();
            //List<Preset>? presets = JsonConvert.DeserializeObject<List<Preset>>(File.ReadAllText(Path.Join(Environment.CurrentDirectory, "JsonDefinitions", "mustang", "default_presets.json")));
            //foreach (var preset in presets!)
            //{
            //    Console.WriteLine(preset);
            //}
            //var input = args[0];
            //var output = args[1];
            //DecodeTestStrings(input, output);
            //Console.Read();
            //foreach(var item in LtAmplifier.DspUnitDefinitions.OrderBy(x => x.Info.SubCategory))
            //{
            //    Console.WriteLine($"{item.Info.SubCategory}\t{item.FenderId}\t//{item.Info.DisplayName}");
            //}
        }

        private static void Amp_MessageSent(FenderMessageLT message)
        {
            Console.WriteLine($">> {message.ToString()}");
        }

        private static void Amp_DeviceConnected(object sender, EventArgs e)
        {
            amp.RenamePresetAt(4, "DOOKIE  CLEAN");
        }

        private static void Amp_MessageReceived(FenderMessageLT message)
        {
            Console.WriteLine($"<< {message.ToString()}");
        }

        public static void DecodeTestStrings(string inputFilename, string outputFilename)
        {

            var inputFile = File.OpenText(inputFilename);
            IEnumerable<byte[]> incomingData = new List<byte[]>();
            using (var outputFile = new StreamWriter(outputFilename))
            {
                while (!inputFile!.EndOfStream)
                {
                    var line = inputFile?.ReadLine()?.Split("\t");
                    var data = Convert.FromHexString(line![6]);
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

        public static void ExtractData(string filename, string outputPath)
        {
            var listStartOffset = 0x1B6A5A0;
            var listEndOffset = 0x1B78779;
            var filesStartOffset = 0x1B78780;
            var endBytes = new byte[] { 0xAE, 0x42, 0x60, 0x82 };
            var items = new List<string>();
            var filePositions = new List<long[]>();
            var files = new List<byte[]>();
            //var listBuffer = new byte[listEndOffset - listStartOffset];
            var listBuffer = new List<byte>();
            var fileBuffer = new List<byte>();
            using (var reader = new BinaryReader(new FileStream(filename, FileMode.Open)))
            {
                reader.BaseStream.Seek(listStartOffset, SeekOrigin.Begin);
                for (var i = listStartOffset; i < listEndOffset; i++)
                {
                    var bt = reader.ReadByte();
                    if (bt == 0 && listBuffer.Count != 0)
                    {
                        items.Add(Encoding.UTF8.GetString(listBuffer.ToArray()));
                        listBuffer.Clear();
                    }
                    else if (bt != 0)
                    {
                        listBuffer.Add(bt);
                    }
                }

                reader.BaseStream.Seek(filesStartOffset, SeekOrigin.Begin);

                long start = filesStartOffset;
                foreach(var item in items)
                {
                    var end = reader.BaseStream.FindPosition(endBytes, start);
                    filePositions.Add(new long[] { start, end });
                    long nullBytes = 0;
                    reader.BaseStream.Seek(end + 4,SeekOrigin.Begin);
                    while(reader.ReadByte() == 0)
                    {
                        nullBytes++;
                    }
                    Console.WriteLine($"{start} - {end} - {end-start}");
                    start = end + 4 + nullBytes;
                }
                for(var i = 0; i < filePositions.Count; i++)
                {
                    if (filePositions[i][1] > 0)
                    {
                        reader.BaseStream.Seek(filePositions[i][0], SeekOrigin.Begin);
                        var count = filePositions[i][1] - filePositions[i][0];
                        File.WriteAllBytes(Path.Combine(outputPath, items[i]), reader.ReadBytes((int)count));
                    }
                    Console.WriteLine($"{items[i]}");
                }

                //var length = stream.BaseStream.Length - filesStartOffset;
                //var j = 0;
                //while (stream.BaseStream.Position != stream.BaseStream.Length)
                //{
                //    var bt = stream.ReadByte();
                //    if(bt == endBytes[0])
                //    {
                //        if(stream.ReadBytes(3) == endBytes.Skip(1).Take(3).ToArray())
                //        {
                //            files.Add(fileBuffer.ToArray());
                //            fileBuffer.Clear();
                //            while(bt == 0)
                //            {
                //                bt = stream.ReadByte();
                //            }
                //        }
                //        fileBuffer.Add(bt);
                //    }
                //}
            }
            //for (var k = 0; k < items.Count; k++)
            //{
            //    Console.Write($"{items[k]}:{positions[k]}");
            //}
            //Console.WriteLine($"{items.Count}: {files.Count}");

            //using(var file = File.Open(Path.Combine(outputPath, "list.txt"),FileMode.OpenOrCreate))
            //{
            //    foreach(var item in filePositions)
            //    {
            //        file.Write(Encoding.UTF8.GetBytes($"{item.Key}: 0x{item.Value[0].ToString("X")} - 0x{item.Value[1].ToString("X")}\n"));

            //    }
            //}
//            File.WriteAllText(,String.Join("\n", items));

        }
    }
}
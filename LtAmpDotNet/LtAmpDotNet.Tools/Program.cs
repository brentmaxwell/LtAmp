using Google.Protobuf;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;

namespace LtAmpDotNet.Tools
{
    internal class Program
    {
        private static LtAmpDevice amp = new LtAmpDevice();
        static void Main(string[] args)
        {
            //amp = new Lib.LtAmpDevice();
            //amp.MessageReceived += Amp_MessageReceived;
            //amp.MessageSent += Amp_MessageSent;
            ////amp.DeviceConnected += Amp_DeviceConnected;
            //amp.Open();
            List<Preset>? presets = JsonConvert.DeserializeObject<List<Preset>>(File.ReadAllText(Path.Join(Environment.CurrentDirectory, "JsonDefinitions", "mustang", "default_presets.json")));
            foreach (var preset in presets!)
            {
                Console.WriteLine(preset);
            }
            //var input = args[0];
            //var output = args[1];
            //DecodeTestStrings(input, output);
            //Console.Read();
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
    }
}
using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    class Program
    {
        //Add to github
        static void Main(string[] args)
        {
            Option1_execProcess();
            //Option2_ironPthon();

            var synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.Speak("All we need to do is to make sure we keep talking");
            Console.ReadKey();
        }
        // >>> import sys
        //>>> sys.executable

        static void Option1_execProcess()
        {
            //create process info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\kuvedi\AppData\Local\Programs\Python\Python38\python.exe";

            //provide script and arguments
            //var script = @"D:\Projects\Python\sound.py";
            var script = @"D:\Projects\Python\get_quote_scrip.py";
            psi.Arguments = $"\"{script}\"";
            //Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //Excute process and get output
            var errors = "";
            var result = "";

            using(var process=Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                result = process.StandardOutput.ReadToEnd();

            }

            //Display output
            //List<string> ll = (List<string>)result;
            Console.WriteLine(errors);
            List<string> result1 = result.Split(',').ToList();
            foreach (var i in result1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine(result);
            Console.WriteLine("Done");

        }

        static  void Option2_ironPthon()
        {
            //create Engine
            var engine = Python.CreateEngine();
            //Provide script and arguments
            var script= @"D:\Projects\Python\get_quote_scrip.py";
            var source = engine.CreateScriptSourceFromFile(script);

            //Output redirct
            var eIO = engine.Runtime.IO;
            var errors = new MemoryStream();
            eIO.SetErrorOutput(errors, Encoding.Default);

            var result = new MemoryStream();
            eIO.SetOutput(result, Encoding.Default);


            //Execute script
            var scope = engine.CreateScope();
            source.Execute(scope);

            //Display output
            string str(byte[] x) => Encoding.Default.GetString(x);
            Console.WriteLine(str(result.ToArray()));


        }
    }
}

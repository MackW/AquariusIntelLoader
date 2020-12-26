using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AquarisBasicMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Contains("/c"))  {
                string sSrc = "";
                string sOut = "";
                string sApp = "";
                bool bText = false;
                foreach (string arg in arguments)
                {
                    if (arg.Length>4 && arg.Substring(0, 4).ToLower() == "/src")
                    {
                        sSrc = getValue(arg);
                    }
                    if (arg.Length > 4 && arg.Substring(0, 4).ToLower() == "/app")
                    {
                        sApp = getValue(arg);
                    }
                    if (arg.Length > 5 && arg.Substring(0, 5).ToLower() == "/dest")
                    {
                        sOut = getValue(arg);
                    }
                    if (arg.Length == 5 && arg.Substring(0, 5).ToLower() == "/text")
                    {
                        bText = true;
                    }
                }
                if (bText)
                {
                    if (sSrc.Length == 0 || sOut.Length == 0)
                    {
                        Console.WriteLine("Error in inputs");
                        return;
                    }
                    if (validateFile(sSrc) == false || validatePath(Path.GetDirectoryName(sOut)) == false)
                    {
                        Console.WriteLine("Path or Source not valid");
                        Console.WriteLine("Path :"+ sSrc);
                        Console.WriteLine("Source :"+ sOut);
                        return;
                    }
                    TextFile.CreateFile(sSrc, sOut);
                    Console.WriteLine("Text File Created");
                }
                else
                {
                    if (sSrc.Length == 0 || sOut.Length == 0 || sApp.Length == 0)
                    {
                        Console.WriteLine("Error in inputs");
                        return;
                    }
                    if (validateFile(sSrc) == false || validatePath(sOut) == false)
                    {
                        Console.WriteLine("Path or Source not valid");
                        return;
                    }
                    CAQ.CreateCAQ(sSrc, sOut, sApp);
                    Console.WriteLine("CAQ File Created");
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

        }
        private static string getValue(string sIn)
        {
            if (sIn.IndexOf("=")>0) {
                return (sIn.Substring(sIn.IndexOf("=")+1).Trim());
            }
            if (sIn.IndexOf(" ") > 0)
            {
                return (sIn.Substring(sIn.IndexOf(" ")+1).Trim());
            }
            return "";
        }
        private static bool validatePath(string sIn)
        {
            return System.IO.Directory.Exists(sIn);
        }
        private static bool validateFile(string sIn)
        {
            return System.IO.File.Exists(sIn);
        }
    }
}

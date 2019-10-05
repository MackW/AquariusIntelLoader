using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                foreach(string arg in arguments)
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
                }
                if (sSrc.Length==0 || sOut.Length==0 || sApp.Length == 0)
                {
                    Console.WriteLine("Error in inputs");
                    return;
                }
                CAQ.CreateCAQ(sSrc, sOut, sApp);
                Console.WriteLine("CAQ File Created");
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
    }
}

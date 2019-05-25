using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace AquarisBasicMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fileIn = new StreamReader(textBox1.Text))
            {
                // read from file
                using (var fileOut = new StreamWriter(textBox2.Text+"\\App.txt",false))
                {
                    // write to just created file
                    fileOut.WriteLine("1 clear 100");
                    fileOut.WriteLine("5 St=32000");
                    fileOut.WriteLine("6 poke 14340,0:poke 14341,125");
                    fileOut.WriteLine("10 READ A$: if A$=\":00000001FF\" then stop");
                    fileOut.WriteLine("20 A$=mid$(a$,2)");
                    fileOut.WriteLine("30 b$=left$(A$,2)");
                    fileOut.WriteLine("35 a$=mid$(A$,9)");
                    fileOut.WriteLine("40 Gosub 200");
                    fileOut.WriteLine("50 CNT=Res");
                    fileOut.WriteLine("60 for I =1 to CNT");
                    fileOut.WriteLine("70 b$=left$(A$,2)");
                    fileOut.WriteLine("75 A$=MID$(A$,3)");
                    fileOut.WriteLine("80 gosub 200");
                    fileOut.WriteLine("90 poke ST,Res");
                    fileOut.WriteLine("100 ST=ST+1");
                    fileOut.WriteLine("110 Next");
                    fileOut.WriteLine("120 goto 10");
                    fileOut.WriteLine("200 A=asc(left$(B$,1))-48");
                    fileOut.WriteLine("205 if A>9 then A=A-7");
                    fileOut.WriteLine("210 B=ASC(Right$(B$,1))-48");
                    fileOut.WriteLine("215 if B>9 then B=B-7");
                    fileOut.WriteLine("220 Res=A*16+B");
                    fileOut.WriteLine("230 return");
                    string sIn = fileIn.ReadToEnd();
                    sIn = sIn.Replace("\r", "");
                    string[] aIn = sIn.Split( '\n');
                    int iLineNum = 500;
                    foreach(string sLine in aIn)
                    {
                        if (sLine != "") { 
                            fileOut.WriteLine(iLineNum.ToString()+" Data \"" +sLine +"\"");
                            iLineNum = iLineNum + 10;
                        }
                    }
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string sCode = "ffffffffffffffffffffffff0023232323232300000000002ad83823234e234611430019e5c5e1b7ed52e5c1e1237eb728fb11b039edb00000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            byte[] bCode = StringToByteArray(sCode);
            
            int iLength = 0;
            string sIn = "";
            using (var fileIn = new StreamReader(textBox1.Text))
            {
                sIn = fileIn.ReadToEnd();
            }
            sIn = sIn.Replace("\r", "");
            string[] aIn = sIn.Split('\n');
            int bDest = 0;
            int.TryParse(aIn[0].Substring(3,4),out bDest);
            using (var fileIn = new StreamReader(textBox1.Text))
            {
                // read from file
                using (var fileOut = new FileStream(textBox2.Text + "\\" + txtAppName.Text + ".CAQ", FileMode.Create, FileAccess.Write))
                {
                    // write to just created file
                    fileOut.Write(bCode, 0, bCode.Length);
                    foreach (string sLine in aIn)
                    {

                    }
                }


            }
            string sLoader = "ffffffffffffffffffffffff00202020202020ffffffffffffffffffffffff000939050055b0300011390a0058b030001e391400854128290026391e009aaa41003c3928009431343334302cc128313435353229a83700503932009431343334312cc128313435353329005b393c0058b0b528302900000000000000000000000000000000000000000000000000";
            byte[] bLoader = StringToByteArray(sLoader);
            char[] cAppName = txtAppName.Text.ToCharArray();
            for(int i=0; i<cAppName.Length;i++)
            {
                bLoader[i+13] = (byte)cAppName[i];
            }
            char[] cLength = iLength.ToString().ToCharArray();
            var mahByteArray = new List<byte>();
            mahByteArray.AddRange(bLoader);
            
            for (int i = cAppName.Length-1; i >-1; i--)
            {
                mahByteArray.Insert(55, (byte)cLength[i]);
            }
            bLoader = mahByteArray.ToArray();
            // read from file
            using (var fileOut = new FileStream(textBox2.Text + "\\"+txtAppName.Text+"_B.CAQ", FileMode.Create, FileAccess.Write))
            {
                fileOut.Write(bLoader, 0, bLoader.Length);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox2.Text;
            folderBrowserDialog1.ShowDialog();
            textBox2.Text = folderBrowserDialog1.SelectedPath;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox1.Text;
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }
        public byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquarisBasicMaker
{
    internal static class TextFile
    {

        internal static void CreateFile(string sInFile,string sOutFile)
        {
            using (var fileIn = new StreamReader(sInFile))
            {
                // read from file
                using (var fileOut = new StreamWriter(sOutFile, false))
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
                    string[] aIn = sIn.Split('\n');
                    int iLineNum = 500;
                    foreach (string sLine in aIn)
                    {
                        if (sLine != "")
                        {
                            fileOut.WriteLine(iLineNum.ToString() + " Data \"" + sLine + "\"");
                            iLineNum = iLineNum + 10;
                        }
                    }
                }


            }
        }
    }

}

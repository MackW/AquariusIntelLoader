using System;
using System.IO;
using System.Collections.Generic;
internal static class CAQ {
    internal static void CreateCAQ(string SourceFile, string OutputPath, string AppName)
    {

        string sCode = "ffffffffffffffffffffffff0023232323232300000000002ad83823234e234611430019e5c5e1b7ed52e5c1e1237eb728fb11b03909eb09eb03edb8c30000000000000000000000000000000000000000000000000000000000000000000000";
        byte[] bCode = StringToByteArray(sCode);

        int iLength = bCode.Length;
        string sIn = "";
        using (var fileIn = new StreamReader(SourceFile))
        {
            sIn = fileIn.ReadToEnd();
        }
        sIn = sIn.Replace("\r", "");
        string[] aIn = sIn.Split('\n');
        int bDest = int.Parse(aIn[0].Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
        if (bDest == 0)
        {
            bDest = 16384;
        }
        bCode[52] = (byte)(int)(bDest / 256);
        bCode[51] = (byte)(bDest - ((int)(bDest / 256)) * 256);
        bCode[62] = (byte)(int)(bDest / 256);
        bCode[61] = (byte)(bDest - ((int)(bDest / 256)) * 256);
        byte[] bPadding = new byte[15];
        int iActualCodeLen = 0;
        int iExtraPadding = 0;
        // read from file
        using (var fileOut = new FileStream(OutputPath + "\\" + AppName + ".CAQ", FileMode.Create, FileAccess.Write))
        {
            // write to just created file
            fileOut.Write(bCode, 0, bCode.Length);
            foreach (string sLine in aIn)
            {
                if (sLine != "")
                {
                    int iLineLength = Convert.ToByte(sLine.Substring(1, 2), 16);
                    iActualCodeLen += iLineLength;
                    byte[] bLine = StringToByteArray(sLine.Substring(9, iLineLength * 2));
                    fileOut.Write(bLine, 0, bLine.Length);
                }
            }
            iExtraPadding = 4 - (iActualCodeLen + 73) % 4;
            if (iExtraPadding == 4)
            {
                iExtraPadding = 0;
            }
            else
            {
                fileOut.Write(bPadding, 0, iExtraPadding);
            }
            fileOut.Write(bPadding, 0, bPadding.Length);
        }

        string sLoader = "ffffffffffffffffffffffff00202020202020ffffffffffffffffffffffff000939050055b0300011390a0058b030001e391400854128290026391e009aaa41003c3928009431343334302cc128313435353229a83700503932009431343334312cc128313435353329005b393c0058b0b528302900000000000000000000000000000000000000000000000000";
        byte[] bLoader = StringToByteArray(sLoader);
        char[] cAppName = AppName.ToCharArray();
        for (int i = 0; i < cAppName.Length; i++)
        {
            bLoader[i + 13] = (byte)cAppName[i];
        }

        iLength = iLength + iExtraPadding + iActualCodeLen - 23;
        iLength = (int)(iLength) / 4;
        char[] cLength = iLength.ToString().ToCharArray();
        var mahByteArray = new List<byte>();
        mahByteArray.AddRange(bLoader);

        for (int i = cLength.Length - 1; i > -1; i--)
        {
            mahByteArray.Insert(55, (byte)cLength[i]);
        }
        bLoader = mahByteArray.ToArray();
        // read from file
        using (var fileOut = new FileStream(OutputPath + "\\" + AppName + "_B.CAQ", FileMode.Create, FileAccess.Write))
        {
            fileOut.Write(bLoader, 0, bLoader.Length);
        }
}
public static byte[] StringToByteArray(string hex)
{
    int NumberChars = hex.Length;
    byte[] bytes = new byte[NumberChars / 2];
    for (int i = 0; i < NumberChars; i += 2)
        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
    return bytes;
}
}

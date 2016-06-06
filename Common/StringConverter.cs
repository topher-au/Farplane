using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.Common
{
    public static class StringConverter
    {
        public static byte[] ToFFXBytes(string inputString)
        {
            var stringBytes = Encoding.UTF8.GetBytes(inputString);
            var outBytes = new byte[stringBytes.Length];

            for (int i = 0; i < stringBytes.Length; i++)
                
                outBytes[i] = ffxToASCII.First(v => v.Value == (char)stringBytes[i]).Key;

            return outBytes;
        }

        public static string ToString(byte[] inputBytes, bool printUnknown = false)
        {
            var outBytes = new StringBuilder();

            for (int i = 0; i < inputBytes.Length; i++)
            {
                if (inputBytes[i] == 0x00) return outBytes.ToString();
                try
                {
                    var addChar = (char) ffxToASCII[inputBytes[i]];
                    outBytes.Append(addChar);
                }
                catch
                {
                    if(printUnknown)
                        outBytes.Append ($"[{inputBytes[i].ToString("X2")}]");
                }

            }
                

            return outBytes.ToString();
        }

        public static Dictionary<byte, char> ffxToASCII = new Dictionary<byte, char>
        {
            {0x03, '\n' },
            {0x30, '0' },
            {0x31, '1' },
            {0x32, '2' },
            {0x33, '3' },
            {0x34, '4' },
            {0x35, '5' },
            {0x36, '6' },
            {0x37, '7' },
            {0x38, '8' },
            {0x39, '9' },
            {0x3a, ' ' },
            {0x3b, '!' },
            {0x3c, '\"' },
            {0x3d, '~' },
            {0x3e, '$' },
            {0x3f, '%' },
            {0x40, '&' },
            {0x41, '\'' },
            {0x42, '(' },
            {0x43, ')' },
            {0x44, '*' },
            {0x45, '+' },
            {0x46, ',' },
            {0x47, '-' },
            {0x48, '.' },
            {0x49, '/' },
            {0x4a, ':' },
            {0x4b, ';' },
            {0x4c, '[' },
            {0x4d, '=' },
            {0x4e, ']' },
            {0x4f, '?' },
            {0x50, 'A' },
            {0x51, 'B' },
            {0x52, 'C' },
            {0x53, 'D' },
            {0x54, 'E' },
            {0x55, 'F' },
            {0x56, 'G' },
            {0x57, 'H' },
            {0x58, 'I' },
            {0x59, 'J' },
            {0x5a, 'K' },
            {0x5b, 'L' },
            {0x5c, 'M' },
            {0x5d, 'N' },
            {0x5e, 'O' },
            {0x5f, 'P' },
            {0x60, 'Q' },
            {0x61, 'R' },
            {0x62, 'S' },
            {0x63, 'T' },
            {0x64, 'U' },
            {0x65, 'V' },
            {0x66, 'W' },
            {0x67, 'X' },
            {0x68, 'Y' },
            {0x69, 'Z' },
            {0x70, 'a' },
            {0x71, 'b' },
            {0x72, 'c' },
            {0x73, 'd' },
            {0x74, 'e' },
            {0x75, 'f' },
            {0x76, 'g' },
            {0x77, 'h' },
            {0x78, 'i' },
            {0x79, 'j' },
            {0x7a, 'k' },
            {0x7b, 'l' },
            {0x7c, 'm' },
            {0x7d, 'n' },
            {0x7e, 'o' },
            {0x7f, 'p' },
            {0x80, 'q' },
            {0x81, 'r' },
            {0x82, 's' },
            {0x83, 't' },
            {0x84, 'u' },
            {0x85, 'v' },
            {0x86, 'w' },
            {0x87, 'x' },
            {0x88, 'y' },
            {0x89, 'z' },
        };
    }
}

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
        public static byte ToFFX(byte inputByte)
        {
            try
            {
                return ffxToASCII.First(v => v.Value == (char)inputByte).Key;
            }
            catch
            {
                return inputByte;
            }
            
        }

        public static char ToASCII(byte inputByte)
        {
            try
            {
                return ffxToASCII[inputByte];
            }
            catch
            {
                return (char)inputByte;
            }
            
        }

        public static byte[] ToFFX(string inputString)
        {
            var stringBytes = Encoding.UTF8.GetBytes(inputString);
            var outBytes = new byte[stringBytes.Length];

            for (int i = 0; i < stringBytes.Length; i++)
                
                outBytes[i] = ffxToASCII.First(v => v.Value == (char)stringBytes[i]).Key;

            return outBytes;
        }

        public static string ToASCII(byte[] inputBytes)
        {
            var outBytes = new StringBuilder();

            for (int i = 0; i < inputBytes.Length; i++)
            {
                var b = inputBytes[i];
                if (b == 0x00) // String end
                    return outBytes.ToString();
                if (b == 0x0A) // Color change
                {
                    outBytes.Append($"{{C:{inputBytes[i + 1].ToString("X2")}}}");
                    i++;
                    continue;
                }

                // Attempt to perform conversion
                try
                {
                    outBytes.Append(ffxToASCII[b]);
                }
                catch
                {
                    outBytes.Append($"{{{b.ToString("X2")}}}");
                }

            }
                

            return outBytes.ToString();
        }

        
        public static Dictionary<byte, char> ffxToASCII = new Dictionary<byte, char>
        {
    { 0x00, (char)0 },
    { 0x03, '\n' },
    { 0x30, '0' },
    { 0x31, '1' },
    { 0x32, '2' },
    { 0x33, '3' },
    { 0x34, '4' },
    { 0x35, '5' },
    { 0x36, '6' },
    { 0x37, '7' },
    { 0x38, '8' },
    { 0x39, '9' },
    { 0x3A, ' ' },
    { 0x3B, '!' },
    { 0x3C, '\"' },
    { 0x3D, '~' },
    { 0x3E, '$' },
    { 0x3F, '%' },
    { 0x40, '&' },
    { 0x41, '\'' },
    { 0x42, '(' },
    { 0x43, ')' },
    { 0x44, '*' },
    { 0x45, '+' },
    { 0x46, ',' },
    { 0x47, '-' },
    { 0x48, '.' },
    { 0x49, '/' },
    { 0x4a, ':' },
    { 0x4b, ';' },
    { 0x4c, '[' },
    { 0x4d, '=' },
    { 0x4e, ']' },
    { 0x4f, '?' },
    { 0x50, 'A' },
    { 0x51, 'B' },
    { 0x52, 'C' },
    { 0x53, 'D' },
    { 0x54, 'E' },
    { 0x55, 'F' },
    { 0x56, 'G' },
    { 0x57, 'H' },
    { 0x58, 'I' },
    { 0x59, 'J' },
    { 0x5a, 'K' },
    { 0x5b, 'L' },
    { 0x5c, 'M' },
    { 0x5d, 'N' },
    { 0x5e, 'O' },
    { 0x5f, 'P' },
    { 0x60, 'Q' },
    { 0x61, 'R' },
    { 0x62, 'S' },
    { 0x63, 'T' },
    { 0x64, 'U' },
    { 0x65, 'V' },
    { 0x66, 'W' },
    { 0x67, 'X' },
    { 0x68, 'Y' },
    { 0x69, 'Z' },
    { 0x6A, '[' },
    { 0x6B, '\\' },
    { 0x6C, ']' },
    { 0x6D, '^' },
    { 0x6E, '`' },
    { 0x70, 'a' },
    { 0x71, 'b' },
    { 0x72, 'c' },
    { 0x73, 'd' },
    { 0x74, 'e' },
    { 0x75, 'f' },
    { 0x76, 'g' },
    { 0x77, 'h' },
    { 0x78, 'i' },
    { 0x79, 'j' },
    { 0x7A, 'k' },
    { 0x7B, 'l' },
    { 0x7C, 'm' },
    { 0x7D, 'n' },
    { 0x7E, 'o' },
    { 0x7F, 'p' },
    { 0x80, 'q' },
    { 0x81, 'r' },
    { 0x82, 's' },
    { 0x83, 't' },
    { 0x84, 'u' },
    { 0x85, 'v' },
    { 0x86, 'w' },
    { 0x87, 'x' },
    { 0x88, 'y' },
    { 0x89, 'z' },
    { 0x8A, '{' },
    { 0x8B, '|' },
    { 0x8C, '}' },
    { 0x8D, '～' },
    { 0x8E, '·' },
    { 0x8F, '【' },
    { 0x90, '】' },
    { 0x91, '♪' },
    { 0x92, '♥' },
    { 0x93, 'Œ' },
    { 0x94, '“' },
    { 0x95, '”' },
    { 0x96, '—' },
    { 0x97, 'œ' },
    { 0x98, '¡' },
    { 0x99, '↑' },
    { 0x9A, '↓' },
    { 0x9B, '←' },
    { 0x9C, '→' },
    { 0x9D, '¨' },
    { 0x9E, '«' },
    { 0x9F, '°' },
    { 0xA0, ' ' }, // Some narrower space
    { 0xA1, '»' },
    { 0xA2, '¿' },
    { 0xA3, 'À' },
    { 0xA4, 'Á' },
    { 0xA5, 'Â' },
    { 0xA6, 'Ä' },
    { 0xA7, 'Ç' },
    { 0xA8, 'È' },
    { 0xA9, 'É' },
    { 0xAA, 'Ê' },
    { 0xAB, 'Ë' },
    { 0xAC, 'Ì' },
    { 0xAD, 'Í' },
    { 0xAE, 'Î' },
    { 0xAF, 'Ï' },
    { 0xB0, 'Ñ' },
    { 0xB1, 'Ò' },
    { 0xB2, 'Ó' },
    { 0xB3, 'Ô' },
    { 0xB4, 'Ö' },
    { 0xB5, 'Ù' },
    { 0xB6, 'Ú' },
    { 0xB7, 'Û' },
    { 0xB8, 'Ü' },
    { 0xB9, 'ß' },
    { 0xBA, 'à' },
    { 0xBB, 'á' },
    { 0xBC, 'â' },
    { 0xBD, 'ä' },
    { 0xBE, 'ç' },
    { 0xBF, 'è' },
    { 0xC0, 'é' },
    { 0xC1, 'ê' },
    { 0xC2, 'ë' },
    { 0xC3, 'ì' },
    { 0xC4, 'í' },
    { 0xC5, 'î' },
    { 0xC6, 'ï' },
    { 0xC7, 'ñ' },
    { 0xC8, 'ò' },
    { 0xC9, 'ó' },
    { 0xCA, 'ô' },
    { 0xCB, 'ö' },
    { 0xCC, 'ù' },
    { 0xCD, 'ú' },
    { 0xCE, 'û' },
    { 0xCF, 'ü' },
    { 0xD0, '，' },
    { 0xD1, 'ƒ' },
    { 0xD2, '„' },
    { 0xD3, '…' },
    { 0xD4, '‘' },
    { 0xD5, '’' },
    { 0xD6, '•' },
    { 0xD7, '‐' },
    { 0xD8, '~' },
    { 0xD9, '™' },
    { 0xDA, ' ' }, // Some narrower space
    { 0xDB, '›' },
    { 0xDC, '§' },
    { 0xDD, '©' },
    { 0xDE, 'ª' },
    { 0xDF, '®' },
    { 0xE0, '±' },
    { 0xE1, '²' },
    { 0xE2, '³' },
    { 0xE3, '¼' },
    { 0xE4, '½' },
    { 0xE5, '¾' },
    { 0xE6, '×' },
    { 0xE7, '÷' },
    { 0xE8, '‹' },
    { 0xE9, '⋯' }
        };
    }
}

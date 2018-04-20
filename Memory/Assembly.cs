using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement;

namespace Farplane.Memory
{
    public static partial class GameMemory
    {
        public static class Assembly
        {
            public static void Inject(int offset, string mnemonics, bool isRelative = true)
            {
                var asmBytes = Generate(mnemonics, offset);
                MemorySharp.Write((IntPtr)offset, asmBytes, isRelative);
            }

            public static void Inject(int offset, string[] mnemonics, bool isRelative = true)
            {
                var asmBytes = Generate(mnemonics, offset);
                MemorySharp.Write((IntPtr)offset, asmBytes, isRelative);
            }

            public static byte[] Generate(string mnemonics, int startOffset = 0)
            {
                return MemorySharp.Assembly.Assembler.Assemble(mnemonics, (IntPtr)startOffset);
            }

            public static byte[] Generate(string[] mnemonics, int startOffset = 0)
            {
                var assembledByteList = new List<byte[]>();
                var outArrayLen = 0;
                for (int i = 0; i < mnemonics.Length; i++)
                {
                    var assemblyBytes = MemorySharp.Assembly.Assembler.Assemble(mnemonics[i], (IntPtr)startOffset + outArrayLen);
                    outArrayLen += assemblyBytes.Length;
                    assembledByteList.Add(assemblyBytes);
                }

                var outArrayPos = 0;
                var outArray = new byte[outArrayLen];
                foreach (var assembly in assembledByteList)
                {
                    Array.Copy(assembly, 0, outArray, outArrayPos, assembly.Length);
                    outArrayPos += assembly.Length;
                }

                return outArray;
            }
        }
    }
}

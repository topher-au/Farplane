using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public class General
    {
        private const string DumpFolder = "dump";
        private static int _offsetCurrentGil = OffsetScanner.GetOffset(GameOffset.FFX_CurrentGil);
        private static int _offsetTidusOverdrive = OffsetScanner.GetOffset(GameOffset.FFX_TidusOverdrive);

        public static int CurrentGil
        {
            get
            {
                var currentGil = GameMemory.Read<int>(_offsetCurrentGil, false);
                return currentGil;
            }
            set
            {
                GameMemory.Write(_offsetCurrentGil, value, false);
            }
        }

        public static int TidusOverdrive
        {
            get
            {
                return GameMemory.Read<int>(_offsetTidusOverdrive, false);
            }
            set
            {
                GameMemory.Write(_offsetTidusOverdrive, value);
            }
        }

        public static void DumpStruct<T>(T structData, string fileName)
        {
            var structLength = Marshal.SizeOf<T>();
            var structPtr = Marshal.AllocHGlobal(structLength);
            var structBytes = new byte[structLength];

            try
            {
                Marshal.StructureToPtr(structData, structPtr, false);
                Marshal.Copy(structPtr, structBytes, 0, structLength);
                if (!Directory.Exists(DumpFolder)) Directory.CreateDirectory(DumpFolder);
                File.WriteAllBytes(Path.Combine(DumpFolder, fileName), structBytes);
            }
            finally
            {
                Marshal.FreeHGlobal(structPtr);
            }

            var structFile = Path.Combine(DumpFolder, $"struct_{typeof (T).Name}.txt");
            if(!File.Exists(structFile))
            {
                foreach (var field in typeof(T).GetFields())
                {
                    File.AppendAllText(structFile, $"{field.Name} {Marshal.OffsetOf<T>(field.Name).ToString("X4")}{Environment.NewLine}");
                }
            }
            
        }
    }
}

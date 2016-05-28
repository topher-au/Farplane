using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane
{
    public static class MemoryReader
    {
        private static IntPtr _memoryHandle;
        private static Process _memoryProcess;

        private static bool _isAttached
            => _memoryHandle != IntPtr.Zero && _memoryProcess != null && _memoryProcess.HasExited == false;

        public static void Attach(Process process)
        {
            // Check if valid process
            if (process == null || process.HasExited) return;

            // Open memory for reading and store handle
            var openHandle =
                WinAPI.OpenProcess(56, false, process.Id);

            if (openHandle == IntPtr.Zero) return;

            _memoryProcess = process;
            _memoryHandle = openHandle;
        }

        public static bool CheckProcess()
        {
            if (_memoryProcess == null || _memoryProcess.HasExited) return false;
            return true;
        }

        public static int ReadInt32(int address)
        {
            var bytes = ReadBytes(address, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static Int16 ReadInt16(int address)
        {
            var bytes = ReadBytes(address, 2);
            return BitConverter.ToInt16(bytes, 0);
        }

        public static uint ReadUInt32(int address)
        {
            var bytes = ReadBytes(address, 4);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static UInt16 ReadUInt16(int address)
        {
            var bytes = ReadBytes(address, 2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public static bool ReadByteFlag(int address)
        {
            var bytes = ReadBytes(address, 1);
            return bytes[0] == 1 ? true : false;
        }

        public static byte ReadByte(int address)
        {
            var bytes = ReadBytes(address, 1);
            return (byte) bytes[0];
        }

        public static byte[] ReadBytes(int address, int length)
        {
            if (!_isAttached) return null;

            
                var readBuffer = new byte[length];
            
            var bytesRead = 0;
            WinAPI.ReadProcessMemory(_memoryHandle, _memoryProcess.MainModule.BaseAddress + address, readBuffer, length, bytesRead);
            return readBuffer;
        }

        public static bool WriteBytes(int address, byte[] bytes)
        {
            if (!_isAttached) return false;

            var writeBuffer = new byte[bytes.Length];
            bytes.CopyTo(writeBuffer,0);
            var bytesWritten = 0;

            var success = WinAPI.WriteProcessMemory(_memoryHandle, _memoryProcess.MainModule.BaseAddress + address, writeBuffer,
                writeBuffer.Length, bytesWritten);

            return success;
        }
    }
}

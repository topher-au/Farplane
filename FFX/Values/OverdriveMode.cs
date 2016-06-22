using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Common;
using Farplane.FFX.Data;
using Farplane.Memory;

namespace Farplane.FFX.Values
{
    public class OverdriveMode
    {
        private static readonly int _offsetParty = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);

        public static OverdriveMode[] OverdriveModes =
        {
            new OverdriveMode() {ID = 0, BitIndex = 0, Name = "Warrior"},
            new OverdriveMode() {ID = 1, BitIndex = 1, Name = "Comrade"},
            new OverdriveMode() {ID = 2, BitIndex = 2, Name = "Stoic"},
            new OverdriveMode() {ID = 3, BitIndex = 3, Name = "Healer"},
            new OverdriveMode() {ID = 4, BitIndex = 4, Name = "Tactician"},
            new OverdriveMode() {ID = 5, BitIndex = 5, Name = "Victim"},
            new OverdriveMode() {ID = 6, BitIndex = 6, Name = "Dancer"},
            new OverdriveMode() {ID = 7, BitIndex = 7, Name = "Avenger"},
            new OverdriveMode() {ID = 8, BitIndex = 8, Name = "Slayer"},
            new OverdriveMode() {ID = 9, BitIndex = 9, Name = "Hero"},
            new OverdriveMode() {ID = 10, BitIndex = 10, Name = "Rook"},
            new OverdriveMode() {ID = 11, BitIndex = 11, Name = "Victim"},
            new OverdriveMode() {ID = 12, BitIndex = 12, Name = "Coward"},
            new OverdriveMode() {ID = 13, BitIndex = 13, Name = "Ally"},
            new OverdriveMode() {ID = 14, BitIndex = 14, Name = "Sufferer"},
            new OverdriveMode() {ID = 15, BitIndex = 15, Name = "Daredevil"},
            new OverdriveMode() {ID = 16, BitIndex = 16, Name = "Loner"},
            new OverdriveMode() {ID = 17, BitIndex = 17, Name = "-"},
            new OverdriveMode() {ID = 18, BitIndex = 18, Name = "-"},
            new OverdriveMode() {ID = 19, BitIndex = 19, Name = "Aeons Only"},
        };

        public static void ToggleOverdriveMode(int charIndex, int overdriveId)
        {
            var odOffset = StructHelper.GetFieldOffset<PartyMember>("OverdriveModes",
                _offsetParty + Marshal.SizeOf<PartyMember>()*charIndex);
            var odBytes = GameMemory.Read<byte>(odOffset, 3, false);

            var odMode = OverdriveModes.First(od => od.ID == overdriveId);

            var bitIndex = odMode.BitIndex%8;
            var byteIndex = odMode.BitIndex/8;

            odBytes[byteIndex] = BitHelper.ToggleBit(odBytes[byteIndex], bitIndex);

            GameMemory.Write(odOffset, odBytes, false);
        }

        public static void SetOverdriveCounter(int charIndex, int odIndex, int odCount)
        {
            var odOffset = StructHelper.GetFieldOffset<PartyMember>("OverdriveWarrior",
                _offsetParty + Marshal.SizeOf<PartyMember>()*charIndex + OverdriveModes[odIndex].BitIndex*2);
            GameMemory.Write(odOffset, BitConverter.GetBytes((ushort) odCount), false);
        }

        public int ID { get; set; }
        public int BitIndex { get; set; }
        public string Name { get; set; }
    }
}
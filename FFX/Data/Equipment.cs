using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public static class Equipment
    {
        public static readonly int Offset = OffsetScanner.GetOffset(GameOffset.FFX_EquipmentBase);
        public const int BlockLength = 0x16;
        public const int MaxItems = 0xB2;

        public static EquipmentItem ReadItem(int itemIndex)
        {
            // Read an item from the game's memory into struct
            var offset = Offset + itemIndex*BlockLength;
            var bytes = GameMemory.Read<byte>(offset, BlockLength, false);

            IntPtr ptrEquipmentItem = Marshal.AllocHGlobal(BlockLength);
            try
            {
                // Attempt to copy memory bytes into struct and return
                Marshal.Copy(bytes, 0, ptrEquipmentItem, BlockLength);
                return (EquipmentItem) Marshal.PtrToStructure(ptrEquipmentItem, typeof (EquipmentItem));
            }
            finally
            {
                Marshal.FreeHGlobal(ptrEquipmentItem);
            }
        }

        public static void WriteItem(int itemIndex, EquipmentItem item)
        {
            // Convert data struct to byte array
            var size = Marshal.SizeOf(item);

            var itemBytes = new byte[size];
            IntPtr ptrByteArray = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(item, ptrByteArray, true);
            Marshal.Copy(ptrByteArray, itemBytes, 0, size);
            Marshal.FreeHGlobal(ptrByteArray);

            // Write to game's memory
            var offset = Offset + itemIndex*BlockLength;
            GameMemory.Write(offset, itemBytes, false);
        }

        public static EquipmentItem[] ReadItems()
        {
            var dataLength = MaxItems*BlockLength;
            var dataBytes = GameMemory.Read<byte>(Offset, dataLength, false);

            var readItems = new EquipmentItem[MaxItems];

            for (int i = 0; i < MaxItems; i++)
            {
                IntPtr ptrEquipmentData = Marshal.AllocHGlobal(dataLength);
                Marshal.Copy(dataBytes, i * BlockLength, ptrEquipmentData, BlockLength);
                readItems[i] = (EquipmentItem)Marshal.PtrToStructure(ptrEquipmentData, typeof(EquipmentItem));
                Marshal.FreeHGlobal(ptrEquipmentData);
            }

            return readItems;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct EquipmentItem
    {
        [MarshalAs(UnmanagedType.U1)] // 0x00
        public byte Name;

        [MarshalAs(UnmanagedType.U1)] // 0x01
        public byte Name2;

        [MarshalAs(UnmanagedType.U1)] // 0x02
        public byte SlotOccupied;

        [MarshalAs(UnmanagedType.U1)] // 0x03
        public byte CustomizeMode;

        [MarshalAs(UnmanagedType.U1)] // 0x04
        public byte Character;

        [MarshalAs(UnmanagedType.U1)] // 0x05
        public byte Type;

        [MarshalAs(UnmanagedType.U2)] // 0x06
        public ushort EquippedBy;

        [MarshalAs(UnmanagedType.U1)] // 0x08
        public byte DamageFormula;

        [MarshalAs(UnmanagedType.U1)] // 0x09
        public byte AttackPower;

        [MarshalAs(UnmanagedType.U1)] // 0x0A
        public byte Critical;

        [MarshalAs(UnmanagedType.U1)] // 0x0B
        public byte AbilityCount;

        [MarshalAs(UnmanagedType.U2)] // 0x0C
        public ushort Appearance;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)] // 0x0E - 0x15
        public ushort[] Abilities;
    }
}
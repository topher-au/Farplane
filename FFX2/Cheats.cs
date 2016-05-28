using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX2
{
    public static class Cheats
    {
        public static void GiveAllItems()
        {
            for (int i = 0; i < 68; i++)
            {
                WriteItem(i, i, 99);
            }
        }

        public static void GiveAllAccessories()
        {
            for (int i = 0; i < 128; i++)
            {
                WriteAccessory(i, i, 99);
            }
        }

        public static void WriteItem(int slot, int item, byte count)
        {
            var typeOffset = Offsets.Items.ItemBase + (slot * 2);
            var countOffset = Offsets.Items.QuantityBase + slot;
            if (item == -1) // No item selected
            {
                MemoryReader.WriteBytes(typeOffset, BitConverter.GetBytes((ushort)0xFF));
                MemoryReader.WriteBytes(countOffset, new byte[] { (byte)0 });
            }
            else
            {
                MemoryReader.WriteBytes(typeOffset, BitConverter.GetBytes((ushort)(item + 0x9000)));
                MemoryReader.WriteBytes(countOffset, new byte[] { (byte)count });
            }
        }

        public static void WriteAccessory(int slot, int item, byte count)
        {
            var typeOffset = Offsets.Accessories.AccessoriesBase + (slot * 2);
            var countOffset = Offsets.Accessories.QuantityBase + slot;
            if (item == -1) // No item selected
            {
                MemoryReader.WriteBytes(typeOffset, BitConverter.GetBytes((ushort)0xFF));
                MemoryReader.WriteBytes(countOffset, new byte[] { (byte)0 });
            }
            else
            {
                MemoryReader.WriteBytes(typeOffset, BitConverter.GetBytes((ushort)(item + 0x9000)));
                MemoryReader.WriteBytes(countOffset, new byte[] { (byte)count });
            }
        }

        public static void GiveAllGrids()
        {
            var allGridBytes = new byte[8].Select(gb => gb = (byte) 0xFF).ToArray();
            MemoryReader.WriteBytes(Offsets.General.GarmentGridBase, allGridBytes);
        }
    }
}

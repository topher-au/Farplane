using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public class SphereGrid
    {
        private static int _offsetSphereGrid = OffsetScanner.GetOffset(GameOffset.FFX_SphereGrid);
        private static int _offsetCurrentNode = _offsetSphereGrid + 0x1130C;

        private static int _sizeSphereGridNode = Marshal.SizeOf<SphereGridNode>();

        public static NodeType[] NodeTypes =
        {
            new NodeType {ID = 0x00, Name = "Lv. 3 Lock"},
            new NodeType {ID = 0x01, Name = "Empty"},
            new NodeType {ID = 0x02, Name = "Strength +1"},
            new NodeType {ID = 0x03, Name = "Strength +2"},
            new NodeType {ID = 0x04, Name = "Strength +3"},
            new NodeType {ID = 0x05, Name = "Strength +4"},
            new NodeType {ID = 0x06, Name = "Defense +1"},
            new NodeType {ID = 0x07, Name = "Defense +2"},
            new NodeType {ID = 0x08, Name = "Defense +3"},
            new NodeType {ID = 0x09, Name = "Defense +4"},
            new NodeType {ID = 0x0a, Name = "Magic +1"},
            new NodeType {ID = 0x0b, Name = "Magic +2"},
            new NodeType {ID = 0x0c, Name = "Magic +3"},
            new NodeType {ID = 0x0d, Name = "Magic +4"},
            new NodeType {ID = 0x0e, Name = "Magic Defense +1"},
            new NodeType {ID = 0x0f, Name = "Magic Defense +2"},
            new NodeType {ID = 0x10, Name = "Magic Defense +3"},
            new NodeType {ID = 0x11, Name = "Magic Defense +4"},
            new NodeType {ID = 0x12, Name = "Agility +1"},
            new NodeType {ID = 0x13, Name = "Agility +2"},
            new NodeType {ID = 0x14, Name = "Agility +3"},
            new NodeType {ID = 0x15, Name = "Agility +4"},
            new NodeType {ID = 0x16, Name = "Luck +1"},
            new NodeType {ID = 0x17, Name = "Luck +2"},
            new NodeType {ID = 0x18, Name = "Luck +3"},
            new NodeType {ID = 0x19, Name = "Luck +4"},
            new NodeType {ID = 0x1a, Name = "Evasion +1"},
            new NodeType {ID = 0x1b, Name = "Evasion +2"},
            new NodeType {ID = 0x1c, Name = "Evasion +3"},
            new NodeType {ID = 0x1d, Name = "Evasion +4"},
            new NodeType {ID = 0x1e, Name = "Accuracy +1"},
            new NodeType {ID = 0x1f, Name = "Accuracy +2"},
            new NodeType {ID = 0x20, Name = "Accuracy +3"},
            new NodeType {ID = 0x21, Name = "Accuracy +4"},
            new NodeType {ID = 0x22, Name = "HP +200"},
            new NodeType {ID = 0x23, Name = "HP +300"},
            new NodeType {ID = 0x24, Name = "MP +40"},
            new NodeType {ID = 0x25, Name = "MP +20"},
            new NodeType {ID = 0x26, Name = "MP +10"},
            new NodeType {ID = 0x27, Name = "Lv. 1 Lock"},
            new NodeType {ID = 0x28, Name = "Lv. 2 Lock"},
            new NodeType {ID = 0x29, Name = "Lv. 4 Lock"},
            new NodeType {ID = 0x2a, Name = "Delay Attack"},
            new NodeType {ID = 0x2b, Name = "Delay Buster"},
            new NodeType {ID = 0x2c, Name = "Sleep Attack"},
            new NodeType {ID = 0x2d, Name = "Silence Attack"},
            new NodeType {ID = 0x2e, Name = "Dark Attack"},
            new NodeType {ID = 0x2f, Name = "Zombie Attack"},
            new NodeType {ID = 0x30, Name = "Sleep Buster"},
            new NodeType {ID = 0x31, Name = "Silence Buster"},
            new NodeType {ID = 0x32, Name = "Dark Buster"},
            new NodeType {ID = 0x33, Name = "Triple Foul"},
            new NodeType {ID = 0x34, Name = "Power Break"},
            new NodeType {ID = 0x35, Name = "Magic Break"},
            new NodeType {ID = 0x36, Name = "Armor Break"},
            new NodeType {ID = 0x37, Name = "Mental Break"},
            new NodeType {ID = 0x38, Name = "Mug"},
            new NodeType {ID = 0x39, Name = "Quick Hit"},
            new NodeType {ID = 0x3a, Name = "Steal"},
            new NodeType {ID = 0x3b, Name = "Use"},
            new NodeType {ID = 0x3c, Name = "Flee"},
            new NodeType {ID = 0x3d, Name = "Pray"},
            new NodeType {ID = 0x3e, Name = "Cheer"},
            new NodeType {ID = 0x3f, Name = "Focus"},
            new NodeType {ID = 0x40, Name = "Reflex"},
            new NodeType {ID = 0x41, Name = "Aim"},
            new NodeType {ID = 0x42, Name = "Luck"},
            new NodeType {ID = 0x43, Name = "Jinx"},
            new NodeType {ID = 0x44, Name = "Lancet"},
            new NodeType {ID = 0x45, Name = "Guard"},
            new NodeType {ID = 0x46, Name = "Sentinel"},
            new NodeType {ID = 0x47, Name = "Spare Change"},
            new NodeType {ID = 0x48, Name = "Threaten"},
            new NodeType {ID = 0x49, Name = "Provoke"},
            new NodeType {ID = 0x4a, Name = "Entrust"},
            new NodeType {ID = 0x4b, Name = "Copycat"},
            new NodeType {ID = 0x4c, Name = "Doublecast"},
            new NodeType {ID = 0x4d, Name = "Bribe"},
            new NodeType {ID = 0x4e, Name = "Cure"},
            new NodeType {ID = 0x4f, Name = "Cura"},
            new NodeType {ID = 0x50, Name = "Curaga"},
            new NodeType {ID = 0x51, Name = "NulFrost"},
            new NodeType {ID = 0x52, Name = "NulBlaze"},
            new NodeType {ID = 0x53, Name = "NulShock"},
            new NodeType {ID = 0x54, Name = "NulTide"},
            new NodeType {ID = 0x55, Name = "Scan"},
            new NodeType {ID = 0x56, Name = "Esuna"},
            new NodeType {ID = 0x57, Name = "Life"},
            new NodeType {ID = 0x58, Name = "Full-Life"},
            new NodeType {ID = 0x59, Name = "Haste"},
            new NodeType {ID = 0x5a, Name = "Hastega"},
            new NodeType {ID = 0x5b, Name = "Slow"},
            new NodeType {ID = 0x5c, Name = "Slowga"},
            new NodeType {ID = 0x5d, Name = "Shell"},
            new NodeType {ID = 0x5e, Name = "Protect"},
            new NodeType {ID = 0x5f, Name = "Reflect"},
            new NodeType {ID = 0x60, Name = "Dispel"},
            new NodeType {ID = 0x61, Name = "Regen"},
            new NodeType {ID = 0x62, Name = "Holy"},
            new NodeType {ID = 0x63, Name = "Auto-Life"},
            new NodeType {ID = 0x64, Name = "Blizzard"},
            new NodeType {ID = 0x65, Name = "Fire"},
            new NodeType {ID = 0x66, Name = "Thunder"},
            new NodeType {ID = 0x67, Name = "Water"},
            new NodeType {ID = 0x68, Name = "Fira"},
            new NodeType {ID = 0x69, Name = "Blizzara"},
            new NodeType {ID = 0x6a, Name = "Thundara"},
            new NodeType {ID = 0x6b, Name = "Watera"},
            new NodeType {ID = 0x6c, Name = "Firaga"},
            new NodeType {ID = 0x6d, Name = "Blizzaga"},
            new NodeType {ID = 0x6e, Name = "Thundaga"},
            new NodeType {ID = 0x6f, Name = "Waterga"},
            new NodeType {ID = 0x70, Name = "Bio"},
            new NodeType {ID = 0x71, Name = "Demi"},
            new NodeType {ID = 0x72, Name = "Death"},
            new NodeType {ID = 0x73, Name = "Drain"},
            new NodeType {ID = 0x74, Name = "Osmose"},
            new NodeType {ID = 0x75, Name = "Flare"},
            new NodeType {ID = 0x76, Name = "Ultima"},
            new NodeType {ID = 0x77, Name = "Pilfer Gil"},
            new NodeType {ID = 0x78, Name = "Full Break"},
            new NodeType {ID = 0x79, Name = "Extract Power"},
            new NodeType {ID = 0x7a, Name = "Extract Mana"},
            new NodeType {ID = 0x7b, Name = "Extract Speed"},
            new NodeType {ID = 0x7c, Name = "Extract Ability"},
            new NodeType {ID = 0x7d, Name = "Nab Gil"},
            new NodeType {ID = 0x7e, Name = "Quick Pockets"},
        };

        public int ID { get; set; }
        public NodeType Type { get; set; }
        public byte ActivatedBy { get; set; }

        public static string[] GetNames()
        {
            return NodeTypes.Select(n => n.Name).ToArray();
        }

        public static int GetSelectedNode()
        {
            var selectedNode = GameMemory.Read<int>(_offsetCurrentNode, false);
            return selectedNode;
        }

        

        public static SphereGridNode ReadNode(int nodeIndex)
        {
            var offset = _offsetSphereGrid + 0x818 + nodeIndex*_sizeSphereGridNode;
            var nodeBytes = GameMemory.Read<byte>(offset, _sizeSphereGridNode, false);

            var nodePtr = Marshal.AllocHGlobal(_sizeSphereGridNode);
            try
            {
                Marshal.Copy(nodeBytes, 0, nodePtr, _sizeSphereGridNode);
                var node = Marshal.PtrToStructure<SphereGridNode>(nodePtr);
                return node;
            }
            finally
            {
                Marshal.FreeHGlobal(nodePtr);
            }
        }

        public static void SetNodeType(int nodeIndex, int nodeType)
        {
            var nodeOffset = _offsetSphereGrid + 0x818 + nodeIndex*_sizeSphereGridNode;
            var dataOffset = Marshal.OffsetOf<SphereGridNode>("NodeType");
            GameMemory.Write((int) (dataOffset + nodeOffset), (ushort) nodeType, false);
        }

        public static void SetNodeActivation(int nodeIndex, byte nodeActivation)
        {
            var nodeOffset = _offsetSphereGrid + 0x818 + nodeIndex*_sizeSphereGridNode;
            var dataOffset = Marshal.OffsetOf<SphereGridNode>("ActivatedBy");
            GameMemory.Write((int) (dataOffset + nodeOffset), nodeActivation, false);
        }
    }

    public class NodeType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, Size = 40)]
    public struct SphereGridNode
    {
        [MarshalAs(UnmanagedType.U2)] public ushort unknown_1;
        [MarshalAs(UnmanagedType.U2)] public ushort unknown_2;
        [MarshalAs(UnmanagedType.U2)] public ushort unknown_3;
        [MarshalAs(UnmanagedType.U2)] public ushort NodeType;
        [MarshalAs(UnmanagedType.U8)] public long unknown_4;
        [MarshalAs(UnmanagedType.U8)] public long unknown_5;
        [MarshalAs(UnmanagedType.U8)] public long unknown_6;
        [MarshalAs(UnmanagedType.U1)] public byte unknown_7;
        [MarshalAs(UnmanagedType.U1)] public byte ActivatedBy;
        [MarshalAs(UnmanagedType.U2)] public ushort unknown_8;
        [MarshalAs(UnmanagedType.U4)] public uint unknown_9;
    }
}
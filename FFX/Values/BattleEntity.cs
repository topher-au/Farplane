using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farplane.Common;

namespace Farplane.FFX.Values
{
    public class BattleEntity
    {
        public static readonly int _offsetPtrParty = Offsets.GetOffset(OffsetType.BattlePlayerPointer);
        public static readonly int _offsetPtrEnemy = Offsets.GetOffset(OffsetType.BattleEnemyPointer);
        public static readonly _FieldInfo[] _structInfo = typeof (BattleEntity).GetFields();

        public static bool ReadEntity(EntityType entityType, int entityIndex, out BattleEntityData outputEntity)
        {
            var offsetEntity = GetEntityOffset(entityType, entityIndex);

            var entityData = MemoryReader.ReadBytes(offsetEntity, (int) BlockLength.BattleEntity, true);

            // Copy entity data
            IntPtr ptrEntityData = Marshal.AllocHGlobal(entityData.Length);
            try
            {
                Marshal.Copy(entityData, 0, ptrEntityData, entityData.Length);
                outputEntity = (BattleEntityData) Marshal.PtrToStructure(ptrEntityData, typeof (BattleEntityData));
            }
            finally
            {
                Marshal.FreeHGlobal(ptrEntityData);
            }
            return true;
        }

        public static bool CheckBattleState()
        {
            var ptrEntityList = MemoryReader.ReadInt32(_offsetPtrParty);
            return ptrEntityList != 0;
        }

        public static int GetEntityOffset(EntityType entityType, int entityIndex)
        {
            var pointerOffset = entityType == EntityType.Party ? _offsetPtrParty : _offsetPtrEnemy;
            var ptrEntityList = MemoryReader.ReadInt32(pointerOffset);

            if (ptrEntityList == 0)
                return 0;

            return ptrEntityList + (int)BlockLength.BattleEntity * entityIndex;
        }

        public static void WriteBytes(EntityType entityType, int entityIndex, EntityDataOffset entityDataType, byte[] dataToWrite)
        {
            var entityOffset = GetEntityOffset(entityType, entityIndex);
            var dataOffset = entityOffset + (int) entityDataType;

            MemoryReader.WriteBytes(dataOffset, dataToWrite, true);
        }

        public static void WriteBytes(EntityType entityType, int entityIndex, EntityDataOffset entityDataType, byte dataToWrite)
        {
            var entityOffset = GetEntityOffset(entityType, entityIndex);
            var dataOffset = entityOffset + (int)entityDataType;

            MemoryReader.WriteBytes(dataOffset, new [] { dataToWrite }, true);
        }
    }

    public enum EntityType
    {
        Party,
        Enemy
    }

    public enum EntityDataOffset
    {
        unknown_1 = 0x0000,
        text_name = 0x0050,
        text_help = 0x0090,
        text_scan = 0x0190,
        unknown_2 = 0x0390,
        hp_max = 0x0594,
        mp_max = 0x0598,
        hp_max2 = 0x059c,
        mp_max2 = 0x05a0,
        overkill = 0x05a4,
        strength = 0x05a8,
        defense = 0x05a9,
        magic = 0x05aa,
        magic_defense = 0x05ab,
        agility = 0x05ac,
        luck = 0x05ad,
        evasion = 0x05ae,
        accuracy = 0x05af,
        unknown_3 = 0x05b0,
        overdrive_current = 0x05bc,
        overdrive_max = 0x05bd,
        unknown_4 = 0x05bf,
        hp_current = 0x05D0,
        mp_current = 0x05D4,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct BattleEntityData
    {
        [MarshalAs(UnmanagedType.I4)]
        public int guid;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76)]
        public byte[] unknown_1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] text_name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] text_help;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] text_scan;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 516)]
        public byte[] unknown_2;

        [MarshalAs(UnmanagedType.U4)]
        public int hp_max;

        [MarshalAs(UnmanagedType.U4)]
        public int mp_max;

        [MarshalAs(UnmanagedType.U4)]
        public int hp_max2;

        [MarshalAs(UnmanagedType.U4)]
        public int mp_max2;

        [MarshalAs(UnmanagedType.U4)]
        public int overkill;

        [MarshalAs(UnmanagedType.U1)]
        public byte strength;

        [MarshalAs(UnmanagedType.U1)]
        public byte defense;

        [MarshalAs(UnmanagedType.U1)]
        public byte magic;

        [MarshalAs(UnmanagedType.U1)]
        public byte magic_defense;

        [MarshalAs(UnmanagedType.U1)]
        public byte agility;

        [MarshalAs(UnmanagedType.U1)]
        public byte luck;
        
        [MarshalAs(UnmanagedType.U1)]
        public byte evasion;

        [MarshalAs(UnmanagedType.U1)]
        public byte accuracy;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] unknown_3;

        [MarshalAs(UnmanagedType.U1)]
        public byte overdrive_current;

        [MarshalAs(UnmanagedType.U1)]
        public byte overdrive_max;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] unknown_4;

        [MarshalAs(UnmanagedType.U4)]
        public int hp_current;

        [MarshalAs(UnmanagedType.U4)]
        public int mp_current;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] unknown_5;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] unknown_6;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] status_flags_negative;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_turns_sleep;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_turns_silence;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_turns_darkness;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_shell;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_protect;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_reflect;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_nultide;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_nulblaze;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_nulshock;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_nulfrost;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_regen;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_haste;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_slow;

        [MarshalAs(UnmanagedType.U1)]
        public byte status_unknown;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] positive_status_flags;
    }


}
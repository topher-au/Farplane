using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farplane.Common;
using Farplane.FFX.Data;
using Farplane.Memory;

namespace Farplane.FFX.Values
{
    public class BattleEntity
    {
        public static readonly int OffsetPtrParty = OffsetScanner.GetOffset(GameOffset.FFX_BattlePointerParty);
        public static readonly int OffsetPtrEnemy = OffsetScanner.GetOffset(GameOffset.FFX_BattlePointerEnemy);
        public static readonly FieldInfo[] StructInfo = typeof (BattleEntity).GetFields();

        public static bool ReadEntity(EntityType entityType, int entityIndex, out BattleEntityData outputEntity)
        {
            var offsetEntity = GetEntityOffset(entityType, entityIndex);

            var entityData = LegacyMemoryReader.ReadBytes(offsetEntity, (int)Battle.BlockLengthEntity, true);

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
            var ptrParty = GameMemory.Read<IntPtr>(OffsetPtrParty, false);
            return ptrParty != IntPtr.Zero;
        }

        public static int GetEntityOffset(EntityType entityType, int entityIndex)
        {
            var pointerOffset = entityType == EntityType.Party ? OffsetPtrParty : OffsetPtrEnemy;
            var ptrEntityList = GameMemory.Read<IntPtr>(pointerOffset, false);

            if (ptrEntityList == IntPtr.Zero)
                return 0;

            return (int)ptrEntityList + (int)Battle.BlockLengthEntity * entityIndex;
        }

        public static void WriteBytes(EntityType entityType, int entityIndex, string entityDataType, byte[] dataToWrite)
        {
            var entityOffset = GetEntityOffset(entityType, entityIndex);
            var dataOffset = entityOffset + (int)Marshal.OffsetOf<BattleEntityData>(entityDataType);

            GameMemory.Write(dataOffset, dataToWrite, false);
        }

        public static void WriteBytes(EntityType entityType, int entityIndex, string entityDataType, byte dataToWrite)
        {
            var entityOffset = GetEntityOffset(entityType, entityIndex);
            var dataOffset = entityOffset + (int)Marshal.OffsetOf<BattleEntityData>(entityDataType);
            GameMemory.Write(dataOffset, dataToWrite, false);
        }
    }

    public enum EntityType
    {
        Party,
        Enemy
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi, Size = 0xF90)]
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

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 88)]
        public byte[] unknown_2;

        [MarshalAs(UnmanagedType.U1)]
        public byte unknown_9;

        [MarshalAs(UnmanagedType.U1)]
        public byte unknown_10;

        [MarshalAs(UnmanagedType.U1)]
        public byte animation_start;

        [MarshalAs(UnmanagedType.U1)]
        public byte unknown_11; // freezes animation when != 0

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public byte[] unknown_12; // 9

        [MarshalAs(UnmanagedType.U1)]
        public byte animation_speed;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 414)]
        public byte[] unknown_8;

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

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] unknown_4;

        [MarshalAs(UnmanagedType.U1)]
        public byte timer_doom;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public byte[]  unknown_14;

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
        public byte[] status_flags_positive;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 176)]
        public byte[] unknown_7;

        [MarshalAs(UnmanagedType.U2)]
        public ushort action_defend;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 110)]
        public byte[] unknown_13;

        [MarshalAs(UnmanagedType.U1)]
        public byte battle_row;
    }


}
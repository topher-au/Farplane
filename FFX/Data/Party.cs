using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using Farplane.Common;
using Farplane.FFX.Values;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public static class Party
    {
        private static readonly int _offsetParty = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);
        private static readonly int _offsetPartyList = OffsetScanner.GetOffset(GameOffset.FFX_PartyList);

        private static readonly int _blockLength = Marshal.SizeOf<PartyMember>();

        public static Character[] GetActiveParty()
        {
            var party = GameMemory.Read<byte>(_offsetPartyList, 8, false);
            if (party == null) return new Character[8];
            var outArray = new Character[8];

            for (int i = 0; i < 8; i++)
                outArray[i] = (Character) party[i];

            return outArray;
        }

        internal static void SetActiveParty(Character[] characters)
        {
            var writeArray = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                if (i > characters.Length)
                    writeArray[i] = 0xFF;
                else
                    writeArray[i] = (byte) characters[i];
            }
            GameMemory.Write(_offsetPartyList, writeArray, false);
        }

        public static void AddCharacter(Character character)
        {
            // Read current list of party members
            var partyList = Party.GetActiveParty();
            int partySize = partyList.Length;
            int charaPos = Array.IndexOf(partyList, character);
            var writePos = Array.IndexOf(partyList, Character.None);

            // If character is not in the party
            if (charaPos == -1 && writePos != -1)
            {
                // Write character into the last party slot
                GameMemory.Write(_offsetPartyList + writePos, (byte) character, false);
            }
        }

        public static void RemoveCharacter(Character character)
        {
            // Check if character is in the party
            var partyList = Party.GetActiveParty();
            int partySize = partyList.Length;
            int charaPos = Array.IndexOf(partyList, character);

            // If in party
            if (charaPos != -1)
            {
                // Remove and move all party members up
                for (int i = charaPos; i < partySize - 1; i++)
                {
                    partyList[i] = partyList[i + 1];
                }
                // Empty last slot
                partyList[partySize - 1] = Character.None;
            }
            else return;

            // Convert party list to array of byte
            var partyBytes = new byte[partySize];
            for (int i = 0; i < partySize; i++)
                partyBytes[i] = (byte) partyList[i];

            // Write changes to memory
            GameMemory.Write(_offsetPartyList, partyBytes, false);
        }

        public static PartyMember ReadPartyMember(int partyIndex)
        {
            // Read an item from the game's memory into struct
            var offset = _offsetParty + partyIndex*_blockLength;
            var bytes = GameMemory.Read<byte>(offset, _blockLength, false);

            IntPtr ptrEquipmentItem = Marshal.AllocHGlobal(_blockLength);
            try
            {
                // Attempt to copy memory bytes into struct and return
                Marshal.Copy(bytes, 0, ptrEquipmentItem, _blockLength);
                return (PartyMember) Marshal.PtrToStructure(ptrEquipmentItem, typeof (PartyMember));
            }
            finally
            {
                Marshal.FreeHGlobal(ptrEquipmentItem);
            }
        }

        public static void SetPartyMemberAttribute<T>(int partyIndex, string attributeName, T value)
        {
            var offset = _offsetParty + partyIndex * _blockLength;
            var attributeOffset = Marshal.OffsetOf<PartyMember>(attributeName);
            GameMemory.Write((int)attributeOffset + offset, value, false);
        }

        public static void ToggleSkillFlag(int partyIndex, int skillFlag)
        {
            var offset = _offsetParty + partyIndex * _blockLength + (int)Marshal.OffsetOf<PartyMember>("SkillFlags");
            var byteIndex = skillFlag / 8;
            var bitIndex = skillFlag % 8;

            var partyMember = Party.ReadPartyMember(partyIndex);
            var skillBytes = partyMember.SkillFlags;

            var newByte = BitHelper.ToggleBit(skillBytes[byteIndex], bitIndex);
            skillBytes[byteIndex] = newByte;

            GameMemory.Write(offset, skillBytes, false);
        }

        public static int GetMemoryOffset(int partyIndex)
        {
            return _offsetParty + partyIndex*_blockLength;
        }

        public static void WritePartyMember(int partyIndex, PartyMember item)
        {
            // Convert data struct to byte array
            var size = Marshal.SizeOf(item);

            var itemBytes = new byte[size];
            IntPtr ptrByteArray = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(item, ptrByteArray, true);
            Marshal.Copy(ptrByteArray, itemBytes, 0, size);
            Marshal.FreeHGlobal(ptrByteArray);

            // Write to game's memory
            var offset = _offsetParty + partyIndex*_blockLength;
            GameMemory.Write(offset, itemBytes, false);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi, Size = 0x94)]
    public struct PartyMember
    {
        [MarshalAs(UnmanagedType.U2)] public ushort unknown_ushort;

        [MarshalAs(UnmanagedType.U4)] public int BaseHp;

        [MarshalAs(UnmanagedType.U4)] public int BaseMp;

        [MarshalAs(UnmanagedType.U1)] public byte BaseStrength;

        [MarshalAs(UnmanagedType.U1)] public byte BaseDefense;

        [MarshalAs(UnmanagedType.U1)] public byte BaseMagic;

        [MarshalAs(UnmanagedType.U1)] public byte BaseMagicDefense;

        [MarshalAs(UnmanagedType.U1)] public byte BaseAgility;

        [MarshalAs(UnmanagedType.U1)] public byte BaseLuck;

        [MarshalAs(UnmanagedType.U1)] public byte BaseEvasion;

        [MarshalAs(UnmanagedType.U1)] public byte BaseAccuracy;

        [MarshalAs(UnmanagedType.U4)] public int ApTotal;

        [MarshalAs(UnmanagedType.U4)] public int ApCurrent;

        [MarshalAs(UnmanagedType.U4)] public int CurrentHp;

        [MarshalAs(UnmanagedType.U4)] public int CurrentMp;

        [MarshalAs(UnmanagedType.U4)] public int CurrentHpMax;

        [MarshalAs(UnmanagedType.U4)] public int CurrentMpMax;

        [MarshalAs(UnmanagedType.U1)] public byte InParty;

        [MarshalAs(UnmanagedType.U1)] public byte EquippedWeapon;

        [MarshalAs(UnmanagedType.U1)] public byte EquippedArmor;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentStrength;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentDefense;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentMagic;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentMagicDefense;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentAgility;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentLuck;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentEvasion;

        [MarshalAs(UnmanagedType.U1)] public byte CurrentAccuracy;

        [MarshalAs(UnmanagedType.U1)] public byte PoisonDamage;

        [MarshalAs(UnmanagedType.U1)] public byte OverdriveMode;

        [MarshalAs(UnmanagedType.U1)] public byte OverdriveLevel;

        [MarshalAs(UnmanagedType.U1)] public byte OverdriveMax;

        [MarshalAs(UnmanagedType.U1)] public byte SphereLevelCurrent;

        [MarshalAs(UnmanagedType.U1)] public byte SphereLevelTotal;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)] public byte[] SkillFlags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public byte[] unknown_1;

        [MarshalAs(UnmanagedType.U4)] public int TotalBattles;

        [MarshalAs(UnmanagedType.U4)] public int TotalKills;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public byte[] unknown_2;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveWarrior;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveComrade;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveStoic;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveHealer;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveTactician;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveVictim;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveDancer;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveAvenger;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveSlayer;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveHero;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveRook;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveVictor;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveCoward;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveAlly;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveSufferer;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveDaredevil;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveLoner;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveBlank1;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveBlank2;

        [MarshalAs(UnmanagedType.U2)] public ushort OverdriveAeonsOnly;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public byte[] OverdriveModes;
    }
}
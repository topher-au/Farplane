using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Common;
using Farplane.FFX.Data;
using Farplane.FFX.Values;

namespace Farplane.FFX
{
    internal static class Cheats
    {
        public static void GiveAllItems()
        {
            for (int i = 1; i < Item.Items.Length - 1; i++)
            {
                Item.WriteItem(i - 1, Item.Items[i].ID, 99);
            }
        }

        public static void MaxAllStats()
        {
            var partyOffset = Offsets.GetOffset(OffsetType.PartyStatsBase);

            for (int i = 0; i < 18; i++)
            {
                int characterOffset = partyOffset + 0x94*i;
                var overdriveLevel = i > 7 ? 20 : 100;
                Memory.WriteBytes(StructHelper.GetFieldOffset<PartyMember>("CurrentHp", characterOffset), BitConverter.GetBytes((uint) 99999));
                Memory.WriteBytes(StructHelper.GetFieldOffset<PartyMember>("MaxHp", characterOffset), BitConverter.GetBytes((uint)99999));
                Memory.WriteBytes(StructHelper.GetFieldOffset<PartyMember>("BaseHp", characterOffset), BitConverter.GetBytes((uint)99999));
                Memory.WriteBytes(StructHelper.GetFieldOffset<PartyMember>("CurrentMp", characterOffset), BitConverter.GetBytes((uint)9999));
                Memory.WriteBytes(StructHelper.GetFieldOffset<PartyMember>("MaxMp", characterOffset), BitConverter.GetBytes((uint)9999));
                Memory.WriteBytes(StructHelper.GetFieldOffset<PartyMember>("BaseMp", characterOffset), BitConverter.GetBytes((uint)9999));
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseStrength", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseDefense", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseMagic", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseMagicDefense", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseAgility", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseLuck", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseEvasion", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("BaseAccuracy", characterOffset), (byte)255);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("OverdriveLevel", characterOffset), (byte)overdriveLevel);
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("OverdriveMax", characterOffset), (byte)overdriveLevel);
            }
        }

        public static void MaxSphereLevels()
        {
            var partyOffset = Offsets.GetOffset(OffsetType.PartyStatsBase);
            for (var i = 0; i < 8; i++)
            {
                int characterOffset = partyOffset + 0x94 * i;
                Memory.WriteByte(StructHelper.GetFieldOffset<PartyMember>("SphereLevelCurrent", characterOffset), 255);
            }
        }

        public static void LearnAllAbilities()
        {
            var partyOffset = Offsets.GetOffset(OffsetType.PartyStatsBase);
            for (var i = 0; i < 18; i++)
            {
                
                int characterAbilityOffset = partyOffset + Marshal.SizeOf<PartyMember>() * i + StructHelper.GetFieldOffset<PartyMember>("SkillFlags"); ;
                var currentAbilities = Memory.ReadBytes(characterAbilityOffset, 13);

                // Flip all normal ability bits
                currentAbilities[1] |= 0xF0;
                for (int b = 2; b < 11; b++)
                    currentAbilities[b] |= 0xFF;
                currentAbilities[11] |= 0x0F;
                currentAbilities[12] |= 0xFF;

                Memory.WriteBytes(characterAbilityOffset, currentAbilities);
            }
        }
    }
}

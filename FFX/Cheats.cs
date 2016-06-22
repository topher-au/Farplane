using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Common;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using Farplane.Memory;

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
            var partyOffset = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);

            for (int i = 0; i < 18; i++)
            {
                int characterOffset = partyOffset + 0x94*i;
                var overdriveLevel = i > 7 ? 20 : 100;

                Party.SetPartyMemberAttribute<uint>(i, "CurrentHp",  99999);
                Party.SetPartyMemberAttribute<uint>(i, "MaxHp", 99999);
                Party.SetPartyMemberAttribute<uint>(i, "BaseHp", 99999);
                Party.SetPartyMemberAttribute<uint>(i, "CurrentMp", 9999);
                Party.SetPartyMemberAttribute<uint>(i, "MaxMp", 9999);
                Party.SetPartyMemberAttribute<uint>(i, "BaseMp", 9999);

                Party.SetPartyMemberAttribute<byte>(i, "BaseStrength", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseDefense", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseMagic", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseMagicDefense", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseAgility", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseLuck", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseEvasion", 255);
                Party.SetPartyMemberAttribute<byte>(i, "BaseAccuracy", 255);
                Party.SetPartyMemberAttribute<byte>(i, "OverdriveLevel", (byte)overdriveLevel);
                Party.SetPartyMemberAttribute<byte>(i, "OverdriveMax", (byte)overdriveLevel);
            }
        }

        public static void MaxSphereLevels()
        {
            var partyOffset = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);
            for (var i = 0; i < 8; i++)
            {
                int characterOffset = partyOffset + 0x94 * i;
                LegacyMemoryReader.WriteByte(StructHelper.GetFieldOffset<PartyMember>("SphereLevelCurrent", characterOffset), 255);
            }
        }

        public static void LearnAllAbilities()
        {
            var partyOffset = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);
            for (var i = 0; i < 18; i++)
            {
                
                int characterAbilityOffset = partyOffset + Marshal.SizeOf<PartyMember>() * i + StructHelper.GetFieldOffset<PartyMember>("SkillFlags"); ;
                var currentAbilities = LegacyMemoryReader.ReadBytes(characterAbilityOffset, 13);

                // Flip all normal ability bits
                currentAbilities[1] |= 0xF0;
                for (int b = 2; b < 11; b++)
                    currentAbilities[b] |= 0xFF;
                currentAbilities[11] |= 0x0F;
                currentAbilities[12] |= 0xFF;

                LegacyMemoryReader.WriteBytes(characterAbilityOffset, currentAbilities);
            }
        }
    }
}

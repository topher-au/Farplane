using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farplane.FFX.Values;

namespace Farplane.FFX
{
    public static class Cheats
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
                MemoryReader.WriteBytes(characterOffset + (int)PartyStatOffset.CurrentHp, BitConverter.GetBytes((uint) 99999));
                MemoryReader.WriteBytes(characterOffset + (int)PartyStatOffset.MaxHp, BitConverter.GetBytes((uint)99999));
                MemoryReader.WriteBytes(characterOffset + (int)PartyStatOffset.BaseHp, BitConverter.GetBytes((uint)99999));
                MemoryReader.WriteBytes(characterOffset + (int)PartyStatOffset.CurrentMp, BitConverter.GetBytes((uint)9999));
                MemoryReader.WriteBytes(characterOffset + (int)PartyStatOffset.MaxMp, BitConverter.GetBytes((uint)9999));
                MemoryReader.WriteBytes(characterOffset + (int)PartyStatOffset.BaseMp, BitConverter.GetBytes((uint)9999));
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseStrength, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseDefense, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseMagic, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseMagicDefense, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseAgility, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseLuck, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseEvasion, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.BaseAccuracy, (byte)255);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.OverdriveLevel, (byte)overdriveLevel);
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.OverdriveMax, (byte)overdriveLevel);
            }
        }

        public static void MaxSphereLevels()
        {
            var partyOffset = Offsets.GetOffset(OffsetType.PartyStatsBase);
            for (var i = 0; i < 8; i++)
            {
                int characterOffset = partyOffset + 0x94 * i;
                MemoryReader.WriteByte(characterOffset + (int)PartyStatOffset.SphereLevelCurrent, 255);
            }
        }

        public static void LearnAllAbilities()
        {
            var partyOffset = Offsets.GetOffset(OffsetType.PartyStatsBase);
            for (var i = 0; i < 18; i++)
            {
                int characterAbilityOffset = partyOffset + 0x94 * i + (int)PartyStatOffset.SkillFlags;
                var currentAbilities = MemoryReader.ReadBytes(characterAbilityOffset, 13);

                // Flip all normal ability bits
                currentAbilities[1] |= 0xF0;
                for (int b = 2; b < 11; b++)
                    currentAbilities[b] |= 0xFF;
                currentAbilities[11] |= 0x0F;
                currentAbilities[12] |= 0xFF;

                MemoryReader.WriteBytes(characterAbilityOffset, currentAbilities);
            }
        }
    }
}

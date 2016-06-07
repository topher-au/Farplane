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

        public static void RemoveDamageLimit()
        {
            var offset = Offsets.GetOffset(OffsetType.RemoveDamageLimit);
            var bytesToWrite = new byte[] 
            {
                0x90, 0x90, 0x90, 0x90,         // db 90 90 90 90
                0xBB, 0xFF, 0xFF, 0xFF, 0x7F    // mov ebx, 0x7FFFFFFF
            };       
                                                                                                    
            MemoryReader.WriteBytes(offset, bytesToWrite);
        }

        public static void RemoveHPLimit()
        {
            var offset = Offsets.GetOffset(OffsetType.RemoveHPLimit);
            var bytesToWrite = new byte[]
            {
                0xB8, 0xFF, 0xFF, 0xFF, 0x7F    // mov eax, 0x7FFFFFFF
            };     
            MemoryReader.WriteBytes(offset, bytesToWrite);

            offset = Offsets.GetOffset(OffsetType.RemoveHPCheck);
            bytesToWrite = new byte[]
            {
                0x25, 0xFF, 0xFF, 0xFF, 0x7F,   // and eax,7FFFFFFF
                0x90,                           // nop
                0x90,                           // nop
                0x90,                           // nop
                0x90,                           // nop
                0x90,                           // nop
            };         
            MemoryReader.WriteBytes(offset, bytesToWrite);
        }

        public static void RemoveMPLimit()
        {
            var offset = Offsets.GetOffset(OffsetType.RemoveMPLimit);
            var bytesToWrite = new byte[]
            {
                0xB8, 0xFF, 0xFF, 0xFF, 0x7F    // mov eax, 0x7FFFFFFF
            };     
            MemoryReader.WriteBytes(offset, bytesToWrite);

            offset = Offsets.GetOffset(OffsetType.RemoveMPCheck);
            bytesToWrite = new byte[]
            {
                0x68, 0xFF, 0xFF, 0xFF, 0x7F    // push 0x7FFFFFFF
            };         
            MemoryReader.WriteBytes(offset, bytesToWrite);
        }
    }
}

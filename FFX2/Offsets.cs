using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX2
{
    public enum OffsetType
    {
        CreatureBase = 0xA01E40, // Block Length = 0x80, Block Count = 8
        CreatureNames = 0xA06254,
        CreatureAbilities = 0xA06A80,
        CreatureTrapBase = 0x9F99FC,
        CreaturePodBase = 0x9FA60C,
        PartyStatBase = 0xA016C0, // Block Length = 0x80, Block Count = 3
        AccessoryType = 0XA01190,
        AccessoryCount = 0xA01290,
        AbilityBase = 0xA022A0, // Block Length = 0x6A0, Block Count = 3
        CurrentGil = 0xA00CE8,
        KnownGarmentGrids = 0xA00D14,
        DressphereCountBase = 0xA00D1C,
        ItemType = 0xA00E50,
        ItemCount = 0xA01050,
        RemoveHPLimit,
        RemoveMPLimit = 0x20E763, // B8 3F 42 0F 00 EB 16
        // FFX-2.exe+20E763 - B8 3F420F00           - mov eax,000F423F { 999999 }
        // FFX-2.exe+20E768 - EB 16                 - jmp FFX-2.exe+20E780


}

public static class Offsets
    {
        public static int GetOffset()
        {
            return 0;
        }

        public enum StatOffsets : int
        {
            HPModifier = 0x4,
            MPModifier = 0x8,
            ModStrength = 0xc,
            ModDefense = 0xd,
            ModMagic = 0xe,
            ModMagicDefense = 0xf,
            ModAgility = 0x10,
            ModAccuracy = 0x11,
            ModEvasion = 0x12,
            ModLuck = 0x13,
            CurrentExperience = 0x14,
            NeededExperience = 0x18,
            CurrentHP = 0x1C,
            CurrentMP = 0x20,
            MaxHP = 0x24,
            MaxMP = 0x28,
            Strength = 0x2D,
            Defense = 0x2E,
            Magic = 0x2F,
            MagicDefense = 0x30,
            Agility = 0x31,
            Accuracy = 0x32,
            Evasion = 0x33,
            Luck = 0x34,
            Size = 0x7A,
        }

        public enum DebugFlags : int
        {
            AllyInvincible = 0x9F78B8,
            EnemyInvincible = 0x9F78B9,
            ControlEnemies = 0x9F78BE,
            ControlMonsters = 0x9F78BF,
            MPZero = 0x9F78C5,
            InfoOutput = 0x9F78CB,
            AlwaysCritical = 0x9F78CC,
            Critical = 0x9F78CD,
            Probability100 = 0x9F78CE,
            DamageRandom = 0x9F78D1,
            Damage1 = 0x9F78D2,
            Damage9999 = 0x9F78D3,
            Damage99999 = 0x9F78D4,
            RareDrop100 = 0x9F78D5,
            EXP100x = 0x9F78D6,
            Gil100x = 0x9F78D7,
            AlwaysOversoul = 0x9F78D9,
            FirstAttack = 0x9F78E4 // 0xFF == OFF
        }

        public enum Party
        {
            Yuna,
            Rikku,
            Paine
        }
    }
}
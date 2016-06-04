using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX
{
    public static class Offsets
    {
        private static Dictionary<OffsetType, int> _offsetList = new Dictionary<OffsetType, int>()
        {
            {OffsetType.EquipmentBase, 0xD30F2C},
            {OffsetType.PartyStatsBase, 0xD32060},
            {OffsetType.ItemTypes, 0xD3095C},
            {OffsetType.ItemCounts, 0xD30B5C},
            {OffsetType.DebugFlags, 0xD2A8F8},
            {OffsetType.PartyInBattleFlags, 0x1F10EA0},
            {OffsetType.PartyGainedApFlags, 0x1F10EC4},
            {OffsetType.AeonNames, 0xD32E7C},
            {OffsetType.KeyItems, 0xD30F1C},
            {OffsetType.AlBhed, 0xD307A0},
            {OffsetType.SphereGridNodes, 0x12AE078 },
            {OffsetType.SphereGridCursor, 0x12BEB6C },
        };

        public static int[] AeonNames => new int[]
        {
            0x00,
            0x14,
            0x28,
            0x3C,
            0x50,
            0x64,
            0x78,
            0x8C,
            0xA0,
            0xB4,
        };

        public static int GetOffset(OffsetType offsetType)
        {
            var offset = _offsetList[offsetType];
            return offset;
        }
    }

    public enum OffsetType
    {
        EquipmentBase,
        PartyStatsBase,
        ItemTypes,
        ItemCounts,
        DebugFlags,
        PartyInBattleFlags,
        PartyGainedApFlags,
        AeonNames,
        KeyItems,
        AlBhed,
        SphereGridNodes,
        SphereGridCursor
    }

    public enum EquipmentOffset
    {
        Name = 0,
        DamageFormula = 2,
        Character = 4,
        Type = 5,
        AbilityCount = 0xB,
        Appearance = 0x0C,
        Ability0 = 0xE,
        Ability1 = 0x10,
        Ability2 = 0x12,
        Ability3 = 0x14
    }

    public enum PartyStatOffset
    {
        BaseHp = 0x00,
        BaseMp = 0x04,
        BaseStrength = 0x08,
        BaseDefense = 0x09,
        BaseMagic = 0x0A,
        BaseMagicDefense = 0x0B,
        BaseAgility = 0x0C,
        BaseLuck = 0x0D,
        BaseEvasion = 0x0E,
        BaseAccuracy = 0x0F,
        ApTotal = 0x10,
        ApCurrent = 0x14,
        CurrentHp = 0x18,
        CurrentMp = 0x1C,
        MaxHp = 0x20,
        MaxMp = 0x24,
        InParty = 0x28,
        EquippedWeapon = 0x29,
        EquippedArmor = 0x2a,
        CurrentStrength = 0x2b,
        CurrentDefense = 0x2c,
        CurrentMagic = 0x2d,
        CurrentMagicDefense = 0x2e,
        CurrentAgility = 0x2f,
        CurrentLuck = 0x30,
        CurrentEvasion = 0x31,
        CurrentAccuracy = 0x32,
        PoisonDamage = 0x33,
        OverdriveMode = 0x34,
        OverdriveLevel = 0x35,
        OverdriveMax = 0x36,
        SphereLevelCurrent = 0x37,
        SphereLevelTotal = 0x38,
        SkillFlags = 0x39,
        TotalBattles = 0x4c,
        TotalKills = 0x50,
        OverdriveWarrior = 0x5c,
        OverdriveComrade = 0x5e,
        OverdriveStoic = 0x60,
        OverdriveHealer = 0x62,
        OverdriveTactician = 0x64,
        OverdriveVictim = 0x66,
        OverdriveDancer = 0x68,
        OverdriveAvenger = 0x6a,
        OverdriveSlayer = 0x6c,
        OverdriveHero = 0x6e,
        OverdriveRook = 0x70,
        OverdriveVictor = 0x72,
        OverdriveCoward = 0x74,
        OverdriveAlly = 0x76,
        OverdriveSufferer = 0x78,
        OverdriveDaredevil = 0x7a,
        OverdriveLoner = 0x7c,
        OverdriveBlank1 = 0x7e,
        OverdriveBlank2 = 0x80,
        OverdriveAeonsOnly = 0x82,
        OverdriveModes = 0x84
    }

    public enum DebugFlags
    {
        EnemyInvincible,
        PartyInvincible,
        EnemyInput,
        Unknown1,
        FreeCamera,
        Unknown2,
        Unknown3,
        Unknown4,
        Unknown5,
        Unknown6,
        Unknown7,
        Unknown8,
        Unknown9,
        Unknown10,
        Unknown11,
        Unknown12,
        Unknown13,
        Unknown14,
        Unknown15,
        Unknown16,
        AlwaysOverdrive,
        AlwaysCritical,
        AlwaysDeal1,
        AlwaysDeal10000,
        AlwaysDeal99999,
        AlwaysRareDrop,
        Ap100X,
        Gil100X,
        Unknown18,
        PermanentSensor,
        Unknown19,
        UnknownHangGame,
    }
}
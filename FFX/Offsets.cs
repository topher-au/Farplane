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
            {OffsetType.PartyStatsBase, 0xD32078},
            {OffsetType.ItemTypes, 0xD3095C},
            {OffsetType.ItemCounts, 0xD30B5C},
            {OffsetType.DebugFlags, 0xD2A8F8},
            {OffsetType.PartyInBattleFlags, 0x1F10EA0},
            {OffsetType.PartyGainedApFlags, 0x1F10EC4},
            {OffsetType.AeonNames, 0xD32E7C}
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
            0xB4
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
        HPCurrent = 0x00,
        MPCurrent = 0x04,
        HPMax = 0x08,
        MPMax = 0x0C,
        InParty = 0x10,
        Strength = 0x13,
        Defense = 0x14,
        Magic = 0x15,
        MagicDefense = 0x16,
        Agility = 0x17,
        Luck = 0x18,
        Evasion = 0x19,
        Accuracy = 0x1a,
        OverdriveMode = 0x1c,
        OverdriveLevel = 0x1d,
        OverdriveMax = 0x1e,
        SphereLevelCurrent = 0x1f,
        SphereLevelTotal = 0x20,
        SkillFlags = 0x22,
    }

    public enum DebugFlagOffset
    {
        EnemyInvincible = 0x00,
        PartyInvincible = 0x01,
        EnemyControl = 0x02,
        AlwaysCritical = 0x15,
        AlwaysDeal1 = 0x16,
        AlwaysDeal10000 = 0x17,
        AlwaysDeal99999 = 0x18,
        AlwaysRareDrop = 0x19,
        BattleAPx100 = 0x1A,
        BattleGilx100 = 0x1B,
        SensorEnabled = 0x1D
    }
    
}

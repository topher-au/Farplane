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
            { OffsetType.EquipmentBase, 0xD30F2C },
            { OffsetType.PartyStatsBase, 0xD32078 },
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
        PartyStatsBase
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
    }

    
}

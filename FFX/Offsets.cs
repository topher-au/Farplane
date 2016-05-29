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
            { OffsetType.EquipmentBase, 0xD30F2C }
        };

        public static int GetOffset(OffsetType offsetType)
        {
            var offset = _offsetList[offsetType];
            return offset;
        }
    }

    public enum OffsetType
    {
        EquipmentBase
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

    
}

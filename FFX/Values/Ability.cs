using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public class Ability
    {
        public static Ability[] Abilities =
        {
            new Ability {BitOffset = 0x08, Name="Sleep Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x09, Name="Silence Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0A, Name="Dark Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0C, Name="Sleep Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0D, Name="Silence Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0E, Name="Dark Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0B, Name="Zombie Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0F, Name="Triple Foul", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x06, Name="Delay Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x07, Name="Delay Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x5a, Name="Extract Power", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x5b, Name="Extract Mana", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x5c, Name="Extract Speed", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x5d, Name="Extract Ability", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x10, Name="Power Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x11, Name="Magic Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x12, Name="Armor Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x13, Name="Mental Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x59, Name="Full Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x14, Name="Mug", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x5e, Name="Nab Gil", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x15, Name="Quick Hit", Type = AbilityType.Skill},
            
            new Ability {BitOffset = 0x16, Name="Steal", Type = AbilityType.Special},
            new Ability {BitOffset = 0x17, Name="Use", Type = AbilityType.Special},
            new Ability {BitOffset = 0x18, Name="Flee", Type = AbilityType.Special},
            new Ability {BitOffset = 0x19, Name="Pray", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1a, Name="Cheer", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1b, Name="Aim", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1c, Name="Focus", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1d, Name="Reflex", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1e, Name="Luck", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1f, Name="Jinx", Type = AbilityType.Special},
            new Ability {BitOffset = 0x20, Name="Lancet", Type = AbilityType.Special},
            new Ability {BitOffset = 0x22, Name="Guard", Type = AbilityType.Special},
            new Ability {BitOffset = 0x23, Name="Sentinel", Type = AbilityType.Special},
            new Ability {BitOffset = 0x24, Name="Spare Change", Type = AbilityType.Special},
            new Ability {BitOffset = 0x25, Name="Threaten", Type = AbilityType.Special},
            new Ability {BitOffset = 0x26, Name="Provoke", Type = AbilityType.Special},
            new Ability {BitOffset = 0x27, Name="Entrust", Type = AbilityType.Special},
            new Ability {BitOffset = 0x28, Name="Copycat", Type = AbilityType.Special},
            new Ability {BitOffset = 0x58, Name="Pilfer Gil", Type = AbilityType.Special},
            new Ability {BitOffset = 0x5f, Name="Quick Pockets", Type = AbilityType.Special},
            new Ability {BitOffset = 0x29, Name="Doublecast", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2a, Name="Bribe", Type = AbilityType.Special},

            new Ability {BitOffset = 0x2b, Name="Cure", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x2c, Name="Cura", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x2d, Name="Curaga", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x2f, Name="NulBlaze", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x30, Name="NulShock", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x31, Name="NulTide", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x2e, Name="NulFrost", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x32, Name="Scan", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x33, Name="Esuna", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x34, Name="Life", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x35, Name="Full-Life", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x36, Name="Haste", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x37, Name="Hastega", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x38, Name="Slow", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x39, Name="Slowga", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3a, Name="Shell", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3b, Name="Protect", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3c, Name="Reflect", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3d, Name="Dispel", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3e, Name="Regen", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3f, Name="Holy", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x40, Name="Auto-Life", Type = AbilityType.WhiteMagic},

            new Ability {BitOffset = 0x42, Name="Fire", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x43, Name="Thunder", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x44, Name="Water", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x41, Name="Blizzard", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x45, Name="Fira", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x47, Name="Thundara", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x48, Name="Watera", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x46, Name="Blizzara", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x49, Name="Firaga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4b, Name="Thundaga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4c, Name="Waterga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4a, Name="Blizzaga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4d, Name="Bio", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4e, Name="Demi", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4f, Name="Death", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x50, Name="Drain", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x51, Name="Osmose", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x52, Name="Flare", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x53, Name="Ultima", Type = AbilityType.BlackMagic},
        };

        public int BitOffset { get; set; }
        public int ID { get; set; }
        public AbilityType Type { get; set; }
        public string Name { get; set; }
    }

    public enum AbilityType
    {
        None,
        Skill,
        Special,
        WhiteMagic,
        BlackMagic
    }
}

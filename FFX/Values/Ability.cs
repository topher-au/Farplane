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
            new Ability() {BitOffset=0x06, Name="Dismiss", Type = AbilityType.Command },
            new Ability() {BitOffset=0x07, Name="Attack", Type = AbilityType.Command },
            new Ability() {BitOffset=0x08, Name="Switch", Type = AbilityType.Command },
            new Ability() {BitOffset=0x09, Name="Item", Type = AbilityType.Command },
            new Ability() {BitOffset=0x0a, Name="Weapon", Type = AbilityType.Command },
            new Ability() {BitOffset=0x0b, Name="Armor", Type = AbilityType.Command },
            new Ability() {BitOffset=0x0c, Name="Escape", Type = AbilityType.Command },
            new Ability() {BitOffset=0x0d, Name="Shield", Type = AbilityType.Command },
            new Ability() {BitOffset=0x0e, Name="Boost", Type = AbilityType.Command },
            new Ability() {BitOffset=0x0f, Name="Dismiss", Type = AbilityType.Command },
            new Ability {BitOffset = 0x10, Name="Sleep Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x11, Name="Silence Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x12, Name="Dark Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x14, Name="Sleep Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x15, Name="Silence Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x16, Name="Dark Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x13, Name="Zombie Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x17, Name="Triple Foul", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0E, Name="Delay Attack", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x0F, Name="Delay Buster", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x62, Name="Extract Power", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x63, Name="Extract Mana", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x64, Name="Extract Speed", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x65, Name="Extract Ability", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x18, Name="Power Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x19, Name="Magic Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x1a, Name="Armor Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x1b, Name="Mental Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x61, Name="Full Break", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x1c, Name="Mug", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x66, Name="Nab Gil", Type = AbilityType.Skill},
            new Ability {BitOffset = 0x1D, Name="Quick Hit", Type = AbilityType.Skill},
            
            new Ability {BitOffset = 0x1E, Name="Steal", Type = AbilityType.Special},
            new Ability {BitOffset = 0x1F, Name="Use", Type = AbilityType.Special},
            new Ability {BitOffset = 0x20, Name="Flee", Type = AbilityType.Special},
            new Ability {BitOffset = 0x21, Name="Pray", Type = AbilityType.Special},
            new Ability {BitOffset = 0x22, Name="Cheer", Type = AbilityType.Special},
            new Ability {BitOffset = 0x23, Name="Aim", Type = AbilityType.Special},
            new Ability {BitOffset = 0x24, Name="Focus", Type = AbilityType.Special},
            new Ability {BitOffset = 0x25, Name="Reflex", Type = AbilityType.Special},
            new Ability {BitOffset = 0x26, Name="Luck", Type = AbilityType.Special},
            new Ability {BitOffset = 0x27, Name="Jinx", Type = AbilityType.Special},
            new Ability {BitOffset = 0x28, Name="Lancet", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2a, Name="Guard", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2b, Name="Sentinel", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2c, Name="Spare Change", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2d, Name="Threaten", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2e, Name="Provoke", Type = AbilityType.Special},
            new Ability {BitOffset = 0x2f, Name="Entrust", Type = AbilityType.Special},
            new Ability {BitOffset = 0x30, Name="Copycat", Type = AbilityType.Special},
            new Ability {BitOffset = 0x60, Name="Pilfer Gil", Type = AbilityType.Special},
            new Ability {BitOffset = 0x67, Name="Quick Pockets", Type = AbilityType.Special},
            new Ability {BitOffset = 0x31, Name="Doublecast", Type = AbilityType.Special},
            new Ability {BitOffset = 0x32, Name="Bribe", Type = AbilityType.Special},

            new Ability {BitOffset = 0x33, Name="Cure", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x34, Name="Cura", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x35, Name="Curaga", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x37, Name="NulBlaze", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x38, Name="NulShock", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x39, Name="NulTide", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x36, Name="NulFrost", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3a, Name="Scan", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3b, Name="Esuna", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3c, Name="Life", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3d, Name="Full-Life", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3e, Name="Haste", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x3f, Name="Hastega", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x40, Name="Slow", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x41, Name="Slowga", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x42, Name="Shell", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x43, Name="Protect", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x44, Name="Reflect", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x45, Name="Dispel", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x46, Name="Regen", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x47, Name="Holy", Type = AbilityType.WhiteMagic},
            new Ability {BitOffset = 0x48, Name="Auto-Life", Type = AbilityType.WhiteMagic},

            new Ability {BitOffset = 0x4A, Name="Fire", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4B, Name="Thunder", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4C, Name="Water", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x49, Name="Blizzard", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4D, Name="Fira", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4F, Name="Thundara", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x50, Name="Watera", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x4E, Name="Blizzara", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x51, Name="Firaga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x53, Name="Thundaga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x54, Name="Waterga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x52, Name="Blizzaga", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x55, Name="Bio", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x56, Name="Demi", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x57, Name="Death", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x58, Name="Drain", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x59, Name="Osmose", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x5a, Name="Flare", Type = AbilityType.BlackMagic},
            new Ability {BitOffset = 0x5b, Name="Ultima", Type = AbilityType.BlackMagic},
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
        BlackMagic,
        Command
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX2
{
    public static class Offsets
    {

        public static class Creatures
        {
            public static readonly int CreatureBase = 0xA01E40;
            public static readonly int CreatureNames = 0xA06254;
            public static readonly int CreatureAbilities = 0xA06A80;
            public static readonly int CreatureTrapBase = 0x9F99FC;
            public static readonly int CreaturePodBase = 0x9FA60C;
        }
        public static class StatBases
        {
            public static readonly int Yuna = 0x0A016C0;
            public static readonly int Rikku = 0x0A01740;
            public static readonly int Paine = 0x0A017C0;
            
        }

        public static class Accessories
        {
                public static readonly int AccessoriesBase = 0xA01190;
                public static readonly int QuantityBase = 0xA01290;
            
        }

        public static class AbilityBases
        {
            public static readonly int Yuna = 0xA022A0;
            public static readonly int Rikku = 0xA02940;
            public static readonly int Paine = 0xA02FE0;
        }

        public static class General
        {
            public static readonly int Gil = 0xA00CE8;
            public static readonly int GarmentGridBase = 0xA00D14;
        }

        public static class Dresspheres
        {
            public static readonly int DressphereBase = 0xA00D1C;
        }

        public enum StatOffsets : int
        {
            HPModifier = 0x4,
            MPModifier = 0x8,
            ModStrength = 0xc,
            ModMagic = 0xd,
            ModDefense = 0xe,
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

        public static class Items
        {
            public static readonly int ItemBase = 0xA00E50;
            public static readonly int QuantityBase = 0xA01050;
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

        public enum Dressphere
        {
            None,
            Gunner,
            GunMage,
            Alchemist,
            Warrior,
            Samurai,
            DarkKnight,
            Berserker,
            Songstress,
            BlackMage,
            WhiteMage,
            Thief,
            Trainer,
            LadyLuck,
            Mascot,
            FloralFallal,
            RightPistil,
            LeftPistil,
            MachinaMaw,
            SmasherR,
            CrusherL,
            FullThrottle,
            DextralWing,
            SinistralWing,
            Psychic = 28,
            Festivalist = 29
        }

        public class Abilities
        {
            public enum Gunner
            {
                Attack,
                TriggerHappy,
                Potshot,
                CheapShot,
                EnchantedAmmo,
                TargetMP,
                QuarterPounder,
                OnTheLevel,
                BurstShot,
                Tableturner,
                Scattershot,
                Scatterburst,
                Darkproof,
                Unk,
                TriggerHappyLv2,
                TriggerHappyLv3
            }
        }
    }
}

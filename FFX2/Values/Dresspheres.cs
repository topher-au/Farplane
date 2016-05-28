namespace Farplane.FFX2.Values
{

    public static class Dresspheres
    {
        public static Dressphere[] GetDresspheres()
        {
            return new[]
            {
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
                Psychic,
                Festivalist,
                FloralFallal,
                MachinaMaw,
                FullThrottle
            };
        }

        public static Dressphere Gunner = new Dressphere
        {
            ID = 1,
            Name = "Gunner",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Trigger Happy", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Potshot", MasteredAP = 20, Offset=0x6},
                new DressphereAbility {Name = "Cheap Shot", MasteredAP = 30, Offset=0x8},
                new DressphereAbility {Name = "Enchanted Ammo", MasteredAP = 30, Offset=0xA},
                new DressphereAbility {Name = "Target MP", MasteredAP = 30, Offset=0xC},
                new DressphereAbility {Name = "Quarter Pounder", MasteredAP = 40, Offset=0xE},
                new DressphereAbility {Name = "On the Level", MasteredAP = 40, Offset=0x10},
                new DressphereAbility {Name = "Burst Shot", MasteredAP = 60, Offset=0x12},
                new DressphereAbility {Name = "Tableturner", MasteredAP = 60, Offset=0x14},
                new DressphereAbility {Name = "Scattershot", MasteredAP = 80, Offset=0x16},
                new DressphereAbility {Name = "Scatterburst", MasteredAP = 120, Offset=0x18},
                new DressphereAbility {Name = "Darkproof", MasteredAP = 30, Offset=0x4E4},
                new DressphereAbility {Name = "Sleepproof", MasteredAP = 30, Offset=0x4DC},
                new DressphereAbility {Name = "Trigger Happy Lv2", MasteredAP = 80, Offset=0x470},
                new DressphereAbility {Name = "Trigger Happy Lv3", MasteredAP = 150, Offset=0x472}
            }
        };

        public static Dressphere GunMage = new Dressphere
        {
            ID = 2,
            Name = "Gun Mage",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Blue Bullet", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Scan", Offset=-1},
                new DressphereAbility {Name = "Shell Cracker", MasteredAP = 20, Offset=0x1C},
                new DressphereAbility {Name = "Anti-Aircraft", MasteredAP = 20, Offset=0x1E},
                new DressphereAbility {Name = "Silver Bullet", MasteredAP = 20, Offset=0x20},
                new DressphereAbility {Name = "Flan Eater", MasteredAP = 20, Offset=0x22},
                new DressphereAbility {Name = "Elementillery", MasteredAP = 20, Offset=0x24},
                new DressphereAbility {Name = "Killasaurus", MasteredAP = 20, Offset=0x26},
                new DressphereAbility {Name = "Drake Slayer", MasteredAP = 20, Offset=0x28},
                new DressphereAbility {Name = "Dismantler", MasteredAP = 20, Offset=0x2A},
                new DressphereAbility {Name = "Mech Destroyer", MasteredAP = 20, Offset=0x2C},
                new DressphereAbility {Name = "Demon Muzzle", MasteredAP = 20, Offset=0x2E},
                new DressphereAbility {Name = "Fiend Hunter", MasteredAP = 30, Offset=0x47C},
                new DressphereAbility {Name = "Scan Lv2", MasteredAP = 20, Offset=0x474},
                new DressphereAbility {Name = "Scan Lv3", MasteredAP = 100, Offset=0x476}
            }
        };

        public static Dressphere Alchemist = new Dressphere
        {
            ID = 3,
            Name = "Alchemist",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Mix", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Potion", ReadOnly = true, Offset=0x52},
                new DressphereAbility {Name = "Hi-Potion", MasteredAP = 40, Offset=0x54},
                new DressphereAbility {Name = "Mega Potion", MasteredAP = 120, Offset=0x56},
                new DressphereAbility {Name = "X-Potion", MasteredAP = 160, Offset=0x58},
                new DressphereAbility {Name = "Remedy", MasteredAP = 20, Offset=0x5A},
                new DressphereAbility {Name = "Dispel Tonic", MasteredAP = 20, Offset=0x5C},
                new DressphereAbility {Name = "Phoenix Down", MasteredAP = 30, Offset=0x5E},
                new DressphereAbility {Name = "Mega Phoenix", MasteredAP = 200, Offset=0x60},
                new DressphereAbility {Name = "Ether", MasteredAP = 400, Offset=0x62},
                new DressphereAbility {Name = "Elixir", MasteredAP = 999, Offset=0x64},
                new DressphereAbility {Name = "Items Lv2", MasteredAP = 30, Offset=0x47E},
                new DressphereAbility {Name = "Chemist", MasteredAP = 40, Offset=0x44C},
                new DressphereAbility {Name = "Elementalist", MasteredAP = 80, Offset=0x44E},
                new DressphereAbility {Name = "Physicist", MasteredAP = 100, Offset=0x450},
            }
        };

        public static Dressphere Warrior = new Dressphere
        {
            ID = 4,
            Name = "Warrior",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Sentinel", MasteredAP = 20, Offset=0x68},
                new DressphereAbility {Name = "Flametongue", MasteredAP = 20, Offset=0x72},
                new DressphereAbility {Name = "Ice Brand", MasteredAP = 20, Offset=0x74},
                new DressphereAbility {Name = "Thunder Blade", MasteredAP = 20, Offset=0x76},
                new DressphereAbility {Name = "Liquid Steel", MasteredAP = 20, Offset=0x78},
                new DressphereAbility {Name = "Demi Sword", MasteredAP = 60, Offset=0x7A},
                new DressphereAbility {Name = "Excalibur", MasteredAP = 120, Offset=0x7C},
                new DressphereAbility {Name = "Power Break", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Armor Break", MasteredAP = 30, Offset=0x6C},
                new DressphereAbility {Name = "Magic Break", MasteredAP = 30, Offset=0x6E},
                new DressphereAbility {Name = "Mental Break", MasteredAP = 30, Offset=0x70},
                new DressphereAbility {Name = "Delay Attack", MasteredAP = 100, Offset=0x7E},
                new DressphereAbility {Name = "Delay Buster", MasteredAP = 120, Offset=0x80},
                new DressphereAbility {Name = "Assault", MasteredAP = 100, Offset=0x66},
                new DressphereAbility {Name = "SOS Protect", MasteredAP = 20, Offset=0x52A},
            }
        };

        public static Dressphere Samurai = new Dressphere
        {
            ID = 5,
            Name = "Samurai",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Spare Change", MasteredAP = 20, Offset=0x9A},
                new DressphereAbility {Name = "Mirror of Equity", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Magicide", MasteredAP = 30, Offset=0x84},
                new DressphereAbility {Name = "Dismissal", MasteredAP = 30, Offset=0x86},
                new DressphereAbility {Name = "Fingersnap", MasteredAP = 40, Offset=0x88},
                new DressphereAbility {Name = "Sparkler", MasteredAP = 10, Offset=0x8A},
                new DressphereAbility {Name = "Fireworks", MasteredAP = 60, Offset=0x8C},
                new DressphereAbility {Name = "Momentum", MasteredAP = 60, Offset=0x8E},
                new DressphereAbility {Name = "Shin-Zantetsu", MasteredAP = 100, Offset=0x90},
                new DressphereAbility {Name = "Nonpareil", MasteredAP = 20, Offset=0x92},
                new DressphereAbility {Name = "No Fear", MasteredAP = 30, Offset=0x94},
                new DressphereAbility {Name = "Clean Slate", MasteredAP = 40, Offset=0x96},
                new DressphereAbility {Name = "Hayate", MasteredAP = 60, Offset=0x98},
                new DressphereAbility {Name = "Zantetsu", MasteredAP = 140, Offset=0x9C},
                new DressphereAbility {Name = "SOS Critical", MasteredAP = 80, Offset=0x536},
            }
        };

        public static Dressphere DarkKnight = new Dressphere
        {
            ID = 6,
            Name = "Dark Knight",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Darkness", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Drain", MasteredAP = 30, Offset=0xA2},
                new DressphereAbility {Name = "Demi", MasteredAP = 30, Offset=0xA4},
                new DressphereAbility {Name = "Confuse", MasteredAP = 30, Offset=0xA6},
                new DressphereAbility {Name = "Break", MasteredAP = 30, Offset=0xA8},
                new DressphereAbility {Name = "Bio", MasteredAP = 40, Offset=0xAA},
                new DressphereAbility {Name = "Doom", MasteredAP = 10, Offset=0xAC},
                new DressphereAbility {Name = "Death", MasteredAP = 60, Offset=0xAE},
                new DressphereAbility {Name = "Black Sky", MasteredAP = 60, Offset=0xB0},
                new DressphereAbility {Name = "Charon", MasteredAP = 100, Offset=0xA0},
                new DressphereAbility {Name = "Poisonproof", MasteredAP = 30, Offset=0x4E8},
                new DressphereAbility {Name = "Stoneproof", MasteredAP = 30, Offset=0x4D8},
                new DressphereAbility {Name = "Confuseproof", MasteredAP = 40, Offset=0x4EC},
                new DressphereAbility {Name = "Curseproof", MasteredAP = 60, Offset=0x4F6},
                new DressphereAbility {Name = "Deathproof", MasteredAP = 140, Offset=0x4D4},
            }
        };

        public static Dressphere Berserker = new Dressphere
        {
            ID = 7,
            Name = "Berserker",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Attack", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Berserk", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Cripple", MasteredAP = 20, Offset=0xB6},
                new DressphereAbility {Name = "Mad Rush", MasteredAP = 30, Offset=0xB8},
                new DressphereAbility {Name = "Crackdown", MasteredAP = 30, Offset=0xBA},
                new DressphereAbility {Name = "Eject", MasteredAP = 40, Offset=0xBC},
                new DressphereAbility {Name = "Unhinge", MasteredAP = 40, Offset=0xBE},
                new DressphereAbility {Name = "Intimidate", MasteredAP = 50, Offset=0xC0},
                new DressphereAbility {Name = "Envenom", MasteredAP = 30, Offset=0xC2},
                new DressphereAbility {Name = "Hurt", MasteredAP = 60, Offset=0xC4},
                new DressphereAbility {Name = "Howl", MasteredAP = 80, Offset=0xB4},
                new DressphereAbility {Name = "Itchproof", MasteredAP = 20, Offset=0x4FA},
                new DressphereAbility {Name = "Counterattack", MasteredAP = 180, Offset=0x444},
                new DressphereAbility {Name = "Magic Counter", MasteredAP = 300, Offset=0x448},
                new DressphereAbility {Name = "Evade & Counter", MasteredAP = 400, Offset=0x446},
                new DressphereAbility {Name = "Auto-Regen", MasteredAP = 80, Offset=0x51C},
            }
        };

        public static Dressphere Songstress = new Dressphere
        {
            ID = 8,
            Name = "Songstress",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Darkness Dance", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere BlackMage = new Dressphere
        {
            ID = 9,
            Name = "Black Mage",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Black Mage", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Black Mage", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere WhiteMage = new Dressphere
        {
            ID = 10,
            Name = "White Mage",
            Abilities = new[]
            {
                new DressphereAbility {Name = "White Mage", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "White Mage", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "White Mage", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "White Mage", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "White Mage", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "White Mage", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "White Mage", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "White Mage", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "White Mage", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "White Mage", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "White Mage", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "White Mage", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "White Mage", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "White Mage", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "White Mage", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "White Mage", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere Thief = new Dressphere
        {
            ID = 11,
            Name = "Thief",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Thief", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere Trainer = new Dressphere
        {
            ID = 12,
            Name = "Trainer",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Trainer", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere LadyLuck = new Dressphere
        {
            ID = 13,
            Name = "Lady Luck",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Lady Luck", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere Mascot = new Dressphere
        {
            ID = 14,
            Name = "Mascot",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Mascot", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere Psychic = new Dressphere
        {
            ID = 15, //28
            Name = "Psychic",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Psychic", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere Festivalist = new Dressphere
        {
            ID = 16, //29
            Name = "Festivalist",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Festivalist", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere FloralFallal = new Dressphere
        {
            ID = 17, //15
            Name = "Floral Fallal",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Floral Fallal", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere MachinaMaw = new Dressphere
        {
            ID = 18, //18
            Name = "Machina Maw",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Machina Maw", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };

        public static Dressphere FullThrottle = new Dressphere
        {
            ID = 19, //21
            Name = "Full Throttle",
            Abilities = new[]
            {
                new DressphereAbility {Name = "Full Throttle", ReadOnly = true, Offset=-1},
                new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset=0xC8},
                new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset=0xCA},
                new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset=0xCC},
                new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset=0xCE},
                new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset=0xD0},
                new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset=0xD2},
                new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset=0xD4},
                new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset=0xD6},
                new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset=0xD8},
                new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset=0xDA},
                new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset=0xDC},
                new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset=0xDE},
                new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset=0xE0},
                new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset=0xE2},
                new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset=0xE4},
            }
        };
    }

    public class Dressphere
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DressphereAbility[] Abilities { get; set; }
    }

    public class DressphereAbility
    {
        public string Name { get; set; }
        public bool ReadOnly { get; set; }
        public int MasteredAP { get; set; }
        public int Offset { get; set; }
    }


}

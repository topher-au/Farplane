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
				RightPistil,
				LeftPistil,
				MachinaMaw,
				SmasherR,
				CrusherL,
				FullThrottle,
				DextralWing,
				SinistralWing
			};
		}

		public static Dressphere Gunner = new Dressphere
		{
			ID = 1,
			Name = "Gunner",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Trigger Happy", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Potshot", MasteredAP = 20, Offset = 0x6},
				new DressphereAbility {Name = "Cheap Shot", MasteredAP = 30, Offset = 0x8},
				new DressphereAbility {Name = "Enchanted Ammo", MasteredAP = 30, Offset = 0xA},
				new DressphereAbility {Name = "Target MP", MasteredAP = 30, Offset = 0xC},
				new DressphereAbility {Name = "Quarter Pounder", MasteredAP = 40, Offset = 0xE},
				new DressphereAbility {Name = "On the Level", MasteredAP = 40, Offset = 0x10},
				new DressphereAbility {Name = "Burst Shot", MasteredAP = 60, Offset = 0x12},
				new DressphereAbility {Name = "Tableturner", MasteredAP = 60, Offset = 0x14},
				new DressphereAbility {Name = "Scattershot", MasteredAP = 80, Offset = 0x16},
				new DressphereAbility {Name = "Scatterburst", MasteredAP = 120, Offset = 0x18},
				new DressphereAbility {Name = "Darkproof", MasteredAP = 30, Offset = 0x4E4},
				new DressphereAbility {Name = "Sleepproof", MasteredAP = 30, Offset = 0x4DC},
				new DressphereAbility {Name = "Trigger Happy Lv2", MasteredAP = 80, Offset = 0x470},
				new DressphereAbility {Name = "Trigger Happy Lv3", MasteredAP = 150, Offset = 0x472}
			}
		};

		public static Dressphere GunMage = new Dressphere
		{
			ID = 2,
			Name = "Gun Mage",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Blue Bullet", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Scan", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Shell Cracker", MasteredAP = 20, Offset = 0x1C},
				new DressphereAbility {Name = "Anti-Aircraft", MasteredAP = 20, Offset = 0x1E},
				new DressphereAbility {Name = "Silver Bullet", MasteredAP = 20, Offset = 0x20},
				new DressphereAbility {Name = "Flan Eater", MasteredAP = 20, Offset = 0x22},
				new DressphereAbility {Name = "Elementillery", MasteredAP = 20, Offset = 0x24},
				new DressphereAbility {Name = "Killasaurus", MasteredAP = 20, Offset = 0x26},
				new DressphereAbility {Name = "Drake Slayer", MasteredAP = 20, Offset = 0x28},
				new DressphereAbility {Name = "Dismantler", MasteredAP = 20, Offset = 0x2A},
				new DressphereAbility {Name = "Mech Destroyer", MasteredAP = 20, Offset = 0x2C},
				new DressphereAbility {Name = "Demon Muzzle", MasteredAP = 20, Offset = 0x2E},
				new DressphereAbility {Name = "Fiend Hunter", MasteredAP = 30, Offset = 0x47C},
				new DressphereAbility {Name = "Scan Lv2", MasteredAP = 20, Offset = 0x474},
				new DressphereAbility {Name = "Scan Lv3", MasteredAP = 100, Offset = 0x476}
			}
		};

		public static Dressphere Alchemist = new Dressphere
		{
			ID = 3,
			Name = "Alchemist",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Mix", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Potion", MasteredAP = 10, Offset = 0x52},
				new DressphereAbility {Name = "Hi-Potion", MasteredAP = 40, Offset = 0x54},
				new DressphereAbility {Name = "Mega Potion", MasteredAP = 120, Offset = 0x56},
				new DressphereAbility {Name = "X-Potion", MasteredAP = 160, Offset = 0x58},
				new DressphereAbility {Name = "Remedy", MasteredAP = 20, Offset = 0x5A},
				new DressphereAbility {Name = "Dispel Tonic", MasteredAP = 20, Offset = 0x5C},
				new DressphereAbility {Name = "Phoenix Down", MasteredAP = 30, Offset = 0x5E},
				new DressphereAbility {Name = "Mega Phoenix", MasteredAP = 200, Offset = 0x60},
				new DressphereAbility {Name = "Ether", MasteredAP = 400, Offset = 0x62},
				new DressphereAbility {Name = "Elixir", MasteredAP = 999, Offset = 0x64},
				new DressphereAbility {Name = "Items Lv2", MasteredAP = 30, Offset = 0x47E},
				new DressphereAbility {Name = "Chemist", MasteredAP = 40, Offset = 0x44C},
				new DressphereAbility {Name = "Elementalist", MasteredAP = 80, Offset = 0x44E},
				new DressphereAbility {Name = "Physicist", MasteredAP = 100, Offset = 0x450},
			}
		};

		public static Dressphere Warrior = new Dressphere
		{
			ID = 4,
			Name = "Warrior",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Sentinel", MasteredAP = 20, Offset = 0x68},
				new DressphereAbility {Name = "Flametongue", MasteredAP = 20, Offset = 0x72},
				new DressphereAbility {Name = "Ice Brand", MasteredAP = 20, Offset = 0x74},
				new DressphereAbility {Name = "Thunder Blade", MasteredAP = 20, Offset = 0x76},
				new DressphereAbility {Name = "Liquid Steel", MasteredAP = 20, Offset = 0x78},
				new DressphereAbility {Name = "Demi Sword", MasteredAP = 60, Offset = 0x7A},
				new DressphereAbility {Name = "Excalibur", MasteredAP = 120, Offset = 0x7C},
				new DressphereAbility {Name = "Power Break", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Armor Break", MasteredAP = 30, Offset = 0x6C},
				new DressphereAbility {Name = "Magic Break", MasteredAP = 30, Offset = 0x6E},
				new DressphereAbility {Name = "Mental Break", MasteredAP = 30, Offset = 0x70},
				new DressphereAbility {Name = "Delay Attack", MasteredAP = 100, Offset = 0x7E},
				new DressphereAbility {Name = "Delay Buster", MasteredAP = 120, Offset = 0x80},
				new DressphereAbility {Name = "Assault", MasteredAP = 100, Offset = 0x66},
				new DressphereAbility {Name = "SOS Protect", MasteredAP = 20, Offset = 0x52A},
			}
		};

		public static Dressphere Samurai = new Dressphere
		{
			ID = 5,
			Name = "Samurai",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Spare Change", MasteredAP = 20, Offset = 0x9A},
				new DressphereAbility {Name = "Mirror of Equity", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Magicide", MasteredAP = 30, Offset = 0x84},
				new DressphereAbility {Name = "Dismissal", MasteredAP = 30, Offset = 0x86},
				new DressphereAbility {Name = "Fingersnap", MasteredAP = 40, Offset = 0x88},
				new DressphereAbility {Name = "Sparkler", MasteredAP = 40, Offset = 0x8A},
				new DressphereAbility {Name = "Fireworks", MasteredAP = 60, Offset = 0x8C},
				new DressphereAbility {Name = "Momentum", MasteredAP = 60, Offset = 0x8E},
				new DressphereAbility {Name = "Shin-Zantetsu", MasteredAP = 100, Offset = 0x90},
				new DressphereAbility {Name = "Nonpareil", MasteredAP = 20, Offset = 0x92},
				new DressphereAbility {Name = "No Fear", MasteredAP = 30, Offset = 0x94},
				new DressphereAbility {Name = "Clean Slate", MasteredAP = 40, Offset = 0x96},
				new DressphereAbility {Name = "Hayate", MasteredAP = 60, Offset = 0x98},
				new DressphereAbility {Name = "Zantetsu", MasteredAP = 140, Offset = 0x9C},
				new DressphereAbility {Name = "SOS Critical", MasteredAP = 80, Offset = 0x536},
			}
		};

		public static Dressphere DarkKnight = new Dressphere
		{
			ID = 6,
			Name = "Dark Knight",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Darkness", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Drain", MasteredAP = 30, Offset = 0xA2},
				new DressphereAbility {Name = "Demi", MasteredAP = 30, Offset = 0xA4},
				new DressphereAbility {Name = "Confuse", MasteredAP = 30, Offset = 0xA6},
				new DressphereAbility {Name = "Break", MasteredAP = 40, Offset = 0xA8},
				new DressphereAbility {Name = "Bio", MasteredAP = 40, Offset = 0xAA},
				new DressphereAbility {Name = "Doom", MasteredAP = 20, Offset = 0xAC},
				new DressphereAbility {Name = "Death", MasteredAP = 60, Offset = 0xAE},
				new DressphereAbility {Name = "Black Sky", MasteredAP = 100, Offset = 0xB0},
				new DressphereAbility {Name = "Charon", MasteredAP = 100, Offset = 0xA0},
				new DressphereAbility {Name = "Poisonproof", MasteredAP = 30, Offset = 0x4E8},
				new DressphereAbility {Name = "Stoneproof", MasteredAP = 30, Offset = 0x4D8},
				new DressphereAbility {Name = "Confuseproof", MasteredAP = 40, Offset = 0x4EC},
				new DressphereAbility {Name = "Curseproof", MasteredAP = 60, Offset = 0x4F6},
				new DressphereAbility {Name = "Deathproof", MasteredAP = 140, Offset = 0x4D4},
			}
		};

		public static Dressphere Berserker = new Dressphere
		{
			ID = 7,
			Name = "Berserker",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Berserk", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Cripple", MasteredAP = 20, Offset = 0xB6},
				new DressphereAbility {Name = "Mad Rush", MasteredAP = 30, Offset = 0xB8},
				new DressphereAbility {Name = "Crackdown", MasteredAP = 30, Offset = 0xBA},
				new DressphereAbility {Name = "Eject", MasteredAP = 40, Offset = 0xBC},
				new DressphereAbility {Name = "Unhinge", MasteredAP = 40, Offset = 0xBE},
				new DressphereAbility {Name = "Intimidate", MasteredAP = 50, Offset = 0xC0},
				new DressphereAbility {Name = "Envenom", MasteredAP = 30, Offset = 0xC2},
				new DressphereAbility {Name = "Hurt", MasteredAP = 60, Offset = 0xC4},
				new DressphereAbility {Name = "Howl", MasteredAP = 80, Offset = 0xB4},
				new DressphereAbility {Name = "Itchproof", MasteredAP = 20, Offset = 0x4FA},
				new DressphereAbility {Name = "Counterattack", MasteredAP = 180, Offset = 0x444},
				new DressphereAbility {Name = "Magic Counter", MasteredAP = 300, Offset = 0x448},
				new DressphereAbility {Name = "Evade & Counter", MasteredAP = 400, Offset = 0x446},
				new DressphereAbility {Name = "Auto-Regen", MasteredAP = 80, Offset = 0x51C},
			}
		};

		public static Dressphere Songstress = new Dressphere
		{
			ID = 8,
			Name = "Songstress",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Darkness Dance", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Samba of Silence", MasteredAP = 20, Offset = 0xC8},
				new DressphereAbility {Name = "MP Mambo", MasteredAP = 20, Offset = 0xCA},
				new DressphereAbility {Name = "Magical Masque", MasteredAP = 20, Offset = 0xCC},
				new DressphereAbility {Name = "Sleepy Shuffle", MasteredAP = 80, Offset = 0xCE},
				new DressphereAbility {Name = "Carnival Cancan", MasteredAP = 80, Offset = 0xD0},
				new DressphereAbility {Name = "Slow Dance", MasteredAP = 60, Offset = 0xD2},
				new DressphereAbility {Name = "Brakedance", MasteredAP = 120, Offset = 0xD4},
				new DressphereAbility {Name = "Jitterbug", MasteredAP = 120, Offset = 0xD6},
				new DressphereAbility {Name = "Dirty Dancing", MasteredAP = 160, Offset = 0xD8},
				new DressphereAbility {Name = "Battle Cry", MasteredAP = 10, Offset = 0xDA},
				new DressphereAbility {Name = "Cantus Firmus", MasteredAP = 10, Offset = 0xDC},
				new DressphereAbility {Name = "Esoteric Melody", MasteredAP = 10, Offset = 0xDE},
				new DressphereAbility {Name = "Disenchant", MasteredAP = 10, Offset = 0xE0},
				new DressphereAbility {Name = "Perfect Pitch", MasteredAP = 10, Offset = 0xE2},
				new DressphereAbility {Name = "Matador's Song", MasteredAP = 10, Offset = 0xE4},
			}
		};

		public static Dressphere BlackMage = new Dressphere
		{
			ID = 9,
			Name = "Black Mage",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Fire", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Blizzard", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Thunder", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Water", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Fira", MasteredAP = 40, Offset = 0xF2},
				new DressphereAbility {Name = "Blizzara", MasteredAP = 40, Offset = 0xF4},
				new DressphereAbility {Name = "Thundara", MasteredAP = 40, Offset = 0xF6},
				new DressphereAbility {Name = "Watera", MasteredAP = 40, Offset = 0xF8},
				new DressphereAbility {Name = "Firaga", MasteredAP = 100, Offset = 0xFA},
				new DressphereAbility {Name = "Blizzaga", MasteredAP = 100, Offset = 0xFC},
				new DressphereAbility {Name = "Thundaga", MasteredAP = 100, Offset = 0xFE},
				new DressphereAbility {Name = "Waterga", MasteredAP = 100, Offset = 0x100},
				new DressphereAbility {Name = "Focus", MasteredAP = 10, Offset = 0xE6},
				new DressphereAbility {Name = "MP Absorb", MasteredAP = 10, Offset = 0xE8},
				new DressphereAbility {Name = "Black Magic Lv 2", MasteredAP = 40, Offset = 0x480},
				new DressphereAbility {Name = "Black Magic Lv 3", MasteredAP = 60, Offset = 0x482},
			}
		};

		public static Dressphere WhiteMage = new Dressphere
		{
			ID = 10,
			Name = "White Mage",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Pray", ReadOnly = true, Offset = 0x102},
				new DressphereAbility {Name = "Vigor", MasteredAP = 20, Offset = 0x104},
				new DressphereAbility {Name = "Cure", MasteredAP = 20, Offset = 0x106},
				new DressphereAbility {Name = "Cura", MasteredAP = 40, Offset = 0x108},
				new DressphereAbility {Name = "Curaga", MasteredAP = 80, Offset = 0x10a},
				new DressphereAbility {Name = "Regen", MasteredAP = 80, Offset = 0x10c},
				new DressphereAbility {Name = "Esuna", MasteredAP = 30, Offset = 0x10e},
				new DressphereAbility {Name = "Dispel", MasteredAP = 80, Offset = 0x110},
				new DressphereAbility {Name = "Life", MasteredAP = 30, Offset = 0x112},
				new DressphereAbility {Name = "Full-Life", MasteredAP = 160, Offset = 0x114},
				new DressphereAbility {Name = "Shell", MasteredAP = 30, Offset = 0x116},
				new DressphereAbility {Name = "Protect", MasteredAP = 30, Offset = 0x118},
				new DressphereAbility {Name = "Reflect", MasteredAP = 30, Offset = 0x11a},
				new DressphereAbility {Name = "Full-Cure", MasteredAP = 80, Offset = 0x11c},
				new DressphereAbility {Name = "White Magic Lv2", MasteredAP = 40, Offset = 0x484},
				new DressphereAbility {Name = "White Magic Lv3", MasteredAP = 60, Offset = 0x486},
			}
		};

		public static Dressphere Thief = new Dressphere
		{
			ID = 11,
			Name = "Thief",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Steal", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Pilfer Gil", MasteredAP = 30, Offset = 0x120},
				new DressphereAbility {Name = "Borrowed Time", MasteredAP = 100, Offset = 0x122},
				new DressphereAbility {Name = "Pilfer HP", MasteredAP = 60, Offset = 0x124},
				new DressphereAbility {Name = "Pilfer MP", MasteredAP = 60, Offset = 0x126},
				new DressphereAbility {Name = "Sticky Fingers", MasteredAP = 120, Offset = 0x128},
				new DressphereAbility {Name = "Master Thief", MasteredAP = 140, Offset = 0x12a},
				new DressphereAbility {Name = "Soul Swipe", MasteredAP = 160, Offset = 0x12c},
				new DressphereAbility {Name = "Steal Will", MasteredAP = 160, Offset = 0x12e},
				new DressphereAbility {Name = "Flee", MasteredAP = 10, Offset = 0x130},
				new DressphereAbility {Name = "Item Hunter", MasteredAP = 60, Offset = 0x464},
				new DressphereAbility {Name = "First Strike", MasteredAP = 40, Offset = 0x440},
				new DressphereAbility {Name = "Initiative", MasteredAP = 60, Offset = 0x442},
				new DressphereAbility {Name = "Slowproof", MasteredAP = 20, Offset = 0x4fe},
				new DressphereAbility {Name = "Stopproof", MasteredAP = 40, Offset = 0x502},
			}
		};

		public static Dressphere Trainer = new Dressphere
		{
			ID = 12,
			Name = "Trainer",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Holy Kogoro", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Kogoro Blaze", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Kogoro Freeze", MasteredAP = 40, Offset = 0x134},
				new DressphereAbility {Name = "Kogoro Shock", MasteredAP = 40, Offset = 0x136},
				new DressphereAbility {Name = "Kogoro Deluge", MasteredAP = 40, Offset = 0x138},
				new DressphereAbility {Name = "Kogoro Strike", MasteredAP = 80, Offset = 0x13a},
				new DressphereAbility {Name = "Doom Kogoro", MasteredAP = 80, Offset = 0x13c},
				new DressphereAbility {Name = "Kogoro Cure", MasteredAP = 30, Offset = 0x13e},
				new DressphereAbility {Name = "Kogoro Remedy", MasteredAP = 40, Offset = 0x140},
				new DressphereAbility {Name = "Pound!", MasteredAP = 100, Offset = 0x144},
				new DressphereAbility {Name = "Half MP Cost", MasteredAP = 200, Offset = 0x478},
				new DressphereAbility {Name = "HP Stroll", MasteredAP = 20, Offset = 0x468},
				new DressphereAbility {Name = "MP Stroll", MasteredAP = 20, Offset = 0x46a},
				new DressphereAbility {Name = "Kogoro Lv2", MasteredAP = 80, Offset = 0x488},
				new DressphereAbility {Name = "Kogoro Lv3", MasteredAP = 100, Offset = 0x48a},
			}
		};

		public static Dressphere LadyLuck = new Dressphere
		{
			ID = 13,
			Name = "Lady Luck",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Bribe", MasteredAP = 40, Offset = 0x180},
				new DressphereAbility {Name = "Two Dice", MasteredAP = 20, Offset = 0x176},
				new DressphereAbility {Name = "Four Dice", MasteredAP = 100, Offset = 0x178},
				new DressphereAbility {Name = "Attack Reels", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Magic Reels", MasteredAP = 70, Offset = 0x170},
				new DressphereAbility {Name = "Item Reels", MasteredAP = 80, Offset = 0x172},
				new DressphereAbility {Name = "Random Reels", MasteredAP = 120, Offset = 0x174},
				new DressphereAbility {Name = "Luck", MasteredAP = 30, Offset = 0x17A},
				new DressphereAbility {Name = "Felicity", MasteredAP = 40, Offset = 0x17C},
				new DressphereAbility {Name = "Tantalize", MasteredAP = 60, Offset = 0x17E},
				new DressphereAbility {Name = "Critical", MasteredAP = 160, Offset = 0x520},
				new DressphereAbility {Name = "Double EXP", MasteredAP = 80, Offset = 0x462},
				new DressphereAbility {Name = "SOS Spellspring", MasteredAP = 30, Offset = 0x532},
				new DressphereAbility {Name = "Gillionaire", MasteredAP = 100, Offset = 0x45E},
				new DressphereAbility {Name = "Double Items", MasteredAP = 100, Offset = 0x460},
			}
		};

		public static Dressphere Mascot = new Dressphere
		{
			ID = 14,
			Name = "Mascot",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Moogle Jolt", MasteredAP = 40, Offset = 0x192},
				new DressphereAbility {Name = "Moogle Cure", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Moogle Regen", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Moogle Wall", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Moogle Life", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Moogle Curaja", MasteredAP = 40, Offset = 0x18A},
				new DressphereAbility {Name = "Moogle Regenja", MasteredAP = 40, Offset = 0x18C},
				new DressphereAbility {Name = "Moogle Wallja", MasteredAP = 40, Offset = 0x18E},
				new DressphereAbility {Name = "Moogle Lifeja", MasteredAP = 40, Offset = 0x190},
				new DressphereAbility {Name = "Moogle Beam", MasteredAP = 80, Offset = 0x194},
				new DressphereAbility {Name = "Ribbon", MasteredAP = 999, Offset = 0x50E},
				new DressphereAbility {Name = "Auto Shell", MasteredAP = 80, Offset = 0x514},
				new DressphereAbility {Name = "Auto Protect", MasteredAP = 80, Offset = 0x516},
				new DressphereAbility {Name = "Swordplay", MasteredAP = 80, Offset = -33},
				new DressphereAbility {Name = "Arcana", MasteredAP = 80, Offset = -29},
			}
		};

		public static Dressphere Psychic = new Dressphere
		{
			ID = 15, //28
			Name = "Psychic",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Psychic", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Psychic Bomb", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Maser Eye", MasteredAP = 30, Offset = 0x370},
				new DressphereAbility {Name = "Telekinesis", MasteredAP = 30, Offset = 0x36E},
				new DressphereAbility {Name = "Brainstorm", MasteredAP = 30, Offset = 0x372},
				new DressphereAbility {Name = "Express", MasteredAP = 40, Offset = 0x376},
				new DressphereAbility {Name = "Teleport", MasteredAP = 30, Offset = 0x37C},
				new DressphereAbility {Name = "Time Trip", MasteredAP = 100, Offset = 0x374},
				new DressphereAbility {Name = "Magic Guard", MasteredAP = 80, Offset = 0x378},
				new DressphereAbility {Name = "Physics Guard", MasteredAP = 80, Offset = 0x37A},
				new DressphereAbility {Name = "Excellence", MasteredAP = 120, Offset = 0x37E},
				new DressphereAbility {Name = "Fire Eater", MasteredAP = 40, Offset = 0x49A},
				new DressphereAbility {Name = "Ice Eater", MasteredAP = 40, Offset = 0x4A2},
				new DressphereAbility {Name = "Lightning Eater", MasteredAP = 40, Offset = 0x4AB},
				new DressphereAbility {Name = "Water Eater", MasteredAP = 40, Offset = 0x4B2},
				new DressphereAbility {Name = "Gravity Eater", MasteredAP = 60, Offset = 0x4BA},
			}
		};

		public static Dressphere Festivalist = new Dressphere
		{
			ID = 16, //29
			Name = "Festivalist",
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Twinkler", MasteredAP = 40, Offset = 0x394},
				new DressphereAbility {Name = "Spinner", MasteredAP = 40, Offset = 0x388},
				new DressphereAbility {Name = "Popper", MasteredAP = 40, Offset = 0x38E},
				new DressphereAbility {Name = "Fountain", MasteredAP = 40, Offset = 0x39A},
				new DressphereAbility {Name = "Fire Sandals", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Ice Sandals", MasteredAP = 30, Offset = 0x3A2},
				new DressphereAbility {Name = "Ltng. Sandals", MasteredAP = 30, Offset = 0x3A4},
				new DressphereAbility {Name = "Water Sandals", MasteredAP = 30, Offset = 0x3A6},
				new DressphereAbility {Name = "Flare Sandals", MasteredAP = 60, Offset = 0x3A8},
				new DressphereAbility {Name = "Ultima Sandals", MasteredAP = 160, Offset = 0x3AA},
				new DressphereAbility {Name = "Piercing Magic", MasteredAP = 30, Offset = 0x466},
				new DressphereAbility {Name = "Silenceproof", MasteredAP = 10, Offset = 0xDC},
				new DressphereAbility {Name = "Pointlessproof", MasteredAP = 10, Offset = 0xDE},
				new DressphereAbility {Name = "SOS Regen", MasteredAP = 30, Offset = 0x52E},
				new DressphereAbility {Name = "SOS Wall", MasteredAP = 30, Offset = 0x576},
			}
		};

		public static Dressphere FloralFallal = new Dressphere
		{
			ID = 17, //15
			Name = "Floral Fallal",
			Special = 0,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Floral Fallal", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Libra", MasteredAP = 4, Offset = 0x1CA},
				new DressphereAbility {Name = "Heat Whirl", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Ice Whirl", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Electric Whirl", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Aqua Whirl", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Barrier", MasteredAP = 20, Offset = 0x1C8},
				new DressphereAbility {Name = "Shield", MasteredAP = 20, Offset = 0x1C6},
				new DressphereAbility {Name = "Flare Whirl", MasteredAP = 24, Offset = 0x1CC},
				new DressphereAbility {Name = "Great Whirl", MasteredAP = 30, Offset = 0x1D0},
				new DressphereAbility {Name = "All-Life", MasteredAP = 8, Offset = 0x1CE},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x524},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x526},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x456},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x45A},
			}
		};

		public static Dressphere RightPistil = new Dressphere
		{
			ID = 18, //16
			Name = "Right Pistil",
			Special = 0,
			Abilities = new[]
			{
				new DressphereAbility {Name = "White Pollen", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "White Honey", MasteredAP = 4, Offset = 0x15B4},
				new DressphereAbility {Name = "Hard Leaves", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Tough Nuts", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Mirror Petals", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Floral Rush", MasteredAP = 20, Offset = 0x15BC},
				new DressphereAbility {Name = "Floral Bomb", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Fallal Bomb", MasteredAP = 10, Offset = 0x15C0},
				new DressphereAbility {Name = "Floral Magisol", MasteredAP = 10, Offset = 0x15C2},
				new DressphereAbility {Name = "Fallal Magisol", MasteredAP = 10, Offset = 0x15C4},
				new DressphereAbility {Name = "Right Stigma", MasteredAP = 20, Offset = 0x15C6},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x1904},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x1906},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x1836},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x183A},
			}
		};

		public static Dressphere LeftPistil = new Dressphere
		{
			ID = 19, //17
			Name = "Left Pistil",
			Special = 0,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Dream Pollen", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Mad Seeds", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Sticky Honey", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Halfdeath Petals", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Poison Leaves", MasteredAP = 10, Offset = 0x1C70},
				new DressphereAbility {Name = "Death Petals", MasteredAP = 10, Offset = 0x1C72},
				new DressphereAbility {Name = "Silent White", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Congealed Honey", MasteredAP = 20, Offset = 0x1C78},
				new DressphereAbility {Name = "Panic Floralysis", MasteredAP = 10, Offset = 0x1C76},
				new DressphereAbility {Name = "Ash Floralysis", MasteredAP = 10, Offset = 0x1C74},
				new DressphereAbility {Name = "Left Stigma", MasteredAP = 20, Offset = 0x1C7C},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x1FA4},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x1FA6},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x1ED6},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x1EDA},
			}
		};

		public static Dressphere MachinaMaw = new Dressphere
		{
			ID = 20, //18
			Name = "Machina Maw",
			Special = 1,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Revival", MasteredAP = 10, Offset = 0x8B0},
				new DressphereAbility {Name = "Death Missile", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Bio Missile", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Break Missile", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Berserk Missile", MasteredAP = 10, Offset = 0x8A6},
				new DressphereAbility {Name = "Stop Missile", MasteredAP = 10, Offset = 0x8A8},
				new DressphereAbility {Name = "Confuse Missile", MasteredAP = 10, Offset = 0x8AA},
				new DressphereAbility {Name = "Shockwave", MasteredAP = 20, Offset = 0x8AC},
				new DressphereAbility {Name = "Shockstorm", MasteredAP = 20, Offset = 0x8AE},
				new DressphereAbility {Name = "Vajra", MasteredAP = 30, Offset = 0x8B2},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0xBC4},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0xBC6},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0xAF6},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0xAFA},
			}
		};

		public static Dressphere SmasherR = new Dressphere
		{
			ID = 21, //18
			Name = "Smasher-R",
			Special = 1,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Howitzer", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Sleep Shell", MasteredAP = 10, Offset = 0x233C},
				new DressphereAbility {Name = "Slow Shell", MasteredAP = 10, Offset = 0x233E},
				new DressphereAbility {Name = "Anti-Power Shell", MasteredAP = 10, Offset = 0x2340},
				new DressphereAbility {Name = "Anti-Armor Shell", MasteredAP = 10, Offset = 0x2342},
				new DressphereAbility {Name = "Scan", MasteredAP = 10, Offset = 0x2344},
				new DressphereAbility {Name = "Shellter", MasteredAP = 20, Offset = 0x2346},
				new DressphereAbility {Name = "Protector", MasteredAP = 20, Offset = 0x2348},
				new DressphereAbility {Name = "HP Repair", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "MP Repair", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Homing Ray", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x2644},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x2646},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x2576},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x257A},
			}
		};

		public static Dressphere CrusherL = new Dressphere
		{
			ID = 22, //18
			Name = "Crusher-L",
			Special = 1,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Howitzer", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Blind Shell", MasteredAP = 10, Offset = 0x29F2},
				new DressphereAbility {Name = "Silence Shell", MasteredAP = 10, Offset = 0x29F4},
				new DressphereAbility {Name = "Anti-Magic Shell", MasteredAP = 10, Offset = 0x29F6},
				new DressphereAbility {Name = "Anti-Mental Shell", MasteredAP = 10, Offset = 0x29F8},
				new DressphereAbility {Name = "Booster", MasteredAP = 20, Offset = 0x29FA},
				new DressphereAbility {Name = "Offense", MasteredAP = 20, Offset = 0x29FC},
				new DressphereAbility {Name = "Defense", MasteredAP = 20, Offset = 0x29FE},
				new DressphereAbility {Name = "HP Repair", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "MP Repair", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Homing Ray", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x2CE4},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x2CE6},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x2C16},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x2C1A},
			}
		};

		public static Dressphere FullThrottle = new Dressphere
		{
			ID = 23, //21
			Name = "Full Throttle",
			Special = 2,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Attack", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Fright", MasteredAP = 20, Offset = 0xF90},
				new DressphereAbility {Name = "Aestus", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Winterkill", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Whelmen", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Levin", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Wisenen", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Fiers", MasteredAP = 20, Offset = 0xF8A},
				new DressphereAbility {Name = "Deeth", MasteredAP = 20, Offset = 0xF8C},
				new DressphereAbility {Name = "Assoil", MasteredAP = 20, Offset = 0xF8E},
				new DressphereAbility {Name = "Sword Dance", MasteredAP = 30, Offset = 0xF92},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x1264},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x1266},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x1196},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x119A},
			}
		};

		public static Dressphere DextralWing = new Dressphere
		{
			ID = 24, //21
			Name = "Dextral Wing",
			Special = 2,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Venom Wing", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Blind Wing", ReadOnly = true, Offset = 0xF90},
				new DressphereAbility {Name = "Mute Wing", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Rock Wing", MasteredAP = 10, Offset = 0x30BA},
				new DressphereAbility {Name = "Lazy Wing", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Violent Wing", MasteredAP = 10, Offset = 0x30BE},
				new DressphereAbility {Name = "Still Wing", MasteredAP = 10, Offset = 0x30C0},
				new DressphereAbility {Name = "Crazy Wing", MasteredAP = 10, Offset = 0x30C2},
				new DressphereAbility {Name = "Stamina", ReadOnly = true, Offset = 0xF8C},
				new DressphereAbility {Name = "Mettle", ReadOnly = true, Offset = 0xF8E},
				new DressphereAbility {Name = "Reboot", MasteredAP = 10, Offset = 0x30C8},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x3384},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x3386},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x32B6},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x32BA},
			}
		};

		public static Dressphere SinistralWing = new Dressphere
		{
			ID = 25, //21
			Name = "Sinistral Wing",
			Special = 2,
			Abilities = new[]
			{
				new DressphereAbility {Name = "Steel Feather", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Diamond Feather", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "White Feather", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Buckle Feather", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Cloudy Feather", MasteredAP = 10, Offset = 0x3776},
				new DressphereAbility {Name = "Pointed Feather", MasteredAP = 10, Offset = 0x3778},
				new DressphereAbility {Name = "Pumice Feather", MasteredAP = 10, Offset = 0x376E},
				new DressphereAbility {Name = "Ma'at's Feather", MasteredAP = 10, Offset = 0x3770},
				new DressphereAbility {Name = "Stamina", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Mettle", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Reboot", MasteredAP = 10, Offset = 0x377E},
				new DressphereAbility {Name = "Ribbon", ReadOnly = true, Offset = -1},
				new DressphereAbility {Name = "Double HP", MasteredAP = 20, Offset = 0x3A24},
				new DressphereAbility {Name = "Triple HP", MasteredAP = 30, Offset = 0x3A26},
				new DressphereAbility {Name = "Break HP Limit", MasteredAP = 20, Offset = 0x3956},
				new DressphereAbility {Name = "Break Damage Limit", MasteredAP = 20, Offset = 0x395A},
			}
		};
	}

	public class Dressphere
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public sbyte Special { get; set; } = -1;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public class MonsterArenaData
    {
        public static MonsterArenaArea[] MonsterArenaAreas =
        {
            new MonsterArenaArea {Name = "Besaid", Monsters = Monsters.Besaid},
            new MonsterArenaArea {Name = "Kilika", Monsters = Monsters.Kilika},
            new MonsterArenaArea {Name = "Mi'ihen Highroad", Monsters = Monsters.MiihenHighroad},
            new MonsterArenaArea {Name = "Mushroom Rock Road", Monsters = Monsters.MushroomRock},
            new MonsterArenaArea {Name = "Djose Road", Monsters = Monsters.DjoseRoad},
            new MonsterArenaArea {Name = "Thunder Plains", Monsters = Monsters.ThunderPlains},
            new MonsterArenaArea {Name = "Macalania", Monsters = Monsters.Macalania},
            new MonsterArenaArea {Name = "Bikanel", Monsters = Monsters.Bikanel},
            new MonsterArenaArea {Name = "Calm Lands", Monsters = Monsters.CalmLands},
            new MonsterArenaArea {Name = "Mt. Gagazet", Monsters = Monsters.MtGagazet},
            new MonsterArenaArea {Name = "Stolen Fayth Cavern", Monsters = Monsters.StolenFaythCavern},
            new MonsterArenaArea {Name = "Inside Sin", Monsters = Monsters.InsideSin},
            new MonsterArenaArea {Name = "Omega Dungeon", Monsters = Monsters.OmegaDungeon},
            new MonsterArenaArea {Name = "Area Conquest", Monsters = Monsters.AreaConquest},
            new MonsterArenaArea {Name = "Species Conquest", Monsters = Monsters.SpeciesConquest},
            new MonsterArenaArea {Name = "Original", Monsters = Monsters.Original},
        };

        public static class Monsters
        {
            public static Monster[] Besaid =
            {
                new Monster {Index = 8, Name = "Dingo"},
                new Monster {Index = 27, Name = "Condor"},
                new Monster {Index = 15, Name = "Water Flan"},
            };

            public static Monster[] Kilika =
            {
                new Monster {Index = 21, Name = "Dinonix"},
                new Monster {Index = 30, Name = "Killer Bee"},
                new Monster {Index = 61, Name = "Yellow Element"},
                new Monster {Index = 38, Name = "Ragora"},
            };

            public static Monster[] MiihenHighroad =
            {
                new Monster {Index = 9, Name = "Mi'ihen Fang"},
                new Monster {Index = 22, Name = "Ipiria"},
                new Monster {Index = 34, Name = "Floating Eye"},
                new Monster {Index = 62, Name = "White Element"},
                new Monster {Index = 0, Name = "Raldo"},
                new Monster {Index = 50, Name = "Vouivre"},
                new Monster {Index = 85, Name = "Bomb"},
                new Monster {Index = 47, Name = "Dual Horn"},
            };

            public static Monster[] MushroomRock =
            {
                new Monster {Index = 23, Name = "Raptor"},
                new Monster {Index = 5, Name = "Gandarewa"},
                new Monster {Index = 16, Name = "Thunder Flan"},
                new Monster {Index = 63, Name = "Red Element"},
                new Monster {Index = 51, Name = "Lamashtu"},
                new Monster {Index = 91, Name = "Funguar"},
                new Monster {Index = 40, Name = "Garuda"},
            };

            public static Monster[] DjoseRoad =
            {
                new Monster {Index = 10, Name = "Garm"},
                new Monster {Index = 28, Name = "Simurgh"},
                new Monster {Index = 31, Name = "Bite Bug"},
                new Monster {Index = 17, Name = "Snow Flan"},
                new Monster {Index = 1, Name = "Bunyip"},
                new Monster {Index = 79, Name = "Basilisk"},
                new Monster {Index = 83, Name = "Ochu"},
            };

            public static Monster[] ThunderPlains =
            {
                new Monster {Index = 24, Name = "Melusine"},
                new Monster {Index = 6, Name = "Aerouge"},
                new Monster {Index = 35, Name = "Buer"},
                new Monster {Index = 64, Name = "Gold Element"},
                new Monster {Index = 52, Name = "Kusariqqu"},
                new Monster {Index = 89, Name = "Larva"},
                new Monster {Index = 76, Name = "Iron Giant"},
                new Monster {Index = 87, Name = "Qactuar"},
            };

            public static Monster[] Macalania =
            {
                new Monster {Index = 11, Name = "Snow Wolf"},
                new Monster {Index = 25, Name = "Iguion"},
                new Monster {Index = 32, Name = "Wasp"},
                new Monster {Index = 36, Name = "Evil Eye"},
                new Monster {Index = 18, Name = "Ice Flan"},
                new Monster {Index = 65, Name = "Blue Element"},
                new Monster {Index = 2, Name = "Murussu"},
                new Monster {Index = 3, Name = "Mafdet"},
                new Monster {Index = 94, Name = "Xiphos"},
                new Monster {Index = 71, Name = "Chimera"},
            };

            public static Monster[] Bikanel =
            {
                new Monster {Index = 12, Name = "Sand Wolf"},
                new Monster {Index = 29, Name = "Alcyone"},
                new Monster {Index = 53, Name = "Mushussu"},
                new Monster {Index = 41, Name = "Zu"},
                new Monster {Index = 42, Name = "Sand Worm"},
                new Monster {Index = 88, Name = "Cactuar"},
            };

            public static Monster[] CalmLands =
            {
                new Monster {Index = 13, Name = "Skoll"},
                new Monster {Index = 33, Name = "Nebiros"},
                new Monster {Index = 19, Name = "Flame Flan"},
                new Monster {Index = 4, Name = "Shred"},
                new Monster {Index = 80, Name = "Anacondaur"},
                new Monster {Index = 57, Name = "Ogre"},
                new Monster {Index = 73, Name = "Coeurl"},
                new Monster {Index = 72, Name = "Chimera Brain"},
                new Monster {Index = 55, Name = "Malboro"},
            };

            public static Monster[] MtGagazet =
            {
                new Monster {Index = 14, Name = "Bandersnatch"},
                new Monster {Index = 37, Name = "Ahriman"},
                new Monster {Index = 20, Name = "Dark Flan"},
                new Monster {Index = 86, Name = "Grenade"},
                new Monster {Index = 39, Name = "Grat"},
                new Monster {Index = 49, Name = "Grendel"},
                new Monster {Index = 58, Name = "Bashura"},
                new Monster {Index = 84, Name = "Mandragora"},
                new Monster {Index = 69, Name = "Behemoth"},
                new Monster {Index = 60, Name = "Splasher"},
                new Monster {Index = 45, Name = "Achelous"},
                new Monster {Index = 46, Name = "Maelspike"},
            };

            public static Monster[] StolenFaythCavern =
            {
                new Monster {Index = 26, Name = "Yowie"},
                new Monster {Index = 7, Name = "Imp"},
                new Monster {Index = 66, Name = "Dark Element"},
                new Monster {Index = 54, Name = "Nidhogg"},
                new Monster {Index = 92, Name = "Thorn"},
                new Monster {Index = 48, Name = "Valaha"},
                new Monster {Index = 68, Name = "Epaaj"},
                new Monster {Index = 44, Name = "Ghost"},
                new Monster {Index = 98, Name = "Tonberry"},
            };

            public static Monster[] InsideSin =
            {
                new Monster {Index = 93, Name = "Exoray"},
                new Monster {Index = 97, Name = "Wraith"},
                new Monster {Index = 77, Name = "Gemini"},
                new Monster {Index = 78, Name = "Gemini"},
                new Monster {Index = 75, Name = "Demonolith"},
                new Monster {Index = 56, Name = "Great Malboro"},
                new Monster {Index = 90, Name = "Barbatos"},
                new Monster {Index = 81, Name = "Adamantoise"},
                new Monster {Index = 70, Name = "Behemoth King"},
            };

            public static Monster[] OmegaDungeon =
            {
                new Monster {Index = 100, Name = "Zaurus"},
                new Monster {Index = 102, Name = "Floating Death"},
                new Monster {Index = 67, Name = "Black Element"},
                new Monster {Index = 101, Name = "Halma"},
                new Monster {Index = 95, Name = "Puroboros"},
                new Monster {Index = 96, Name = "Spirit"},
                new Monster {Index = 103, Name = "Machea"},
                new Monster {Index = 74, Name = "Master Coeurl"},
                new Monster {Index = 99, Name = "Master Tonberry"},
                new Monster {Index = 82, Name = "Varuna"},
            };

            public static Monster[] AreaConquest =
            {
                new Monster {Index = 120, Name = "Stratoavis"},
                new Monster {Index = 124, Name = "Malboro Menace"},
                new Monster {Index = 125, Name = "Kottos"},
                new Monster {Index = 129, Name = "Coeurlregina"},
                new Monster {Index = 131, Name = "Jormungand"},
                new Monster {Index = 134, Name = "Cactuar King"},
                new Monster {Index = 138, Name = "Espada"},
                new Monster {Index = 121, Name = "Abyss Worm"},
                new Monster {Index = 128, Name = "Chimerageist"},
                new Monster {Index = 137, Name = "Don Tonberry"},
                new Monster {Index = 127, Name = "Catoblepas"},
                new Monster {Index = 132, Name = "Abaddon"},
                new Monster {Index = 135, Name = "Vorban"},
            };

            public static Monster[] SpeciesConquest =
            {
                new Monster {Index = 114, Name = "Fenrir"},
                new Monster {Index = 116, Name = "Ornitholestes"},
                new Monster {Index = 117, Name = "Pteryx"},
                new Monster {Index = 118, Name = "Hornet"},
                new Monster {Index = 113, Name = "Vidatu"},
                new Monster {Index = 119, Name = "One-Eye"},
                new Monster {Index = 115, Name = "Jumbo Flan"},
                new Monster {Index = 126, Name = "Nega Elemental"},
                new Monster {Index = 112, Name = "Tanket"},
                new Monster {Index = 123, Name = "Fafnir"},
                new Monster {Index = 136, Name = "Sleep Sprout"},
                new Monster {Index = 133, Name = "Bomb King"},
                new Monster {Index = 122, Name = "Juggernaut"},
                new Monster {Index = 130, Name = "Ironclad"},
            };

            public static Monster[] Original =
            {
                new Monster {Index = 107, Name = "Earth Eater"},
                new Monster {Index = 109, Name = "Greater Sphere"},
                new Monster {Index = 105, Name = "Catastrophe"},
                new Monster {Index = 110, Name = "Th'uban"},
                new Monster {Index = 106, Name = "Neslug"},
                new Monster {Index = 108, Name = "Ultima Buster"},
                new Monster {Index = 111, Name = "Shinryu"},
                new Monster {Index = 104, Name = "Nemesis"},
            };
        }
    }

    public class Monster
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class MonsterArenaArea
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public Monster[] Monsters { get; set; }
    }
}
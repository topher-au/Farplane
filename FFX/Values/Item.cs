using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public class Item
    {
        private const int TotalItems = 112;

        public static Item[] Items =
        {
            new Item { ID = 0x2000, Name = "Potion" },
            new Item { ID = 0x2001, Name = "Hi-potion" },
            new Item { ID = 0x2002, Name = "X-potion" },
            new Item { ID = 0x2003, Name = "Mega Potion" },
            new Item { ID = 0x2004, Name = "Ether" },
            new Item { ID = 0x2005, Name = "Turbo Ether" },
            new Item { ID = 0x2006, Name = "Phoenix Down" },
            new Item { ID = 0x2007, Name = "Mega Phoenix" },
            new Item { ID = 0x2008, Name = "Elixir" },
            new Item { ID = 0x2009, Name = "Megalixir" },
            new Item { ID = 0x200A, Name = "Antidote" },
            new Item { ID = 0x200B, Name = "Soft" },
            new Item { ID = 0x200C, Name = "Eye Drops" },
            new Item { ID = 0x200D, Name = "Echo Screen" },
            new Item { ID = 0x200E, Name = "Holy Water" },
            new Item { ID = 0x200F, Name = "Remedy" },
            new Item { ID = 0x2010, Name = "Power Distiller" },
            new Item { ID = 0x2011, Name = "Mana Distiller" },
            new Item { ID = 0x2012, Name = "Speed Distiller" },
            new Item { ID = 0x2013, Name = "Ability Distiller" },
            new Item { ID = 0x2014, Name = "Al Bhed Potion" },
            new Item { ID = 0x2015, Name = "Healing Water" },
            new Item { ID = 0x2016, Name = "Tetra Elemental" },
            new Item { ID = 0x2017, Name = "Antarctic Wind" },
            new Item { ID = 0x2018, Name = "Arctic Wind" },
            new Item { ID = 0x2019, Name = "Ice Gem" },
            new Item { ID = 0x201A, Name = "Bomb Fragment" },
            new Item { ID = 0x201B, Name = "Bomb Core" },
            new Item { ID = 0x201C, Name = "Fire Gem" },
            new Item { ID = 0x201D, Name = "Electro Marble" },
            new Item { ID = 0x201E, Name = "Lightning Marble" },
            new Item { ID = 0x201F, Name = "Lightning Gem" },
            new Item { ID = 0x2020, Name = "Fish Scale" },
            new Item { ID = 0x2021, Name = "Dragon Scale" },
            new Item { ID = 0x2022, Name = "Water Gem" },
            new Item { ID = 0x2023, Name = "Grenade" },
            new Item { ID = 0x2024, Name = "Frag Grenade" },
            new Item { ID = 0x2025, Name = "Sleeping Grenade" },
            new Item { ID = 0x2026, Name = "Dream Powder" },
            new Item { ID = 0x2027, Name = "Silence Grenade" },
            new Item { ID = 0x2028, Name = "Smoke Bomb" },
            new Item { ID = 0x2029, Name = "Shadow Gem" },
            new Item { ID = 0x202A, Name = "Shining Gem" },
            new Item { ID = 0x202B, Name = "Blessed Gem" },
            new Item { ID = 0x202C, Name = "Supreme Gem" },
            new Item { ID = 0x202D, Name = "Poison Gem" },
            new Item { ID = 0x202E, Name = "Silver Hourglass" },
            new Item { ID = 0x202F, Name = "Gold Hourglass" },
            new Item { ID = 0x2030, Name = "Candle of Life" },
            new Item { ID = 0x2031, Name = "Petrify Grenade" },
            new Item { ID = 0x2032, Name = "Farplane Shadow" },
            new Item { ID = 0x2033, Name = "Farplane Wind" },
            new Item { ID = 0x2034, Name = "Designer Wallet" },
            new Item { ID = 0x2035, Name = "Dark Matter" },
            new Item { ID = 0x2036, Name = "Chocobo Feather" },
            new Item { ID = 0x2037, Name = "Chocobo Wing" },
            new Item { ID = 0x2038, Name = "Lunar Curtain" },
            new Item { ID = 0x2039, Name = "Light Curtain" },
            new Item { ID = 0x203A, Name = "Star Curtain" },
            new Item { ID = 0x203B, Name = "Healing Spring" },
            new Item { ID = 0x203C, Name = "Mana Spring" },
            new Item { ID = 0x203D, Name = "Stamia Spring" },
            new Item { ID = 0x203E, Name = "Soul Spring" },
            new Item { ID = 0x203F, Name = "Purifying Salt" },
            new Item { ID = 0x2040, Name = "Stamia Tablet" },
            new Item { ID = 0x2041, Name = "Mana Tablet" },
            new Item { ID = 0x2042, Name = "Twin Stars" },
            new Item { ID = 0x2043, Name = "Stamia Tonic" },
            new Item { ID = 0x2044, Name = "Mana Tonic" },
            new Item { ID = 0x2045, Name = "Three Stars" },
            new Item { ID = 0x2046, Name = "Power Sphere" },
            new Item { ID = 0x2047, Name = "Mana Sphere" },
            new Item { ID = 0x2048, Name = "Speed Sphere" },
            new Item { ID = 0x2049, Name = "Ability Sphere" },
            new Item { ID = 0x204A, Name = "Fortune Sphere" },
            new Item { ID = 0x204B, Name = "Attribute Sphere" },
            new Item { ID = 0x204C, Name = "Special Sphere" },
            new Item { ID = 0x204D, Name = "Skill Sphere" },
            new Item { ID = 0x204E, Name = "Wht Magic Sphere" },
            new Item { ID = 0x204F, Name = "Blk Magic Sphere" },
            new Item { ID = 0x2050, Name = "Master Sphere" },
            new Item { ID = 0x2051, Name = "Lv. 1 Key Sphere" },
            new Item { ID = 0x2052, Name = "Lv. 2 Key Sphere" },
            new Item { ID = 0x2053, Name = "Lv. 3 Key Sphere" },
            new Item { ID = 0x2054, Name = "Lv. 4 Key Sphere" },
            new Item { ID = 0x2055, Name = "Hp Sphere" },
            new Item { ID = 0x2056, Name = "Mp Sphere" },
            new Item { ID = 0x2057, Name = "Strength Sphere" },
            new Item { ID = 0x2058, Name = "Defense Sphere" },
            new Item { ID = 0x2059, Name = "Magic Sphere" },
            new Item { ID = 0x205A, Name = "Magic Def Sphere" },
            new Item { ID = 0x205B, Name = "Agility Sphere" },
            new Item { ID = 0x205C, Name = "Evasion Sphere" },
            new Item { ID = 0x205D, Name = "Accuracy Sphere" },
            new Item { ID = 0x205E, Name = "Luck Sphere" },
            new Item { ID = 0x205F, Name = "Clear Sphere" },
            new Item { ID = 0x2060, Name = "Return Sphere" },
            new Item { ID = 0x2061, Name = "Friend Sphere" },
            new Item { ID = 0x2062, Name = "Teleport Sphere" },
            new Item { ID = 0x2063, Name = "Warp Sphere" },
            new Item { ID = 0x2064, Name = "Map" },
            new Item { ID = 0x2065, Name = "Rename Card" },
            new Item { ID = 0x2066, Name = "Musk" },
            new Item { ID = 0x2067, Name = "Hypello Potion" },
            new Item { ID = 0x2068, Name = "Shining Thorn" },
            new Item { ID = 0x2069, Name = "Pendulum" },
            new Item { ID = 0x206A, Name = "Amulet" },
            new Item { ID = 0x206B, Name = "Door To Tomorrow" },
            new Item { ID = 0x206C, Name = "Wings to Discovery" },
            new Item { ID = 0x206D, Name = "Gambler's Spirit" },
            new Item { ID = 0x206E, Name = "Underdog's Secret" },
            new Item { ID = 0x206F, Name = "Winning Formula" },
        };

        public static Item FromID(int itemID)
        {
            return Items.FirstOrDefault(item => item.ID == itemID);
        }

        public static Item[] ReadItems()
        {
            var itemOffset = Offsets.GetOffset(OffsetType.ItemTypes);
            var countOffset = Offsets.GetOffset(OffsetType.ItemCounts);

            var itemData = MemoryReader.ReadBytes(itemOffset, TotalItems * 2);
            var countData = MemoryReader.ReadBytes(countOffset, TotalItems);

            var itemList = new List<Item>();

            for (int i = 0; i < TotalItems; i++)
            {
                var itemID = BitConverter.ToInt16(itemData, i*2);
                var itemCount = countData[i];
                var item = FromID(itemID);
                itemList.Add(new Item()
                {
                    ID = itemID,
                    Name = item == null ? string.Empty : item.Name,
                    Count = itemCount
                });
            }

            return itemList.ToArray();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public byte Count { get; set; }
    }
}

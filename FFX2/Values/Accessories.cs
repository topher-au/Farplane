using System;
using System.Collections.Generic;
using System.Linq;

namespace Farplane.FFX2.Values
{
    public static class Accessories
    {
        public static Accessory[] AccessoryList = {
            new Accessory { ID = 0x00, Name="Iron Bangle" },
            new Accessory { ID = 0x01, Name="Titanium Bangle" },
            new Accessory { ID = 0x02, Name="Mythril Bangle" },
            new Accessory { ID = 0x03, Name="Crystal Bangle" },
            new Accessory { ID = 0x04, Name="Silver Bracer" },
            new Accessory { ID = 0x05, Name="Gold Bracer" },
            new Accessory { ID = 0x06, Name="Rune Bracer" },
            new Accessory { ID = 0x07, Name="Wristband" },
            new Accessory { ID = 0x08, Name="Power Wrist" },
            new Accessory { ID = 0x09, Name="Hyper Wrist" },
            new Accessory { ID = 0x0A, Name="Power Gloves" },
            new Accessory { ID = 0x0B, Name="Kaiser Knuckles" },
            new Accessory { ID = 0x0C, Name="Mythril Gloves" },
            new Accessory { ID = 0x0D, Name="Diamond Gloves" },
            new Accessory { ID = 0x0E, Name="Crystal Gloves" },
            new Accessory { ID = 0x0F, Name="Amulet" },
            new Accessory { ID = 0x10, Name="Tarot Card" },
            new Accessory { ID = 0x11, Name="Talisman" },
            new Accessory { ID = 0x12, Name="Pixie Dust" },
            new Accessory { ID = 0x13, Name="Crystal Ball" },
            new Accessory { ID = 0x14, Name="Defense Veil" },
            new Accessory { ID = 0x15, Name="Mystery Veil" },
            new Accessory { ID = 0x16, Name="Oath Veil" },
            new Accessory { ID = 0x17, Name="Gauntlets" },
            new Accessory { ID = 0x18, Name="Muscle Belt" },
            new Accessory { ID = 0x19, Name="Black Belt" },
            new Accessory { ID = 0x1A, Name="Champion Belt" },
            new Accessory { ID = 0x1B, Name="Tiara" },
            new Accessory { ID = 0x1C, Name="Circlet" },
            new Accessory { ID = 0x1D, Name="Hypno Crown" },
            new Accessory { ID = 0x1E, Name="Regal Crown" },
            new Accessory { ID = 0x1F, Name="Rabite's Foot" },
            new Accessory { ID = 0x20, Name="Fiery Gleam" },
            new Accessory { ID = 0x21, Name="Red Ring" },
            new Accessory { ID = 0x22, Name="NulBlaze Ring" },
            new Accessory { ID = 0x23, Name="Crimson Ring" },
            new Accessory { ID = 0x24, Name="Icy Gleam" },
            new Accessory { ID = 0x25, Name="White Ring" },
            new Accessory { ID = 0x26, Name="NulFrost Ring" },
            new Accessory { ID = 0x27, Name="Snow Ring" },
            new Accessory { ID = 0x28, Name="Lightning Gleam" },
            new Accessory { ID = 0x29, Name="Yellow Ring" },
            new Accessory { ID = 0x2A, Name="NulShock Ring" },
            new Accessory { ID = 0x2B, Name="Ochre Ring" },
            new Accessory { ID = 0x2C, Name="Water Ring" },
            new Accessory { ID = 0x2D, Name="Blue Ring" },
            new Accessory { ID = 0x2E, Name="NulTide Ring" },
            new Accessory { ID = 0x2F, Name="Cerulean Ring" },
            new Accessory { ID = 0x30, Name="Bloodlust" },
            new Accessory { ID = 0x31, Name="Wring" },
            new Accessory { ID = 0x32, Name="Black Ring" },
            new Accessory { ID = 0x33, Name="Freezerburn" },
            new Accessory { ID = 0x34, Name="Sublimator" },
            new Accessory { ID = 0x35, Name="Electrocutioner" },
            new Accessory { ID = 0x36, Name="Short Circuit" },
            new Accessory { ID = 0x37, Name="Tetra Gloves" },
            new Accessory { ID = 0x38, Name="Tetra Bracelet" },
            new Accessory { ID = 0x39, Name="Tetra Band" },
            new Accessory { ID = 0x3A, Name="Tetra Guard" },
            new Accessory { ID = 0x3B, Name="Mortal Shock" },
            new Accessory { ID = 0x3C, Name="Stone Shock" },
            new Accessory { ID = 0x3D, Name="Dream Shock" },
            new Accessory { ID = 0x3E, Name="Mute Shock" },
            new Accessory { ID = 0x3F, Name="Blind Shock" },
            new Accessory { ID = 0x40, Name="Venom Shock" },
            new Accessory { ID = 0x41, Name="Chaos Shock" },
            new Accessory { ID = 0x42, Name="Fury Shock" },
            new Accessory { ID = 0x43, Name="Lag Shock" },
            new Accessory { ID = 0x44, Name="System Shock" },
            new Accessory { ID = 0x45, Name="Angel Earrings" },
            new Accessory { ID = 0x46, Name="Gold Anklet" },
            new Accessory { ID = 0x47, Name="Twist Headband" },
            new Accessory { ID = 0x48, Name="White Cape" },
            new Accessory { ID = 0x49, Name="Silver Glasses" },
            new Accessory { ID = 0x4A, Name="Star Pendant" },
            new Accessory { ID = 0x4B, Name="Black Choker" },
            new Accessory { ID = 0x4C, Name="Potpourri" },
            new Accessory { ID = 0x4D, Name="Gris-Gris Bag" },
            new Accessory { ID = 0x4E, Name="Pearl Necklace" },
            new Accessory { ID = 0x4F, Name="Pretty Orb" },
            new Accessory { ID = 0x50, Name="Dragonfly Orb" },
            new Accessory { ID = 0x51, Name="Beaded Brooch" },
            new Accessory { ID = 0x52, Name="Glass Buckle" },
            new Accessory { ID = 0x53, Name="Faerie Earrings" },
            new Accessory { ID = 0x54, Name="Kinesis Badge" },
            new Accessory { ID = 0x55, Name="Safety Bit" },
            new Accessory { ID = 0x56, Name="Ribbon" },
            new Accessory { ID = 0x57, Name="Wall Ring" },
            new Accessory { ID = 0x58, Name="Favorite Outfit" },
            new Accessory { ID = 0x59, Name="Lure Bracer" },
            new Accessory { ID = 0x5A, Name="Regen Bangle" },
            new Accessory { ID = 0x5B, Name="Haste Bangle" },
            new Accessory { ID = 0x5C, Name="Moon Bracer" },
            new Accessory { ID = 0x5D, Name="Shining Bracer" },
            new Accessory { ID = 0x5E, Name="Star Bracer" },
            new Accessory { ID = 0x5F, Name="Defense Bracer" },
            new Accessory { ID = 0x60, Name="Recovery Bracer" },
            new Accessory { ID = 0x61, Name="Speed Bracer" },
            new Accessory { ID = 0x62, Name="Sword Lore" },
            new Accessory { ID = 0x63, Name="Bushido Lore" },
            new Accessory { ID = 0x64, Name="Arcane Lore" },
            new Accessory { ID = 0x65, Name="Nature's Lore" },
            new Accessory { ID = 0x66, Name="Black Lore" },
            new Accessory { ID = 0x67, Name="White Lore" },
            new Accessory { ID = 0x68, Name="Sword Tome" },
            new Accessory { ID = 0x69, Name="Bushido Tome" },
            new Accessory { ID = 0x6A, Name="Nature's Tome" },
            new Accessory { ID = 0x6B, Name="Arcane Tome" },
            new Accessory { ID = 0x6C, Name="White Tome" },
            new Accessory { ID = 0x6D, Name="Black Tome" },
            new Accessory { ID = 0x6E, Name="Sprint Shoes" },
            new Accessory { ID = 0x6F, Name="AP Egg" },
            new Accessory { ID = 0x70, Name="Cat's Bell" },
            new Accessory { ID = 0x71, Name="Wizard Bracelet" },
            new Accessory { ID = 0x72, Name="Charm Bangle" },
            new Accessory { ID = 0x73, Name="Gold Hairpin" },
            new Accessory { ID = 0x74, Name="Soul Of Thamasa" },
            new Accessory { ID = 0x75, Name="Heady Perfume" },
            new Accessory { ID = 0x76, Name="Shmooth Shailing" },
            new Accessory { ID = 0x77, Name="Key To Success" },
            new Accessory { ID = 0x78, Name="Minerva's Plate" },
            new Accessory { ID = 0x79, Name="Adamantite" },
            new Accessory { ID = 0x7A, Name="Force Of Nature" },
            new Accessory { ID = 0x7B, Name="Cat Nip" },
            new Accessory { ID = 0x7C, Name="Iron Duke" },
            new Accessory { ID = 0x7D, Name="Ragnarok" },
            new Accessory { ID = 0x7E, Name="Enterprise" },
            new Accessory { ID = 0x7F, Name="Invincible" },
        };

        public static List<string> GetNames()
        {
            return AccessoryList.Select(accessory => accessory.Name).ToList();
        } 
        
        public static Accessory FromName(string accessoryName)
        {
            return AccessoryList.First(accessory => string.Equals(accessory.Name, accessoryName, StringComparison.CurrentCultureIgnoreCase));
        }

        public static Accessory FromBytes(byte[] accessoryBytes)
        {
            return AccessoryList.First(accessory => accessory.ID == (int)accessoryBytes[1]);
        }

        public static Accessory FromID(int accessoryID)
        {
            return AccessoryList.First(accessory => accessory.ID == accessoryID);
        }

        public static int IndexOf(Accessory accessory)
        {
            return Array.IndexOf(AccessoryList, accessory);
        }
    }

    public class Accessory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public byte[] GetBytes()
        {
            return new byte[]
            {
                (byte)0x90,
                (byte)ID
            };
        }
    }
}

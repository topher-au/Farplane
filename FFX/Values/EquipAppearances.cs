using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farplane.FFX
{
    public static class EquipAppearance
    {
        public static EquipAppearanceItem[] EquipEquipAppearancesItem =
        {
            new EquipAppearanceItem { ID=0x00FF, Name="Empty" },
            new EquipAppearanceItem { ID=0x4001, Name="Brotherhood" },
            new EquipAppearanceItem { ID=0x4002, Name="Longsword Red" },
            new EquipAppearanceItem { ID=0x4003, Name="Longsword Runed" },
            new EquipAppearanceItem { ID=0x4004, Name="Longsword Black" },
            new EquipAppearanceItem { ID=0x4005, Name="Longsword Blue" },
            new EquipAppearanceItem { ID=0x4006, Name="Longsword Yellow" },
            new EquipAppearanceItem { ID=0x4007, Name="Caladbolg" },
            new EquipAppearanceItem { ID=0x400b, Name="Staff Standard" },
            new EquipAppearanceItem { ID=0x400c, Name="Staff Ball" },
            new EquipAppearanceItem { ID=0x400d, Name="Staff Orange" },
            new EquipAppearanceItem { ID=0x400e, Name="Staff Pink" },
            new EquipAppearanceItem { ID=0x400f, Name="Staff Green" },
            new EquipAppearanceItem { ID=0x4010, Name="Nirvana" },
            new EquipAppearanceItem { ID=0x4015, Name="Katana Standard" },
            new EquipAppearanceItem { ID=0x4016, Name="Katana Curved" },
            new EquipAppearanceItem { ID=0x4017, Name="Katana Hooked" },
            new EquipAppearanceItem { ID=0x4018, Name="Katana Symmetric" },
            new EquipAppearanceItem { ID=0x4019, Name="Masamune" },
            new EquipAppearanceItem { ID=0x401F, Name="Spear Standard" },
            new EquipAppearanceItem { ID=0x4020, Name="Spear Dual Crescent" },
            new EquipAppearanceItem { ID=0x4021, Name="Spear Halberd" },
            new EquipAppearanceItem { ID=0x4022, Name="Spear Green" },
            new EquipAppearanceItem { ID=0x4023, Name="Spear Red" },
            new EquipAppearanceItem { ID=0x4024, Name="Spirit Lance" },
            new EquipAppearanceItem { ID=0x4029, Name="Ball Standard" },
            new EquipAppearanceItem { ID=0x402a, Name="Ball Wrapped" },
            new EquipAppearanceItem { ID=0x402b, Name="Ball Twin Spike" },
            new EquipAppearanceItem { ID=0x402c, Name="Ball Banded" },
            new EquipAppearanceItem { ID=0x402d, Name="Ball Spiked" },
            new EquipAppearanceItem { ID=0x402e, Name="World Champion" },
            new EquipAppearanceItem { ID=0x4033, Name="Doll Moogle" },
            new EquipAppearanceItem { ID=0x4034, Name="Doll Cait Sith" },
            new EquipAppearanceItem { ID=0x4035, Name="Doll Moomba" },
            new EquipAppearanceItem { ID=0x4036, Name="Doll Cactuar" },
            new EquipAppearanceItem { ID=0x4037, Name="Doll PuPu" },
            new EquipAppearanceItem { ID=0x4038, Name="Onion Knight" },
            new EquipAppearanceItem { ID=0x403D, Name="Claw Standard" },
            new EquipAppearanceItem { ID=0x403E, Name="Claw Spiked" },
            new EquipAppearanceItem { ID=0x403F, Name="Claw Blocky" },
            new EquipAppearanceItem { ID=0x4040, Name="Claw Blue" },
            new EquipAppearanceItem { ID=0x4041, Name="Godhand" },
            new EquipAppearanceItem { ID=0x4042, Name="Shield 1" },
            new EquipAppearanceItem { ID=0x4043, Name="Shield 2" },
            new EquipAppearanceItem { ID=0x4044, Name="Shield 3" },
            new EquipAppearanceItem { ID=0x4045, Name="Shield 4" },
            new EquipAppearanceItem { ID=0x4046, Name="Shield 5" },
            new EquipAppearanceItem { ID=0x4047, Name="Ring 1" },
            new EquipAppearanceItem { ID=0x4048, Name="Ring 2" },
            new EquipAppearanceItem { ID=0x4049, Name="Ring 3" },
            new EquipAppearanceItem { ID=0x404a, Name="Ring 4" },
            new EquipAppearanceItem { ID=0x404b, Name="Ring 5" },
            new EquipAppearanceItem { ID=0x404c, Name="Bracer 1" },
            new EquipAppearanceItem { ID=0x404d, Name="Bracer 2" },
            new EquipAppearanceItem { ID=0x404e, Name="Bracer 3" },
            new EquipAppearanceItem { ID=0x404f, Name="Bracer 4" },
            new EquipAppearanceItem { ID=0x4050, Name="Armlet 1" },
            new EquipAppearanceItem { ID=0x4051, Name="Armlet 2" },
            new EquipAppearanceItem { ID=0x4052, Name="Armlet 3" },
            new EquipAppearanceItem { ID=0x4053, Name="Armlet 4" },
            new EquipAppearanceItem { ID=0x4054, Name="Armlet 5" },
            new EquipAppearanceItem { ID=0x4055, Name="Armguard 1" },
            new EquipAppearanceItem { ID=0x4056, Name="Armguard 2" },
            new EquipAppearanceItem { ID=0x4057, Name="Armguard 3" },
            new EquipAppearanceItem { ID=0x4058, Name="Armguard 4" },
            new EquipAppearanceItem { ID=0x4059, Name="Armguard 5" },
            new EquipAppearanceItem { ID=0x405a, Name="Bangle 1" },
            new EquipAppearanceItem { ID=0x405b, Name="Bangle 2" },
            new EquipAppearanceItem { ID=0x405c, Name="Bangle 3" },
            new EquipAppearanceItem { ID=0x405d, Name="Bangle 4" },
            new EquipAppearanceItem { ID=0x405e, Name="Bangle 5" },
            new EquipAppearanceItem { ID=0x405f, Name="Targe 1" },
            new EquipAppearanceItem { ID=0x4060, Name="Targe 2" },
            new EquipAppearanceItem { ID=0x4061, Name="Targe 3" },
            new EquipAppearanceItem { ID=0x4062, Name="Targe 4" },
            new EquipAppearanceItem { ID=0x4064, Name="Flamethrower" },
            new EquipAppearanceItem { ID=0x4065, Name="Rifle" },
            new EquipAppearanceItem { ID=0x4066, Name="Seymour Staff" },
            new EquipAppearanceItem { ID=0x4067, Name="Seymour Armor" },
            new EquipAppearanceItem { ID=0x5017, Name="Yojimbo Dog" },
            new EquipAppearanceItem { ID=0x1074, Name="Dark Yojimbo Dog" },
            new EquipAppearanceItem { ID=0x301F, Name="Kozuka" },
            new EquipAppearanceItem { ID=0x4008, Name="Buster Sword" },
            new EquipAppearanceItem { ID=0x5067, Name="Samurai Sword" },
            new EquipAppearanceItem { ID=0x5062, Name="Knight Sword" },
            new EquipAppearanceItem { ID=0x5058, Name="Rusty Sword" },
            new EquipAppearanceItem { ID=0x5040, Name="Saw" },
            new EquipAppearanceItem { ID=0x5018, Name="Video Camera" },
            new EquipAppearanceItem { ID=0x501B, Name="Megaphone" },
            new EquipAppearanceItem { ID=0x5011, Name="Beer Bottle" },
            new EquipAppearanceItem { ID=0x50FF, Name="Celestial Mirror" },
            new EquipAppearanceItem { ID=0xFF7C, Name="Jecht Sword" },
        };

        public static EquipAppearanceItem FromID(int appearanceID)
        {
            return EquipEquipAppearancesItem.FirstOrDefault(a => a.ID == appearanceID);
        }
    }

    public class EquipAppearanceItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farplane.FFX
{
    public static class WeaponAppearances
    {
        public static Appearance[] Appearances =
        {
            new Appearance { ID=0x00FF, Name="Empty" },
            new Appearance { ID=0x4001, Name="Brotherhood" },
            new Appearance { ID=0x4002, Name="Longsword Red" },
            new Appearance { ID=0x4003, Name="Longsword Runed" },
            new Appearance { ID=0x4004, Name="Longsword Black" },
            new Appearance { ID=0x4005, Name="Longsword Blue" },
            new Appearance { ID=0x4006, Name="Longsword Yellow" },
            new Appearance { ID=0x4007, Name="Caladbolg" },
            new Appearance { ID=0x400b, Name="Staff Standard" },
            new Appearance { ID=0x400c, Name="Staff Ball" },
            new Appearance { ID=0x400d, Name="Staff Orange" },
            new Appearance { ID=0x400e, Name="Staff Pink" },
            new Appearance { ID=0x400f, Name="Staff Green" },
            new Appearance { ID=0x4010, Name="Nirvana" },
            new Appearance { ID=0x4015, Name="Katana Standard" },
            new Appearance { ID=0x4016, Name="Katana Curved" },
            new Appearance { ID=0x4017, Name="Katana Hooked" },
            new Appearance { ID=0x4018, Name="Katana Symmetric" },
            new Appearance { ID=0x4019, Name="Masamune" },
            new Appearance { ID=0x401F, Name="Spear Standard" },
            new Appearance { ID=0x4020, Name="Spear Dual Crescent" },
            new Appearance { ID=0x4021, Name="Spear Halberd" },
            new Appearance { ID=0x4022, Name="Spear Green" },
            new Appearance { ID=0x4023, Name="Spear Red" },
            new Appearance { ID=0x4024, Name="Spirit Lance" },
            new Appearance { ID=0x4029, Name="Ball Standard" },
            new Appearance { ID=0x402a, Name="Ball Wrapped" },
            new Appearance { ID=0x402b, Name="Ball Twin Spike" },
            new Appearance { ID=0x402c, Name="Ball Banded" },
            new Appearance { ID=0x402d, Name="Ball Spiked" },
            new Appearance { ID=0x402e, Name="World Champion" },
            new Appearance { ID=0x4033, Name="Doll Moogle" },
            new Appearance { ID=0x4034, Name="Doll Cait Sith" },
            new Appearance { ID=0x4035, Name="Doll Moomba" },
            new Appearance { ID=0x4036, Name="Doll Cactuar" },
            new Appearance { ID=0x4037, Name="Doll PuPu" },
            new Appearance { ID=0x4038, Name="Onion Knight" },
            new Appearance { ID=0x403D, Name="Claw Standard" },
            new Appearance { ID=0x403E, Name="Claw Spiked" },
            new Appearance { ID=0x403F, Name="Claw Blocky" },
            new Appearance { ID=0x4040, Name="Claw Blue" },
            new Appearance { ID=0x4041, Name="Godhand" },
            new Appearance { ID=0x4042, Name="Shield 1" },
            new Appearance { ID=0x4043, Name="Shield 2" },
            new Appearance { ID=0x4044, Name="Shield 3" },
            new Appearance { ID=0x4045, Name="Shield 4" },
            new Appearance { ID=0x4046, Name="Shield 5" },
            new Appearance { ID=0x4047, Name="Ring 1" },
            new Appearance { ID=0x4048, Name="Ring 2" },
            new Appearance { ID=0x4049, Name="Ring 3" },
            new Appearance { ID=0x404a, Name="Ring 4" },
            new Appearance { ID=0x404b, Name="Ring 5" },
            new Appearance { ID=0x404c, Name="Bracer 1" },
            new Appearance { ID=0x404d, Name="Bracer 2" },
            new Appearance { ID=0x404e, Name="Bracer 3" },
            new Appearance { ID=0x404f, Name="Bracer 4" },
            new Appearance { ID=0x4050, Name="Armlet 1" },
            new Appearance { ID=0x4051, Name="Armlet 2" },
            new Appearance { ID=0x4052, Name="Armlet 3" },
            new Appearance { ID=0x4053, Name="Armlet 4" },
            new Appearance { ID=0x4054, Name="Armlet 5" },
            new Appearance { ID=0x4055, Name="Armguard 1" },
            new Appearance { ID=0x4056, Name="Armguard 2" },
            new Appearance { ID=0x4057, Name="Armguard 3" },
            new Appearance { ID=0x4058, Name="Armguard 4" },
            new Appearance { ID=0x4059, Name="Armguard 5" },
            new Appearance { ID=0x405a, Name="Bangle 1" },
            new Appearance { ID=0x405b, Name="Bangle 2" },
            new Appearance { ID=0x405c, Name="Bangle 3" },
            new Appearance { ID=0x405d, Name="Bangle 4" },
            new Appearance { ID=0x405e, Name="Bangle 5" },
            new Appearance { ID=0x405f, Name="Targe 1" },
            new Appearance { ID=0x4060, Name="Targe 2" },
            new Appearance { ID=0x4061, Name="Targe 3" },
            new Appearance { ID=0x4062, Name="Targe 4" },
            new Appearance { ID=0x4064, Name="Flamethrower" },
            new Appearance { ID=0x4065, Name="Rifle" },
            new Appearance { ID=0x4066, Name="Seymour Staff" },
            new Appearance { ID=0x4067, Name="Seymour Armor" },
            new Appearance { ID=0x5017, Name="Yojimbo Dog" },
            new Appearance { ID=0x1074, Name="Dark Yojimbo Dog" },
            new Appearance { ID=0x301F, Name="Kozuka" },
            new Appearance { ID=0x4008, Name="Buster Sword" },
            new Appearance { ID=0x5067, Name="Samurai Sword" },
            new Appearance { ID=0x5062, Name="Knight Sword" },
            new Appearance { ID=0x5058, Name="Rusty Sword" },
            new Appearance { ID=0x5040, Name="Saw" },
            new Appearance { ID=0x5018, Name="Video Camera" },
            new Appearance { ID=0x501B, Name="Megaphone" },
            new Appearance { ID=0x5011, Name="Beer Bottle" },
            new Appearance { ID=0x50FF, Name="Celestial Mirror" },
            new Appearance { ID=0xFF7C, Name="Jecht Sword" },
        };

        public static Appearance FromID(int appearanceID)
        {
            return Appearances.FirstOrDefault(a => a.ID == appearanceID);
        }
    }

    public class Appearance
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

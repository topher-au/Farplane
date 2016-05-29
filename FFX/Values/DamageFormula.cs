using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public static class DamageFormula
    {
        public static Formula[] DamageFormulas =
        {
            new Formula { ID=1, Name="Normal" },
            new Formula { ID=2, Name="Ignore Defense" },
            new Formula { ID=17, Name="Celestial High HP" },
            new Formula { ID=18, Name="Celestial High MP" },
            new Formula { ID=19, Name="Celestial Low HP" },
            new Formula { ID=5, Name="Target Current HP" },
            new Formula { ID=8, Name="Target Max HP" },
            new Formula { ID=12, Name="Target Current MP" },
            new Formula { ID=10, Name="Target Max MP" },
            new Formula { ID=3, Name="Magic Formula" },
            new Formula { ID=4, Name="Ignore Magic Defense" },
            new Formula { ID=15, Name="Special Magic" },
            new Formula { ID=7, Name="Healing Formula" },
            new Formula { ID=16, Name="User Max HP" },
            new Formula { ID=6, Name="Multiples of 50" },
            new Formula { ID=23, Name="Multiples of 9999" },
            new Formula { ID=22, Name="Target Enemies Killed" },
            new Formula { ID=11, Name="Target Tick Speed" },
            new Formula { ID=13, Name="Target Tick Counter" },
            new Formula { ID=14, Name="Ignore Defense (NR)" },
            new Formula { ID=20, Name="Special Magic (NR)" },
            new Formula { ID=9, Name="Multiples of 50 (R)" },
            new Formula { ID=0, Name="No Damage" },
        };

        public static Formula FromID(int formulaId)
        {
            return DamageFormulas.FirstOrDefault(f => f.ID == formulaId);
        }
    }

    public class Formula
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

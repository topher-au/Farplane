using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public class DamageFormula
    {
        public static DamageFormula[] DamageFormulas =
        {
            new DamageFormula { ID=1, Name="Normal" },
            new DamageFormula { ID=2, Name="Ignore Defense" },
            new DamageFormula { ID=17, Name="Celestial High HP" },
            new DamageFormula { ID=18, Name="Celestial High MP" },
            new DamageFormula { ID=19, Name="Celestial Low HP" },
            new DamageFormula { ID=5, Name="Target Current HP" },
            new DamageFormula { ID=8, Name="Target Max HP" },
            new DamageFormula { ID=12, Name="Target Current MP" },
            new DamageFormula { ID=10, Name="Target Max MP" },
            new DamageFormula { ID=3, Name="Magic DamageFormula" },
            new DamageFormula { ID=4, Name="Ignore Magic Defense" },
            new DamageFormula { ID=15, Name="Special Magic" },
            new DamageFormula { ID=7, Name="Healing DamageFormula" },
            new DamageFormula { ID=16, Name="User Max HP" },
            new DamageFormula { ID=6, Name="Multiples of 50" },
            new DamageFormula { ID=23, Name="Multiples of 9999" },
            new DamageFormula { ID=22, Name="Target Enemies Killed" },
            new DamageFormula { ID=11, Name="Target Tick Speed" },
            new DamageFormula { ID=13, Name="Target Tick Counter" },
            new DamageFormula { ID=14, Name="Ignore Defense (NR)" },
            new DamageFormula { ID=20, Name="Special Magic (NR)" },
            new DamageFormula { ID=9, Name="Multiples of 50 (R)" },
            new DamageFormula { ID=0, Name="No Damage" },
        };

        public static DamageFormula FromID(int DamageFormulaId)
        {
            return DamageFormulas.FirstOrDefault(f => f.ID == DamageFormulaId);
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}

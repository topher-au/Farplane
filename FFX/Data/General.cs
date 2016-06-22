using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public class General
    {
        private static int _offsetCurrentGil = OffsetScanner.GetOffset(GameOffset.FFX_CurrentGil);
        private static int _offsetTidusOverdrive = OffsetScanner.GetOffset(GameOffset.FFX_TidusOverdrive);

        public static int CurrentGil
        {
            get
            {
                var currentGil = GameMemory.Read<int>(_offsetCurrentGil, false);
                return currentGil;
            }
            set
            {
                GameMemory.Write(_offsetCurrentGil, value, false);
            }
        }

        public static int TidusOverdrive
        {
            get
            {
                return GameMemory.Read<int>(_offsetTidusOverdrive, false);
            }
            set
            {
                GameMemory.Write(_offsetTidusOverdrive, value);
            }
        }
    }
}

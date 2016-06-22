using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public class MonsterArena
    {
        private static int _offsetMonstersCaptured = OffsetScanner.GetOffset(GameOffset.FFX_MonstersCaptured);

        public static byte[] GetCaptureCounts()
        {
            return GameMemory.Read<byte>(_offsetMonstersCaptured, 139, false);
        }

        public static void SetCaptureCount(int index, byte count)
        {
            GameMemory.Write(_offsetMonstersCaptured + index, count, false);
        }
    }
}

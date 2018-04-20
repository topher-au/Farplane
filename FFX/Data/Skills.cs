using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Common;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public static class Skills
    {
        private static int _offsetSkillTable = 0;
        private static KernelTable _skillTable;

        private static void UpdateOffset()
        {
            var skillTablePointer = OffsetScanner.GetOffset(GameOffset.FFX_SkillTablePointer);
            _offsetSkillTable = GameMemory.Read<int>(skillTablePointer, false);
        }

        private static void LoadTable()
        {
            _skillTable = new KernelTable(_offsetSkillTable);
            //_skillTable = new KernelTable("D:\\Games\\Steam\\SteamApps\\common\\FINAL FANTASY FFX&FFX-2 HD Remaster\\data\\ffx_data_VBF\\ffx_ps2\\ffx\\master\\new_uspc\\battle\\kernel\\sphere.bin");
        }

        public static List<string> GetSkillNames()
        {
            if (_offsetSkillTable == 0) UpdateOffset();

            LoadTable();

            var names = new List<string>();
            for (int i = 0; i < _skillTable.BlockCount; i++)
            {
                var skillName = _skillTable.GetString1(i);
                names.Add(skillName);
            }

            return names;
        }

        public static string GetSkillName(int skillIndex)
        {
            return _skillTable.GetString1(skillIndex);
        }
        public static string GetSkillDescription(int skillIndex)
        {
            return _skillTable.GetString2(skillIndex);
        }
    }

    public struct SkillData
    {
        public ushort unknown_1;
        public ushort unknown_2;
        public ushort unknown_3;
        public ushort Animation1;
        public ushort Animation2;
    }
}
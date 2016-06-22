using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Farplane.FarplaneMod;

namespace Farplane.Memory
{
    public static class OffsetScanner
    {
        private static SigScan _sigScan = new SigScan();

        private static OffsetBytePattern[] _bytePatterns =
        {
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_ItemTypes,
                Pattern = "55 8B EC 8B 45 08 85 C0 74 06 C7 00 70 00 00 00 B8 ?? ?? ?? ?? 5D C3" +
                          "?? ?? ?? ?? ?? ?? ?? ?? ??" +
                          "55 8B EC 8B 45 08 85 C0 74 06 C7 00 70 00 00 00 B8 ?? ?? ?? ?? 5D C3",
                PatternOffset = 49
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_ItemCounts,
                Pattern = "55 8B EC 8B 45 08 85 C0 74 06 C7 00 70 00 00 00 B8 ?? ?? ?? ?? 5D C3" +
                          "?? ?? ?? ?? ?? ?? ?? ?? ??" +
                          "55 8B EC 8B 45 08 85 C0 74 06 C7 00 70 00 00 00 B8 ?? ?? ?? ?? 5D C3",
                PatternOffset = 17
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_PartyStatBase,
                Pattern = "81 C6 ?? ?? ?? ?? 83 7D 0C 00",
                PatternOffset = 2
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_EquipmentBase,
                Pattern = "6B F6 16 81 C6 ?? ?? ?? ?? EB 2A 85 F6",
                PatternOffset = 5
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_KeyItems,
                Pattern = "C1 F8 04 BE 01 00 00 00 D3 E6 0F B7 0C 45 ?? ?? ?? ?? 85 F1 5E",
                PatternOffset = 14
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_AlBhed,
                Pattern = "51 a1 ?? ?? ?? ?? 53 8b 1d ?? ?? ?? ?? 56 57 33 ff",
                PatternOffset = 9
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_SphereGrid,
                Pattern = "d9 54 24 14 66 a3 ?? ?? ?? ?? d9 05 ?? ?? ?? ?? 33 c0",
                PatternOffset = 6
            },

            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_CurrentGil,
                Pattern = "c3 c7 05 ?? ?? ?? ?? ff c9 9a 3b 3d ff c9 9a 3b",
                PatternOffset = 3
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_TidusOverdrive,
                Pattern = "50 e8 ?? ?? ?? ?? 83 c4 04 ff 05 ?? ?? ?? ?? BF ?? ?? ?? ?? BE ?? ?? ?? ?? 0F B7 1E",
                PatternOffset = 11
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_PartyList,
                Pattern = "8b 45 08 85 c0 74 06 c7 00 03 00 00 00 b8 ?? ?? ?? ?? 5d c3",
                PatternOffset = 14
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_MonstersCaptured,
                Pattern = "8b 45 08 25 ff 0f 00 00 0f b6 80 ?? ?? ?? ?? 5d c3",
                PatternOffset = 11
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_AeonNames,
                Pattern = "8d 04 b6 5e 8d 04 85 ?? ?? ?? ?? 5d c3",
                PatternOffset = 7
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_InBattleFlags,
                Pattern = "74 18 6a 01 c6 81 ?? ?? ?? ?? 01 ff 70 04 51 51",
                PatternOffset = 6
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_GainedAPFlags,
                Pattern = "74 0c 83 fb 08 7d 07 c6 83 ?? ?? ?? ?? 01 0f be 87",
                PatternOffset = 9
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_BattlePointerEnemy,
                Pattern = "0f b6 45 08 69 c0 90 0f 00 00 03 05 ?? ?? ?? ?? 5d c3",
                PatternOffset = 12
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_BattlePointerParty,
                Pattern = "83 f8 1f 7d 0e 69 c0 90 0f 00 00 03 05 ?? ?? ?? ?? 5d c3",
                PatternOffset = 13
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_CurrentRoom,
                Pattern = "0f b7 05 ?? ?? ?? ?? 3b 05 ?? ?? ?? ?? 0f 85 d3 00 00 00",
                PatternOffset = 9
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_DebugFlags,
                Pattern = "83 f8 01 75 09 80 3d ?? ?? ?? ?? 00 75 17 ff 75 08",
                PatternOffset = 7
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_BlitzballData,
                Pattern = "6a 05 b9 78 00 00 00 bf ?? ?? ?? ?? 68 ?? ?? ?? ?? f3 ab",
                PatternOffset = 8
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_BlitzballPointer,
                Pattern = "55 8b ec 8b 45 08 a3 ?? ?? ?? ?? 5d c3",
                PatternOffset = 7
            },
            new OffsetBytePattern()
            {
                Type = GameOffset.FFX_BlitzballMode,
                Pattern = "55 8b ec a1 ?? ?? ?? ?? 3b 45 08 75 0a c7 05 ?? ?? ?? ?? FF FF FF FF",
                PatternOffset = 4
            },
        };

        private static Dictionary<GameOffset, int> _offsets = new Dictionary<GameOffset, int>();

        public static int GetOffset(GameOffset type)
        {
            var foundOffset = _offsets.FirstOrDefault(offset => offset.Key == type).Value;
            if (foundOffset == 0)
            {
                var searchSuccess = FindOffset(type);
                if (!searchSuccess)
                {
                    // Offset search failed! Abort! Abort!
                    MessageBox.Show(
                        $"A critical error occurred while scanning for offsets:\n\nUnable to locate offset:\n{type}\n\nThe application will now exit.",
                        "Critical error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(0);
                }
                foundOffset = _offsets.FirstOrDefault(offset => offset.Key == type).Value;
            }

            return foundOffset;
        }

        public static void Reset()
        {
            _offsets.Clear();
            _sigScan.ResetRegion();
        }

        private static bool FindOffset(GameOffset offsetType)
        {
            // Build a table of offsets from the current process
            _sigScan.Process = GameMemory.Process;
            _sigScan.Address = GameMemory.Process.MainModule.BaseAddress;
            _sigScan.Size = GameMemory.Process.MainModule.ModuleMemorySize;

            var patternData = _bytePatterns.First(pattern => pattern.Type == offsetType);

            var pointer = _sigScan.FindPattern(patternData.Pattern, patternData.PatternOffset);
            if (pointer == IntPtr.Zero) return false;

            var offset = GameMemory.Read<IntPtr>((int) pointer, false);
            _offsets.Add(patternData.Type, (int) offset);

            return true;
        }
    }

    public class OffsetBytePattern
    {
        public GameOffset Type { get; set; }
        public string Pattern { get; set; }
        public int PatternOffset { get; set; }
    }

    public enum GameOffset
    {
        FFX_ItemTypes,
        FFX_ItemCounts,
        FFX_PartyStatBase,
        FFX_EquipmentBase,
        FFX_KeyItems,
        FFX_AlBhed,
        FFX_SphereGrid,
        FFX_CurrentGil,
        FFX_TidusOverdrive,
        FFX_PartyList,
        FFX_MonstersCaptured,
        FFX_AeonNames,
        FFX_InBattleFlags,
        FFX_GainedAPFlags,
        FFX_BattlePointerEnemy,
        FFX_BattlePointerParty,
        FFX_CurrentRoom,
        FFX_DebugFlags,
        FFX_BlitzballData,
        FFX_BlitzballPointer,
        FFX_BlitzballMode,
    }
}


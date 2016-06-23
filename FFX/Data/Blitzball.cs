using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public static class Blitzball
    {
        public const int TotalPlayers = 60;
        private static int _dataPointer = OffsetScanner.GetOffset(GameOffset.FFX_BlitzballData);
        private static int _modePointer = OffsetScanner.GetOffset(GameOffset.FFX_BlitzballMode);
        private static int _gamePointer = OffsetScanner.GetOffset(GameOffset.FFX_BlitzballPointer);

        private static int _blitzDataSize = Marshal.SizeOf<BlitzballData>();

        public static BlitzballData ReadBlitzballData(bool dumpBytes = false)
        {
            var offsetOfPrizes = (int) Marshal.OffsetOf<BlitzballData>("BlitzballPrizes");
            var blitzBytes = GameMemory.Read<byte>(_dataPointer, _blitzDataSize, false);

            if (dumpBytes) File.WriteAllBytes($"blitzdata_{DateTime.Now.Millisecond.ToString()}.bin", blitzBytes);

            var blitzPtr = Marshal.AllocHGlobal(blitzBytes.Length);
            try
            {
                Marshal.Copy(blitzBytes, 0, blitzPtr, blitzBytes.Length);
                return (BlitzballData) Marshal.PtrToStructure(blitzPtr, typeof (BlitzballData));
            }
            finally
            {
                Marshal.FreeHGlobal(blitzPtr);
            }
        }

        public static void WriteBlitzballData(BlitzballData data)
        {
            var dataPtr = Marshal.AllocHGlobal(_blitzDataSize);
            try
            {
                Marshal.StructureToPtr(data, dataPtr, false);
                var dataBytes = new byte[_blitzDataSize];
                Marshal.Copy(dataPtr, dataBytes, 0, _blitzDataSize);
                GameMemory.Write(_dataPointer, dataBytes, false);
            }
            finally
            {
                Marshal.FreeHGlobal(dataPtr);
            }
        }

        public static BlitzballPlayer[] GetPlayers()
        {
            var blitzData = ReadBlitzballData();
            var players = new BlitzballPlayer[TotalPlayers];

            for (int i = 0; i < TotalPlayers; i++)
            {
                players[i] = GetPlayerInfo(i, blitzData);
            }

            return players;
        }

        public static BlitzballPlayer GetPlayerInfo(int playerIndex, BlitzballData blitzData)
        {
            var techs = new byte[5];

            for (int i = 0; i < 5; i++)
                techs[i] = blitzData.TechsEquipped[playerIndex*5 + i];

            var player = new BlitzballPlayer()
            {
                Level = blitzData.PlayerLevels[playerIndex],
                Contract = blitzData.PlayerContracts[playerIndex],
                Experience = blitzData.PlayerExperience[playerIndex],
                LeagueGoals = 0, // TODO Implement This
                Salary = blitzData.PlayerSalary[playerIndex],
                SkillFlags1 = blitzData.AbilityFlags1[playerIndex],
                SkillFlags2 = blitzData.AbilityFlags2[playerIndex],
                TechCapacity = blitzData.TechsAvailable[playerIndex],
                Techs = techs,
                TournamentGoals = 0, // TODO Implement This
            };

            return player;
        }

        public static byte GetTeamSize(int teamIndex)
        {
            var sizes = GetTeamSizes();
            return sizes[teamIndex];
        }

        public static void SetTeamSize(int teamIndex, byte teamSize)
        {
            var bdPointer1 = OffsetScanner.GetOffset(GameOffset.FFX_BlitzballTeamData);
            var bdPointer2 = GameMemory.Read<int>(bdPointer1, false);
            var bdPointer = GameMemory.Read<int>(bdPointer2 + 0x2C, false);
            GameMemory.Write(bdPointer + 0xA88 + teamIndex, teamSize, false);
        }

        public static byte[] GetTeamSizes()
        {
            var bdPointer1 = OffsetScanner.GetOffset(GameOffset.FFX_BlitzballTeamData);
            var bdPointer2 = GameMemory.Read<int>(bdPointer1, false);
            var bdPointer = GameMemory.Read<int>(bdPointer2 + 0x2C, false);

            return GameMemory.Read<byte>(bdPointer + 0xA88, 6, false);
        }

        public static BlitzballPlayer GetPlayerInfo(int playerIndex)
        {
            var blitzData = ReadBlitzballData();

            return GetPlayerInfo(playerIndex, blitzData);
        }

        public static void SetPlayerInfo(int playerIndex, BlitzballPlayer playerInfo)
        {
            var blitzData = ReadBlitzballData();

            blitzData.PlayerLevels[playerIndex] = playerInfo.Level;
            blitzData.PlayerContracts[playerIndex] = playerInfo.Contract;
            blitzData.PlayerExperience[playerIndex] = playerInfo.Experience;
            blitzData.PlayerSalary[playerIndex] = playerInfo.Salary;
            blitzData.AbilityFlags1[playerIndex] = playerInfo.SkillFlags1;
            blitzData.AbilityFlags2[playerIndex] = playerInfo.SkillFlags2;
            blitzData.TechsAvailable[playerIndex] = playerInfo.TechCapacity;
            Array.Copy(playerInfo.Techs, blitzData.TechsEquipped, 5);

            WriteBlitzballData(blitzData);
        }
    }

    public class BlitzballStat
    {
        public ushort HP { get; set; }
        public ushort SH { get; set; }
        public ushort PA { get; set; }
        public ushort EN { get; set; }
    }

    public class BlitzballPlayer
    {
        public byte Level { get; set; }
        public ushort Experience { get; set; }
        public byte Contract { get; set; }
        public ushort Salary { get; set; }
        public ushort LeagueGoals { get; set; }
        public ushort TournamentGoals { get; set; }
        public byte TechCapacity { get; set; }
        public byte[] Techs { get; set; }
        public int SkillFlags1 { get; set; }
        public int SkillFlags2 { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, Size = 2080)]
    public struct BlitzballData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public int[] AbilityFlags1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public int[] AbilityFlags2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 134)] public byte[] unknown_0;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 300)] public byte[] TechsEquipped;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public byte[] TechsAvailable;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public byte[] PlayerLevels;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] unknown_2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)] public byte[] TeamData;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)] public byte[] unknown_3;

        [MarshalAs(UnmanagedType.U1)] public byte LeagueStatus;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public byte[] LeagueMatchups;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public byte[] TournamentMatchups;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public byte[] TournamentWinners;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] TeamLeagueWins;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] TeamLeagueLosses;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 45)] public byte[] unknown_7;

        public byte TournamentStatus;

        public byte unknown_8;

        public byte GoalCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)] public byte[] GoalScorers;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] unknown_4;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public byte[] PlayerContracts;

        [MarshalAs(UnmanagedType.U2)] public ushort TeamWins;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public ushort[] PlayerExperience;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 192)] public byte[] unknown_5;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 248)] public byte[] unknown_6;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] public ushort[] PlayerSalary;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public ushort[] BlitzballPrizes;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public class Blitzball
    {
        private static int _dataPointer = Offsets.GetOffset(OffsetType.BlitzballDataPointer);
        private static int _gamePointer = Offsets.GetOffset(OffsetType.BlitzballGamePointer);

        public static BlitzballPlayer[] Players =
        {
            new BlitzballPlayer {Index = 0, Name = "Tidus"},
            new BlitzballPlayer {Index = 1, Name = "Wakka"},
            new BlitzballPlayer {Index = 2, Name = "Datto"},
            new BlitzballPlayer {Index = 3, Name = "Letty"},
            new BlitzballPlayer {Index = 4, Name = "Jassu"},
            new BlitzballPlayer {Index = 5, Name = "Botta"},
            new BlitzballPlayer {Index = 6, Name = "Keepa"},
            new BlitzballPlayer {Index = 7, Name = "Bickson"},
            new BlitzballPlayer {Index = 8, Name = "Abus"},
            new BlitzballPlayer {Index = 9, Name = "Graav"},
            new BlitzballPlayer {Index = 10, Name = "Doram"},
            new BlitzballPlayer {Index = 11, Name = "Balgerda"},
            new BlitzballPlayer {Index = 12, Name = "Raudy"},
            new BlitzballPlayer {Index = 13, Name = "Larbeight"},
            new BlitzballPlayer {Index = 14, Name = "Isken"},
            new BlitzballPlayer {Index = 15, Name = "Vuroja"},
            new BlitzballPlayer {Index = 16, Name = "Kulukan"},
            new BlitzballPlayer {Index = 17, Name = "Deim"},
            new BlitzballPlayer {Index = 18, Name = "Nizarut"},
            new BlitzballPlayer {Index = 19, Name = "Eigaar"},
            new BlitzballPlayer {Index = 20, Name = "Blappa"},
            new BlitzballPlayer {Index = 21, Name = "Berrik"},
            new BlitzballPlayer {Index = 22, Name = "Judda"},
            new BlitzballPlayer {Index = 23, Name = "Lakkam"},
            new BlitzballPlayer {Index = 24, Name = "Nimrook"},
            new BlitzballPlayer {Index = 25, Name = "Basik Ronso"},
            new BlitzballPlayer {Index = 26, Name = "Argai Ronso"},
            new BlitzballPlayer {Index = 27, Name = "Gazna Ronso"},
            new BlitzballPlayer {Index = 28, Name = "Nuvy Ronso"},
            new BlitzballPlayer {Index = 29, Name = "Irga Ronso"},
            new BlitzballPlayer {Index = 30, Name = "Zamzi Ronso"},
            new BlitzballPlayer {Index = 31, Name = "Giera Guado"},
            new BlitzballPlayer {Index = 32, Name = "Zazi Guado"},
            new BlitzballPlayer {Index = 33, Name = "Nav Guado"},
            new BlitzballPlayer {Index = 34, Name = "Auda Guado"},
            new BlitzballPlayer {Index = 35, Name = "Pah Guado"},
            new BlitzballPlayer {Index = 36, Name = "Noy Guado"},
            new BlitzballPlayer {Index = 37, Name = "Rin"},
            new BlitzballPlayer {Index = 38, Name = "Tatts"},
            new BlitzballPlayer {Index = 39, Name = "Kyou"},
            new BlitzballPlayer {Index = 40, Name = "Shuu"},
            new BlitzballPlayer {Index = 41, Name = "Nedus"},
            new BlitzballPlayer {Index = 42, Name = "Biggs"},
            new BlitzballPlayer {Index = 43, Name = "Wedge"},
            new BlitzballPlayer {Index = 44, Name = "Ropp"},
            new BlitzballPlayer {Index = 45, Name = "Linna"},
            new BlitzballPlayer {Index = 46, Name = "Mep"},
            new BlitzballPlayer {Index = 47, Name = "Zalitz"},
            new BlitzballPlayer {Index = 48, Name = "Naida"},
            new BlitzballPlayer {Index = 49, Name = "Durren"},
            new BlitzballPlayer {Index = 50, Name = "Jumal"},
            new BlitzballPlayer {Index = 51, Name = "Svanda"},
            new BlitzballPlayer {Index = 52, Name = "Vilucha"},
            new BlitzballPlayer {Index = 53, Name = "Shaami"},
            new BlitzballPlayer {Index = 54, Name = "Zev Ronso"},
            new BlitzballPlayer {Index = 55, Name = "Yuma Guado"},
            new BlitzballPlayer {Index = 56, Name = "Kiyuri"},
            new BlitzballPlayer {Index = 57, Name = "Brother"},
            new BlitzballPlayer {Index = 58, Name = "Mifurey"},
            new BlitzballPlayer {Index = 59, Name = "Miyu"},
        };

        public static BlitzballTeam[] Teams =
        {
            new BlitzballTeam {Index = 0, Name = "Luca Goers"},
            new BlitzballTeam {Index = 1, Name = "Kilika Beasts"},
            new BlitzballTeam {Index = 2, Name = "Al Bhed Psyches"},
            new BlitzballTeam {Index = 3, Name = "Ronso Fangs"},
            new BlitzballTeam {Index = 4, Name = "Guado Glories"},
            new BlitzballTeam {Index = 5, Name = "Besaid Aurochs"},
        };

        public static BlitzballTech[] Techs =
        {
            new BlitzballTech {Index = 1, Name = "Jecht Shot"},
            new BlitzballTech {Index = 2, Name = "Jecht Shot 2"},
            new BlitzballTech {Index = 3, Name = "Sphere Shot"},
            new BlitzballTech {Index = 4, Name = "Invisible Shot"},
            new BlitzballTech {Index = 5, Name = "Venom Shot"},
            new BlitzballTech {Index = 6, Name = "Venom Shot 2"},
            new BlitzballTech {Index = 7, Name = "Venom Shot 3"},
            new BlitzballTech {Index = 8, Name = "Nap Shot"},
            new BlitzballTech {Index = 9, Name = "Nap Shot 2"},
            new BlitzballTech {Index = 10, Name = "Nap Shot 3"},
            new BlitzballTech {Index = 11, Name = "Wither Shot"},
            new BlitzballTech {Index = 12, Name = "Wither Shot 2"},
            new BlitzballTech {Index = 13, Name = "Wither Shot 3"},
            new BlitzballTech {Index = 14, Name = "Venom Pass"},
            new BlitzballTech {Index = 15, Name = "Venom Pass 2"},
            new BlitzballTech {Index = 16, Name = "Venom Pass 3"},
            new BlitzballTech {Index = 17, Name = "Nap Pass"},
            new BlitzballTech {Index = 18, Name = "Nap Pass 2"},
            new BlitzballTech {Index = 19, Name = "Nap Pass 3"},
            new BlitzballTech {Index = 20, Name = "Wither Pass"},
            new BlitzballTech {Index = 21, Name = "Wither Pass 2"},
            new BlitzballTech {Index = 22, Name = "Wither Pass 3"},
            new BlitzballTech {Index = 23, Name = "Volley Shot"},
            new BlitzballTech {Index = 24, Name = "Volley Shot 2"},
            new BlitzballTech {Index = 25, Name = "Volley Shot 3"},
            new BlitzballTech {Index = 26, Name = "Venom Tackle"},
            new BlitzballTech {Index = 27, Name = "Venom Tackle 2"},
            new BlitzballTech {Index = 28, Name = "Venom Tackle 3"},
            new BlitzballTech {Index = 29, Name = "Nap Tackle"},
            new BlitzballTech {Index = 30, Name = "Nap Tackle 2"},
            new BlitzballTech {Index = 31, Name = "Nap Tackle 3"},
            new BlitzballTech {Index = 32, Name = "Wither Tackle"},
            new BlitzballTech {Index = 33, Name = "Wither Tackle 2"},
            new BlitzballTech {Index = 34, Name = "Wither Tackle 3"},
            new BlitzballTech {Index = 35, Name = "Drain Tackle"},
            new BlitzballTech {Index = 36, Name = "Drain Tackle 2"},
            new BlitzballTech {Index = 37, Name = "Drain Tackle 3"},
            new BlitzballTech {Index = 38, Name = "Tackle Slip"},
            new BlitzballTech {Index = 39, Name = "Tackle Slip 2"},
            new BlitzballTech {Index = 40, Name = "Anti-Venom"},
            new BlitzballTech {Index = 41, Name = "Anti-Venom 2"},
            new BlitzballTech {Index = 42, Name = "Anti-Nap"},
            new BlitzballTech {Index = 43, Name = "Anti-Nap 2"},
            new BlitzballTech {Index = 44, Name = "Anti-Wither"},
            new BlitzballTech {Index = 45, Name = "Anti-Wither 2"},
            new BlitzballTech {Index = 46, Name = "Anti-Drain"},
            new BlitzballTech {Index = 47, Name = "Anti-Drain 2"},
            new BlitzballTech {Index = 48, Name = "Spin Ball"},
            new BlitzballTech {Index = 49, Name = "Grip Gloves"},
            new BlitzballTech {Index = 50, Name = "Elite Defense"},
            new BlitzballTech {Index = 51, Name = "Brawler"},
            new BlitzballTech {Index = 52, Name = "Pile Venom"},
            new BlitzballTech {Index = 53, Name = "Pile Wither"},
            new BlitzballTech {Index = 54, Name = "Regen"},
            new BlitzballTech {Index = 55, Name = "Good Morning!"},
            new BlitzballTech {Index = 56, Name = "Hi-Risk"},
            new BlitzballTech {Index = 57, Name = "Golden Arm"},
            new BlitzballTech {Index = 58, Name = "Gamble"},
            new BlitzballTech {Index = 59, Name = "Super Goalie"},
            new BlitzballTech {Index = 60, Name = "Aurochs Spirit"},
        };

        public static int GetDataOffset()
        {
            var pointer = MemoryReader.ReadInt32(_dataPointer);
            return pointer;
        }

        public static int GetGameOffset()
        {
            var pointer = MemoryReader.ReadInt32(_gamePointer);
            return pointer;
        }

        public static void RemovePlayerFromTeam(int teamIndex, int playerIndex)
        {
            
        }
    }

    public class BlitzballPlayer
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class BlitzballTech
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class BlitzballTeam
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }
}

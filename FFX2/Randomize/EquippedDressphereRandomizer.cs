using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Farplane.FFX2.Values;

namespace Farplane.FFX2
{
    public class Dresspheremap
    {
        public int ID;
        public int DS_ID;
        public string Name;

        public Dresspheremap(int id, int ds_id, string name)
        {
            ID = id;
            DS_ID = ds_id;
            Name = name;
        }
    }

    public static class EquippedDressphereRandomizer
    {
        private static uint PreviousYunaExp = 0;
        private static uint PreviousRikkuExp = 0;
        private static uint PreviousPaineExp = 0;

        private static readonly int _statsOffset = (int)OffsetType.PartyStatBase;
        private static readonly int _expOffset = (int)Offsets.StatOffsets.CurrentExperience;
        private static readonly int _currentGGOffset = (int)Offsets.StatOffsets.CurrentGarmentGrid;
        private static readonly int _currentDSOffset = (int)Offsets.StatOffsets.CurrentDresssphere;
        private static readonly int _equippedGGNode = (int)OffsetType.EquippedGarmentGridNode;
        private static readonly int _statsSize = 0x80;
        private static readonly int _garmentGridOffset = (int)OffsetType.GarmentGridNodes;

        private static Dresspheremap[] dressphereMap =
        {
            new Dresspheremap(1,1,"Gunner"),
            new Dresspheremap(2,2,"Gun Mage"),
            new Dresspheremap(3,3,"Alchemist"),
            new Dresspheremap(4,4,"Warrior"),
            new Dresspheremap(5,5,"Samurai"),
            new Dresspheremap(6,6,"Dark Knight"),
            new Dresspheremap(7,7,"Berserker"),
            new Dresspheremap(8,8,"Songstress"),
            new Dresspheremap(9,9,"Black Mage"),
            new Dresspheremap(10,10,"White Mage"),
            new Dresspheremap(11,11,"Thief"),
            new Dresspheremap(12,12,"Trainer"),
            new Dresspheremap(13,13,"Lady Luck"),
            new Dresspheremap(14,14,"Mascot"),
            new Dresspheremap(15,28,"Psychic"),
            new Dresspheremap(16,29,"Festivalist"),
        };

        private static Random random = new Random();

        public static void InitExp()
        {
            //PreviousYunaExp = LegacyMemoryReader.ReadUInt32(_statsOffset + _expOffset);
            //PreviousRikkuExp = LegacyMemoryReader.ReadUInt32(_statsOffset + _statsSize + _expOffset);
            //PreviousPaineExp = LegacyMemoryReader.ReadUInt32(_statsOffset + (_statsSize * 2) + _expOffset);

            //Randomize immedietly instead
            PreviousYunaExp = uint.MaxValue;
            PreviousRikkuExp = uint.MaxValue;
            PreviousPaineExp = uint.MaxValue;
        }

        private static bool EXPChanged()
        {
            bool result = false; 

            var currentYunaExp = LegacyMemoryReader.ReadUInt32(_statsOffset + _expOffset);
            var currentRikkuExp = LegacyMemoryReader.ReadUInt32(_statsOffset + _statsSize + _expOffset);
            var currentPaineExp = LegacyMemoryReader.ReadUInt32(_statsOffset + (_statsSize * 2) + _expOffset);

            result = currentYunaExp != PreviousYunaExp | currentRikkuExp != PreviousRikkuExp | currentPaineExp != PreviousPaineExp;

            PreviousYunaExp = currentYunaExp;
            PreviousRikkuExp = currentRikkuExp;
            PreviousPaineExp = currentPaineExp;

            return result;
        }

        public static async Task Run(CancellationToken ct)
        {
            try
            {
                bool first = true;
                do
                {
                    ct.ThrowIfCancellationRequested();
                    if (EXPChanged() || first)
                    {
                        first = false;
                        var garmentGrids = LegacyMemoryReader.ReadBytes(_garmentGridOffset, 0x800);

                        foreach (var node in GarmentGridNodes.GarmentGridNodesList)
                        {
                            garmentGrids[node.GGOffset + node.NodePosition] = BitConverter.GetBytes(dressphereMap[random.Next(1, 16)].DS_ID)[0];
                        }

                        LegacyMemoryReader.WriteBytes(_garmentGridOffset, garmentGrids);

                        //Find current equipped node
                        for (int i = 0; i < 3; i++)
                        {
                            int test = _statsOffset + (_statsSize * i) + _currentGGOffset;
                            var currentGG = LegacyMemoryReader.ReadInt16(_statsOffset + (_statsSize * i) + _currentGGOffset);
                            var currentNode = LegacyMemoryReader.ReadInt16((int)(_equippedGGNode + currentGG + (GarmentGrids.GarmentGridList.Count() * i)));
                            LegacyMemoryReader.WriteByte(_statsOffset + _currentDSOffset + (_statsSize * i), garmentGrids[GarmentGridNodes.GarmentGridNodesList.First(x => x.GarmentGridID == currentGG).GGOffset + currentNode]);
                        }
                    }

                    await Task.Delay(250);
                } while (true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Farplane.FFX2.Values;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for GarmentGridEditor.xaml
    /// </summary>
    public partial class GarmentGridEditor : UserControl
    {
        private readonly int _offsetGarmentGrids = (int) OffsetType.KnownGarmentGrids;
        private bool _refreshing;
        public GarmentGridEditor()
        {
            InitializeComponent();
            foreach (var gg in GarmentGrids.GarmentGridList)
            {
                var ggCheckBox = new CheckBox()
                {
                    Name = "Grid" + gg.ID,
                    Content = gg.Name
                };
                ggCheckBox.Checked += GarmentGridChanged;
                ggCheckBox.Unchecked += GarmentGridChanged;
                GarmentGridList.Children.Add(ggCheckBox);
            }
            Refresh();
        }

        private void GarmentGridChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_refreshing) return;

            var checkBox = sender as CheckBox;
            if (checkBox == null) return;

            var gridIndex = int.Parse(checkBox.Name.Substring(4));

            var byteIndex = gridIndex/8;
            var bitIndex = gridIndex%8;

            var garmentGridBytes = MemoryReader.ReadBytes(_offsetGarmentGrids, 8);
            var gByte = garmentGridBytes[byteIndex];

            var mask = (1 << bitIndex);
            var newByte = gByte ^ (byte) mask;

            MemoryReader.WriteBytes(_offsetGarmentGrids + byteIndex, new byte[] {(byte)newByte});
            Refresh();
        }



        public void Refresh()
        {
            _refreshing = true;
            var garmentGridBytes = MemoryReader.ReadBytes(_offsetGarmentGrids, 8);
            var gridsRem = GarmentGrids.GarmentGridList.Length % 8;
            for (int i = 0; i < GarmentGrids.GarmentGridList.Length; i++)
            {
                var checkBox = (CheckBox) GarmentGridList.Children[i];
                if (checkBox == null) continue;

                var numBytes = GarmentGrids.GarmentGridList.Length - gridsRem;


                var byteIndex = i/8;
                var bitIndex = i%8;

                byte mask = (byte)(1 << bitIndex);
                bool isSet = (garmentGridBytes[byteIndex] & mask) != 0;

                checkBox.IsChecked = isSet;
            }
            _refreshing = false;
        }

        private void GiveAllGrids_Click(object sender, RoutedEventArgs e)
        {
            Cheats.GiveAllGrids();
            Refresh();
        }
    }
}

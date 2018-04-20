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
using Farplane.Common;
using Farplane.Memory;
using MahApps.Metro.Controls;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for CreatureEditor.xaml
    /// </summary>
    public partial class CreatureEditor : UserControl
    {
        private int _creatureIndex = -1;
        private int _statsOffset = 0;
        private CreatureAbilities _creatureAbilities;
        private StatsPanel _statsPanel;
        private bool _refreshing = false;

        private int _offsetCreatureBase = (int) OffsetType.PartyStatBase;
        private int _offsetCreatureName = (int) OffsetType.CreatureNames;

        public CreatureEditor(int creatureIndex)
        {
            _creatureIndex = creatureIndex;
            _statsOffset = _offsetCreatureBase + (creatureIndex + 15)*0x80;
            InitializeComponent();

            _creatureAbilities = new CreatureAbilities(creatureIndex);
            _statsPanel = new StatsPanel();

            ccStats.Content = _statsPanel;
            TabAbilities.Content = _creatureAbilities;
            Refresh();

            foreach (TabItem tabControl in CreatureTab.Items)
                ControlsHelper.SetHeaderFontSize(tabControl, 18);
        }

        public void Refresh()
        {
            _refreshing = true;
            var nameBytes = LegacyMemoryReader.ReadBytes(_offsetCreatureName + (_creatureIndex*40), 18);
            var name = StringConverter.ToASCII(nameBytes);
            CreatureName.Text = name;
            CreatureSize.SelectedIndex = GameMemory.Read<byte>(_statsOffset + (int) Offsets.StatOffsets.Size) - 1;
            _statsPanel.Refresh(_creatureIndex + 15);
            _creatureAbilities.Refresh();
            _refreshing = false;
        }

        private void CreatureName_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var nameBytes = StringConverter.ToFFX(CreatureName.Text);
            var writeBytes = new byte[18];
            nameBytes.CopyTo(writeBytes, 0);
            LegacyMemoryReader.WriteBytes(_offsetCreatureName + _creatureIndex*40, writeBytes);
            Refresh();
        }

        private void CreatureSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            LegacyMemoryReader.WriteBytes(_statsOffset + (int) Offsets.StatOffsets.Size,
                new byte[] {(byte) (CreatureSize.SelectedIndex + 1)});
        }
    }
}
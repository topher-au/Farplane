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
using Farplane.FFX.Values;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.BlitzballPanel
{
    /// <summary>
    /// Interaction logic for BlitzballPlayerEditor.xaml
    /// </summary>
    public partial class BlitzballPlayerEditor : UserControl
    {
        private readonly int _dataOffset = Blitzball.GetDataOffset();
        private bool _canWriteData = false;

        public BlitzballPlayerEditor()
        {
            InitializeComponent();

            foreach (var player in Blitzball.Players)
                TreeBlitzPlayers.Items.Add(new TreeViewItem() {Header = player.Name});

            (TreeBlitzPlayers.Items[0] as TreeViewItem).IsSelected = true;

            _canWriteData = true;
        }

        public void Refresh()
        {
            _canWriteData = false;

            // Refresh player data
            var playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);

            var playerLevel = MemoryReader.ReadByte(_dataOffset + (int) BlitzballDataOffset.PlayerLevels + playerIndex,
                true);
            TextLevel.Text = playerLevel.ToString();

            var playerExp = MemoryReader.ReadUInt16(_dataOffset + (int) BlitzballDataOffset.PlayerEXP + playerIndex*2,
                true);
            TextEXP.Text = playerExp.ToString();

            // Refresh equipped techs

            var techCount = MemoryReader.ReadByte(_dataOffset + (int) BlitzballDataOffset.TechCount + playerIndex, true);

            ComboTechCount.SelectedIndex = techCount;

            var techData = MemoryReader.ReadBytes(_dataOffset + (int) BlitzballDataOffset.EquippedTechs + playerIndex*5, 5,
                true);
            
            for (int i = 0; i < 5; i++)
            {
                var techButton = (Button) FindName("EquippedTech" + (i+1).ToString().Trim());
                techButton.Visibility = (i >= techCount ? Visibility.Collapsed : Visibility.Visible);

                var tech = Blitzball.Techs.FirstOrDefault(t => t.Index == techData[i]);
                if (techData[i] == 0) 
                    techButton.Content = "< EMPTY >";
                else if (tech == null)
                    techButton.Content = "????";
                else
                    techButton.Content = tech.Name;
            }

            _canWriteData = true;
        }

        private void TreeBlitzPlayers_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Refresh();
        }

        private void TechCount_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (!_canWriteData) return;

            var newCount = ComboTechCount.SelectedIndex;
            var playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);

            MemoryReader.WriteByte(_dataOffset + (int) BlitzballDataOffset.TechCount + playerIndex, (byte) newCount,
                true);

            Refresh();
        }

        private void EquippedTech_OnClick(object sender, RoutedEventArgs e)
        {
            var playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);
            var techIndex = int.Parse((sender as Button).Name.Substring(12))-1;
            var techNames = Blitzball.Techs.Select(t => t.Name);
            var searchDialog = new SearchDialog(techNames.ToList(), (sender as Button).Content.ToString()) {Owner=this.TryFindParent<Window>()};
            var success = searchDialog.ShowDialog();

            if (!success.Value) return;

            if (searchDialog.ResultIndex == -1)
            {
                // clear slot
                MemoryReader.WriteByte(_dataOffset + (int)BlitzballDataOffset.EquippedTechs + techIndex + playerIndex, (byte)0,
                    true);
            }
            else
            {
                // equip tech
                var tech = Blitzball.Techs[searchDialog.ResultIndex];
                MemoryReader.WriteByte(_dataOffset + (int)BlitzballDataOffset.EquippedTechs + techIndex + (playerIndex * 5), (byte)tech.Index,
                    true);
            }
            Refresh();
        }

        private void TextLevel_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            try
            {
                var playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);
                var level = byte.Parse(TextLevel.Text);
                MemoryReader.WriteByte(_dataOffset + (int)BlitzballDataOffset.PlayerLevels + playerIndex, level,
                    true);
                TextLevel.SelectionStart = 0;
                TextLevel.SelectionLength = TextLevel.Text.Length;
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 255");
            }

            Refresh();
        }

        private void TextEXP_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            try
            {
                var playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);
                var exp = ushort.Parse(TextEXP.Text);
                MemoryReader.WriteBytes(_dataOffset + (int)BlitzballDataOffset.PlayerEXP + playerIndex, BitConverter.GetBytes(exp),
                    true);
                TextEXP.SelectionStart = 0;
                TextEXP.SelectionLength = TextEXP.Text.Length;
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 255");
            }

            Refresh();
        }
    }
}
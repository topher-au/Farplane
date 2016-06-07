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
    /// Interaction logic for BlitzballTeamEditor.xaml
    /// </summary>
    public partial class BlitzballTeamEditor : UserControl
    {
        private readonly int _dataOffset = Blitzball.GetDataOffset();
        private bool _canWriteData = false;
        private byte[] _teamData;

        public BlitzballTeamEditor()
        {
            InitializeComponent();

            for (int i = 0; i < Blitzball.Teams.Length; i++)
            {
                var teamTab = new TabItem() {Header = Blitzball.Teams[i].Name};
                ControlsHelper.SetHeaderFontSize(teamTab, 14);
                TabTeam.Items.Add(teamTab);
            }


            _canWriteData = true;
        }

        public void Refresh()
        {
            if (!_canWriteData) return;
            _canWriteData = false;

            var selectedTeam = TabTeam.SelectedIndex;

            // refresh all roster info
            _teamData = MemoryReader.ReadBytes(_dataOffset + (int) BlitzballDataOffset.TeamData, 48, true);
            var contractData = MemoryReader.ReadBytes(_dataOffset + (int) BlitzballDataOffset.ContractLength, 60, true);

            var teamSize = MemoryReader.ReadByte(Offsets.GetOffset(OffsetType.BlitzballTeamSizes) + selectedTeam);

            if (teamSize < 6)
                ComboRosterSize.SelectedIndex = 0;
            else
                ComboRosterSize.SelectedIndex = teamSize - 6;

            // refresh panel for current team
            for (int i = 0; i < 8; i++)
            {
                var playerButton = (Button) FindName("ButtonPlayer" + (i + 1).ToString().Trim());
                var contractBox = (TextBox) FindName("TextPlayer" + (i + 1).ToString().Trim());

                if (i > teamSize - 1)
                {
                    playerButton.Visibility = Visibility.Collapsed;
                    contractBox.Visibility = Visibility.Collapsed;

                    continue;
                }

                playerButton.Visibility = Visibility.Visible;
                contractBox.Visibility = Visibility.Visible;

                var playerIndex = _teamData[(selectedTeam*8) + i];

                if (playerIndex >= 0x3C) // empty slot
                {
                    playerButton.Content = "< EMPTY >";
                    contractBox.Text = string.Empty;
                }
                else
                {
                    var player = Blitzball.Players.FirstOrDefault(p => p.Index == playerIndex);
                    playerButton.Content = player.Name;
                    contractBox.Text = contractData[player.Index].ToString();
                }
            }

            _canWriteData = true;
        }

        private void TabTeam_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void ButtonPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var playerButton = (sender as Button);
            var playerIndex = int.Parse(playerButton.Name.Substring(12)) - 1;
            var teamIndex = TabTeam.SelectedIndex;

            var playerNames = Blitzball.Players.Select(player => player.Name);

            var playerSearchDialog = new SearchDialog(playerNames.ToList()) {Owner = this.TryFindParent<Window>()};

            var search = playerSearchDialog.ShowDialog();
            if (!search.Value) return;

            var offset = _dataOffset + (int) BlitzballDataOffset.TeamData + (teamIndex*8) + playerIndex;

            if (playerSearchDialog.ResultIndex != -1)
            {
                // update player
                var player = Blitzball.Players[playerSearchDialog.ResultIndex];

                // check if player is on another team
                for (int i = 0; i < 48; i++)
                {
                    if (_teamData[i] == player.Index)
                    {
                        var sourceTeam = i/8;
                        var sourcePosition = i%8;

                        var movePlayer =
                            MessageBox.Show(
                                $"{player.Name} is already a member of the {Blitzball.Teams[sourceTeam].Name}.\n\nMove {player.Name} to the {Blitzball.Teams[teamIndex].Name}?",
                                "Move player?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (movePlayer == MessageBoxResult.No) return;

                        // Remove old player from team
                        var removed = RemovePlayer(sourceTeam, sourcePosition);
                        if (!removed)
                        {
                            Error.Show(
                                $"Unable to remove {player.Name} from the {Blitzball.Teams[sourceTeam].Name}. Blitzball teams must consist of at least 6 players at all times.");
                            return;
                        }
                    }
                }

                MemoryReader.WriteByte(offset, (byte) player.Index, true);
            }
            else
            {
                // remove player
                if (!RemovePlayer(teamIndex, playerIndex))
                {
                    var player = Blitzball.Players[_teamData[(teamIndex*8) + playerIndex]];
                    Error.Show(
                        $"Unable to remove {player.Name} from the {Blitzball.Teams[teamIndex].Name}. Blitzball teams must consist of at least 6 players at all times.");
                }
            }

            Refresh();
        }

        private void RosterSize_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (!_canWriteData) return;
            var selectedTeam = TabTeam.SelectedIndex;

            MemoryReader.WriteByte(Offsets.GetOffset(OffsetType.BlitzballTeamSizes) + selectedTeam,
                (byte) (ComboRosterSize.SelectedIndex + 6));
            Refresh();
        }

        private bool RemovePlayer(int team, int player)
        {
            // read team data
            var count = MemoryReader.ReadByte(Offsets.GetOffset(OffsetType.BlitzballTeamSizes) + team);


            var offset = _dataOffset + (int) BlitzballDataOffset.TeamData + (team*8);
            var data = MemoryReader.ReadBytes(offset, 8, true);

            var actualPlayers = data.Where(d => d != 0x3C).Count();

            if (actualPlayers <= 6)
            {
                return false;
            }


            // move all players up

            for (int i = player; i < 7; i++)
            {
                data[i] = data[i + 1];
            }
            data[7] = 0x3C;

            // adjust team size
            count--;

            // write data
            MemoryReader.WriteBytes(offset, data, true);
            MemoryReader.WriteByte(Offsets.GetOffset(OffsetType.BlitzballTeamSizes) + team, count);

            return true;
        }

        private void TextPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            try
            {
                var textBox = sender as TextBox;
                var buttonIndex = int.Parse(textBox.Name.Substring(10));
                var teamIndex = TabTeam.SelectedIndex;

                var playerIndex = _teamData[teamIndex*8 + buttonIndex-1];
                MemoryReader.WriteByte(_dataOffset + (int) BlitzballDataOffset.ContractLength + playerIndex,
                    byte.Parse(textBox.Text), true);

                textBox.SelectAll();
            }
            catch
            {
                Error.Show("Please enter a number between 0 and 255.");
            }
            
        }
    }
}
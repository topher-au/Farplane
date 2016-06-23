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
using Farplane.Common.Dialogs;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using Farplane.Memory;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.BlitzballPanel
{
    /// <summary>
    /// Interaction logic for BlitzballTeamEditor.xaml
    /// </summary>
    public partial class BlitzballTeamEditor : UserControl
    {
        private readonly int _dataOffset = 0;
        private bool _canWriteData = false;
        private byte[] _teamData;

        public BlitzballTeamEditor()
        {
            InitializeComponent();

            for (int i = 0; i < BlitzballValues.Teams.Length - 1; i++)
            {
                var teamTab = new TabItem() {Header = BlitzballValues.Teams[i].Name};
                ControlsHelper.SetHeaderFontSize(teamTab, 16);
                TabTeam.Items.Add(teamTab);
            }

            TabTeam.SelectedIndex = 0;


            _canWriteData = true;
        }

        public void Refresh()
        {
            if (!_canWriteData) return;
            _canWriteData = false;

            var selectedTeam = TabTeam.SelectedIndex;

            // refresh all roster info
            var blitzData = Blitzball.ReadBlitzballData();
            _teamData = blitzData.TeamData;
            var blitzTeamSizes = Blitzball.GetTeamSizes();
            var teamSize = blitzTeamSizes[selectedTeam];

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
                    var player = BlitzballValues.Players.FirstOrDefault(p => p.Index == playerIndex);
                    playerButton.Content = player.Name;
                    contractBox.Text = blitzData.PlayerContracts[player.Index].ToString();
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

            var playerNames = BlitzballValues.Players.Select(player => player.Name);

            var playerSearchDialog = new SearchDialog(playerNames.ToList()) {Owner = this.TryFindParent<Window>()};

            var search = playerSearchDialog.ShowDialog();
            if (!search.Value) return;

            int selectedPlayer = -1;
            var searchIndex = playerSearchDialog.ResultIndex;
            if (searchIndex != -1)
                selectedPlayer = BlitzballValues.Players[searchIndex].Index;
            
            MovePlayer(selectedPlayer, teamIndex, playerIndex);

            Refresh();
        }

        private void RosterSize_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (!_canWriteData) return;

            var teamIndex = TabTeam.SelectedIndex;
            Blitzball.SetTeamSize(teamIndex, (byte) (ComboRosterSize.SelectedIndex + 6));

            Refresh();
        }

        private void MovePlayer(int playerIndex, int destTeamIndex, int destPos)
        {
            var blitzData = Blitzball.ReadBlitzballData();

            

            // Find index of player if already assigned to team
            var playerTeamIndex = Array.IndexOf(blitzData.TeamData, (byte) playerIndex);
            
            var destTeamData = new byte[8];
            Array.Copy(blitzData.TeamData, destTeamIndex * 8, destTeamData, 0, 8);
            var destTeamSize = 8 - Array.FindAll(destTeamData, b => b == (byte)0x3C).Length;
            if (destTeamSize == 8) return;

            if (playerIndex == -1)
            {
                // Check destination team
                
                destTeamData[destPos] = 0x3C;
                Array.Copy(destTeamData, 0, blitzData.TeamData, destTeamIndex * 8, 8);
                Blitzball.WriteBlitzballData(blitzData);
                Blitzball.SetTeamSize(destTeamIndex, (byte)(destTeamSize + 1));
                return;
            }

            var player = BlitzballValues.Players[playerIndex];
            var destTeam = BlitzballValues.Teams[destTeamIndex];

            // If assigned to team, we need to remove player from the team
            if (playerTeamIndex != -1)
            {
                var sourceTeamIndex = playerTeamIndex/8;
                var sourceTeam = BlitzballValues.Teams[sourceTeamIndex];

                // Copy team data to array
                var sourceTeamData = new byte[8];
                Array.Copy(blitzData.TeamData, sourceTeamIndex * 8, sourceTeamData, 0, 8);

                // Check source team size
                var sourceTeamSize = Array.FindAll(sourceTeamData, b => b != (byte)0x3C).Length;
                if (sourceTeamSize <= 6)
                {
                    // Team too small to remove player, return error
                    MessageBox.Show(
                        string.Format(
                            "{0} is already assigned to the {1}, but could not be removed as the {1} must have at least 6 players.\n\n" +
                            "Remove {0} from the {1} or add another player before trying again.",
                            player.Name, sourceTeam.Name),
                        "Unable to assign player", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Player is in a team, prompt user
                var movePlayer =
                    MessageBox.Show(
                        string.Format("{0} is already in the {1}. Press OK to move {0} to the {2}", player.Name, sourceTeam.Name ,
                            destTeam.Name), "Player already assigned",
                        MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (movePlayer != MessageBoxResult.OK) return;

                // Remove player from source team
                
                // Find player position
                var playerPos = Array.IndexOf(sourceTeamData, (byte) player.Index);
                
                for (int i = playerPos; i < 7; i++)
                {
                    sourceTeamData[i] = sourceTeamData[i+1];    // Move other players up one position
                    sourceTeamData[7] = 0x3C;                   // Empty final slot
                }

                // Copy updated roster back to blitz data
                Array.Copy(sourceTeamData, 0, blitzData.TeamData, sourceTeamIndex * 8, 8);
                Blitzball.WriteBlitzballData(blitzData);

                // Resize team
                Blitzball.SetTeamSize(sourceTeamIndex, (byte)(sourceTeamSize - 1));
            }

            // Check destination team

            if (destTeamSize == 8) return;

            // Insert player into team roster
            Array.Copy(blitzData.TeamData, destTeamIndex*8, destTeamData, 0, 8);

            destTeamData[destPos] = (byte)player.Index;
            Array.Copy(destTeamData, 0, blitzData.TeamData, destTeamIndex * 8, 8);
            Blitzball.WriteBlitzballData(blitzData);
        }

        private void TextPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            try
            {
                var textBox = sender as TextBox;
                var buttonIndex = int.Parse(textBox.Name.Substring(10));
                var teamIndex = TabTeam.SelectedIndex;

                var playerIndex = _teamData[teamIndex*8 + buttonIndex - 1];

                var blitzData = Blitzball.ReadBlitzballData();
                blitzData.PlayerContracts[playerIndex] = byte.Parse(textBox.Text);
                Blitzball.WriteBlitzballData(blitzData);
                
                textBox.SelectAll();
            }
            catch
            {
                Error.Show("Please enter a number between 0 and 255.");
            }
        }
    }
}
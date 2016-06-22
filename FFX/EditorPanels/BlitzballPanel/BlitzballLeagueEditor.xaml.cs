using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Farplane.Common.Dialogs;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.BlitzballPanel
{
    /// <summary>
    /// Interaction logic for BlitzballGeneralEditor.xaml
    /// </summary>
    public partial class BlitzballLeagueEditor : UserControl
    {
        private bool _refresh;

        public BlitzballLeagueEditor()
        {
            InitializeComponent();

            _refresh = true;
            ComboLeagueStatus.ItemsSource = BlitzballValues.LeagueStates.Select(state => state.Name);
            foreach (var teamCombo in this.FindChildren<ComboBox>().Where(child => child.Name.StartsWith("LeagueTeam") == true))
                teamCombo.ItemsSource = BlitzballValues.Teams.Select(team => team.Name);
            _refresh = false;
        }

        public void Refresh()
        {
            _refresh = true;
            var prizes = BlitzballValues.Prizes;
            var blitzData = Blitzball.ReadBlitzballData();

            // Update League status
            var leagueStatusIndex =
                Array.IndexOf(BlitzballValues.LeagueStates, BlitzballValues.LeagueStates.First(state => state.Index == blitzData.LeagueStatus));

            ComboLeagueStatus.SelectedIndex = leagueStatusIndex;

            // Update team matchups
            for (int i = 0; i < 6; i++)
            {
                var teamCombo = FindName($"LeagueTeam{i}") as ComboBox;
                if (teamCombo == null) continue;
                teamCombo.SelectedIndex = blitzData.LeagueMatchups[i];
            }

            // Update prizes
            for (int i = 0; i < 8; i++)
            {
                var prizeButton = FindName($"Prize{i}") as Button;
                if (prizeButton == null) continue;
                var currentPrize = prizes.FirstOrDefault(prize => prize.Index == blitzData.BlitzballPrizes[i]);
                if (currentPrize != null)
                    prizeButton.Content = currentPrize.Name;
                else
                    prizeButton.Content = $"???? [{blitzData.BlitzballPrizes[i].ToString("X2")}]";
            }

            _refresh = false;
        }

        private void Prize_OnClick(object sender, RoutedEventArgs e)
        {
            var prizeButton = sender as Button;
            var prizeDialog = new SearchDialog(BlitzballValues.Prizes.Select(prize => prize.Name).ToList(), string.Empty, false);
            var dialogResult = prizeDialog.ShowDialog();
            if (dialogResult == false) return;

            var prizeNumber = int.Parse(prizeButton.Name.Substring(5));
            var prizeIndex = BlitzballValues.Prizes[prizeDialog.ResultIndex].Index;
            var blitzData = Blitzball.ReadBlitzballData();
            blitzData.BlitzballPrizes[prizeNumber] = (ushort)prizeIndex;
            Blitzball.WriteBlitzballData(blitzData);

            Refresh();
        }

        private void LeagueTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;
            var teamBox = sender as ComboBox;
            var teamNum = int.Parse(teamBox.Name.Substring(10));
            var teamIndex = BlitzballValues.Teams[teamBox.SelectedIndex].Index;
            var blitzData = Blitzball.ReadBlitzballData();
            blitzData.LeagueMatchups[teamNum] = (byte)teamIndex;
            Blitzball.WriteBlitzballData(blitzData);
            Refresh();
        }

        private void CurrentRound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;
            var roundBox = sender as ComboBox;
            var roundIndex = BlitzballValues.LeagueStates[roundBox.SelectedIndex].Index;
            var blitzData = Blitzball.ReadBlitzballData();
            blitzData.LeagueStatus = (byte)roundIndex;
            Blitzball.WriteBlitzballData(blitzData);
            Refresh();
        }
    }
}

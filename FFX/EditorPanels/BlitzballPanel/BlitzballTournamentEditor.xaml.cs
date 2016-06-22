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
    public partial class BlitzballTournamentEditor : UserControl
    {
        private bool _refresh;

        public BlitzballTournamentEditor()
        {
            InitializeComponent();

            _refresh = true;
            ComboTournamentStatus.ItemsSource = BlitzballValues.TournamentStates.Select(state => state.Name);
            foreach (var teamCombo in this.FindChildren<ComboBox>().Where(child => child.Name.StartsWith("ComboWinner") || child.Name.StartsWith("ComboTeam")))
                teamCombo.ItemsSource = BlitzballValues.Teams.Select(team => team.Name);
            _refresh = false;
        }

        public void Refresh()
        {
            _refresh = true;
            var prizes = BlitzballValues.Prizes;
            var blitzData = Blitzball.ReadBlitzballData();

            // Tournament status
            var tournamentStatusIndex =
                Array.IndexOf(BlitzballValues.TournamentStates, BlitzballValues.TournamentStates.First(state => state.Index == blitzData.TournamentStatus));

            ComboTournamentStatus.SelectedIndex = tournamentStatusIndex;

            // Prizes
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

            // Tournament teams
            for (int i = 0; i < 6; i++)
            {
                var comboTeam = FindName($"ComboTeam{i}") as ComboBox;
                if (comboTeam == null) continue;

                var currentTeam = BlitzballValues.Teams.First(team => team.Index == blitzData.TournamentMatchups[i]);
                comboTeam.SelectedIndex = Array.IndexOf(BlitzballValues.Teams, currentTeam);
            }

            // Tournament winners
            for (int i = 0; i < 8; i++)
            {
                var comboTeam = FindName($"ComboWinner{i}") as ComboBox;
                if (comboTeam == null) continue;

                var currentTeam = BlitzballValues.Teams.First(team => team.Index == blitzData.TournamentWinners[i]);
                comboTeam.SelectedIndex = Array.IndexOf(BlitzballValues.Teams, currentTeam);
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

        private void ComboTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;
            var teamBox = sender as ComboBox;
            var teamNum = int.Parse(teamBox.Name.Substring(9));
            var teamIndex = BlitzballValues.Teams[teamBox.SelectedIndex].Index;
            var blitzData = Blitzball.ReadBlitzballData();
            blitzData.TournamentMatchups[teamNum] = (byte)teamIndex;
            Blitzball.WriteBlitzballData(blitzData);
            Refresh();
        }

        private void ComboWinner_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;
            var teamBox = sender as ComboBox;
            var teamNum = int.Parse(teamBox.Name.Substring(11));
            var teamIndex = BlitzballValues.Teams[teamBox.SelectedIndex].Index;
            var blitzData = Blitzball.ReadBlitzballData();
            blitzData.TournamentWinners[teamNum] = (byte)teamIndex;
            Blitzball.WriteBlitzballData(blitzData);
            Refresh();
        }
    }
}

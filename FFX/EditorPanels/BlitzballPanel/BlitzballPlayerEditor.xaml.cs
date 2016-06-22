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
using Farplane.Common.Controls;
using Farplane.Common.Dialogs;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.BlitzballPanel
{
    /// <summary>
    /// Interaction logic for BlitzballPlayerEditor.xaml
    /// </summary>
    public partial class BlitzballPlayerEditor : UserControl
    {
        private bool _refresh;
        private int _playerIndex = 0;
        private Data.BlitzballPlayer[] _players;
        private ButtonGrid _buttons = new ButtonGrid(3, BlitzballValues.Techs.Length);
        private static readonly Tuple<AppTheme, Accent> currentStyle = ThemeManager.DetectAppStyle(Application.Current);

        private readonly Brush _trueBrush =
            new SolidColorBrush((Color) currentStyle.Item1.Resources["BlackColor"]);

        private readonly Brush _falseBrush = new SolidColorBrush((Color) currentStyle.Item1.Resources["Gray2"]);

        public BlitzballPlayerEditor()
        {
            InitializeComponent();

            foreach (var player in BlitzballValues.Players)
                TreeBlitzPlayers.Items.Add(new TreeViewItem() {Header = player.Name});

            KnownTechs.Content = _buttons;
            for (int i = 0; i < BlitzballValues.Techs.Length; i++)
                _buttons.SetContent(i, BlitzballValues.Techs[i].Name);

            _buttons.ButtonClicked += ButtonsOnButtonClicked;
        }

        private void ButtonsOnButtonClicked(int buttonIndex)
        {
            var player = _players[_playerIndex];
            var skillIndex = BlitzballValues.Techs[buttonIndex].Index;
            int skillData = 0;


            
            if (buttonIndex < 30)
            {
                // Skill Flags 1
                var byteIndex = (buttonIndex+1) / 8;
                var bitIndex = (buttonIndex+1) % 8;
                skillData = player.SkillFlags1;
                var flagBytes = BitConverter.GetBytes(skillData);
                flagBytes[byteIndex] = (byte)(flagBytes[byteIndex] ^ (1 << bitIndex));

                player.SkillFlags1 = BitConverter.ToInt32(flagBytes, 0);
            }
            else
            {
                // Skill Flags 2
                var byteIndex = (buttonIndex-29) / 8;
                var bitIndex = ((buttonIndex - 29) % 8);
                skillData = player.SkillFlags2;
                var flagBytes = BitConverter.GetBytes(skillData);
                flagBytes[byteIndex] = (byte)(flagBytes[byteIndex] ^ (1 << bitIndex));

                player.SkillFlags2 = BitConverter.ToInt32(flagBytes, 0);
            }

            Blitzball.SetPlayerInfo(_playerIndex, player);

            Refresh();
        }

        public void Refresh()
        {
            _refresh = true;

            if (TreeBlitzPlayers.SelectedItem == null)
                (TreeBlitzPlayers.Items[0] as TreeViewItem).IsSelected = true;

            // Refresh player data

            _playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);
            _players = Data.Blitzball.GetPlayers();
            RefreshCurrentPlayer();

            _refresh = false;
        }

        public void RefreshCurrentPlayer()
        {
            _refresh = true;

            // Refresh player data

            _playerIndex = TreeBlitzPlayers.Items.IndexOf(TreeBlitzPlayers.SelectedItem);

            var player = _players[_playerIndex];

            TextLevel.Text = player.Level.ToString();
            TextEXP.Text = player.Experience.ToString();
            TextSalary.Text = player.Salary.ToString();

            // Refresh equipped techs
            ComboTechCount.SelectedIndex = player.TechCapacity;

            var techData = player.Techs;

            for (int i = 0; i < 5; i++)
            {
                var techButton = (Button) FindName("EquippedTech" + (i + 1).ToString().Trim());
                techButton.Visibility = (i >= player.TechCapacity ? Visibility.Collapsed : Visibility.Visible);

                var tech = BlitzballValues.Techs.FirstOrDefault(t => t.Index == techData[i]);
                if (techData[i] == 0)
                    techButton.Content = "< EMPTY >";
                else if (tech == null)
                    techButton.Content = "????";
                else
                    techButton.Content = tech.Name;
            }

            // Refresh known techs
            for (int i = 0; i < BlitzballValues.Techs.Length; i++)
            {
                var skillIndex = BlitzballValues.Techs[i].Index;
                int skillData = 0;
                if (i < 30)
                {
                    // Skill Flags 1
                    skillData = player.SkillFlags1;
                }
                else
                {
                    // Skill Flags 2
                    skillIndex -= 30;
                    skillData = player.SkillFlags2;
                }

                var byteIndex = skillIndex/8;
                var bitIndex = skillIndex%8;

                var flagBytes = BitConverter.GetBytes(skillData);
                var flagSet = (flagBytes[byteIndex] & (1 << bitIndex)) == (1 << bitIndex);

                if (flagSet)
                {
                    // Tech known
                    _buttons[i].Foreground = _trueBrush;
                }
                else
                {
                    // Tech not known
                    _buttons[i].Foreground = _falseBrush;
                }
            }

            _refresh = false;
        }

        private void TreeBlitzPlayers_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_refresh) return;
            RefreshCurrentPlayer();
        }

        private void TechCount_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;

            var newCount = ComboTechCount.SelectedIndex;

            var player = _players[_playerIndex];
            player.TechCapacity = (byte) newCount;

            Data.Blitzball.SetPlayerInfo(_playerIndex, player);

            RefreshCurrentPlayer();
        }

        private void EquippedTech_OnClick(object sender, RoutedEventArgs e)
        {
            var techIndex = int.Parse((sender as Button).Name.Substring(12)) - 1;
            var techNames = BlitzballValues.Techs.Select(t => t.Name);
            var searchDialog = new SearchDialog(techNames.ToList(), (sender as Button).Content.ToString())
            {
                Owner = this.TryFindParent<Window>()
            };
            var success = searchDialog.ShowDialog();

            if (!success.Value) return;

            var player = _players[_playerIndex];
            if (searchDialog.ResultIndex == -1)
            {
                player.Techs[techIndex] = 0;
            }
            else
            {
                // equip tech
                var tech = BlitzballValues.Techs[searchDialog.ResultIndex];
                player.Techs[techIndex] = (byte) tech.Index;
            }

            Data.Blitzball.SetPlayerInfo(_playerIndex, player);
            RefreshCurrentPlayer();
        }

        private void TextLevel_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var player = _players[_playerIndex];

            try
            {
                var level = byte.Parse(TextLevel.Text);
                player.Level = level;
                TextLevel.SelectAll();
                Data.Blitzball.SetPlayerInfo(_playerIndex, player);
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 255");
            }

            RefreshCurrentPlayer();
        }

        private void TextEXP_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var player = _players[_playerIndex];
            try
            {
                var exp = ushort.Parse(TextEXP.Text);
                player.Experience = exp;
                TextEXP.SelectAll();
                Data.Blitzball.SetPlayerInfo(_playerIndex, player);
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 65535");
            }

            RefreshCurrentPlayer();
        }

        private void TextSalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var player = _players[_playerIndex];
            try
            {
                var salary = ushort.Parse(TextSalary.Text);
                player.Salary = salary;
                TextSalary.SelectAll();
                Blitzball.SetPlayerInfo(_playerIndex, player);
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 65535");
            }

            RefreshCurrentPlayer();
        }

        private void TextTournamentGoals_OnKeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TextLeagueGoals_OnKeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
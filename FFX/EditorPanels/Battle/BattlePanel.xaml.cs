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

namespace Farplane.FFX.EditorPanels.Battle
{
    /// <summary>
    /// Interaction logic for BattlePanel.xaml
    /// </summary>
    public partial class BattlePanel : UserControl
    {
        private bool _componentsLoaded = false;
        private bool _refreshing = false;

        private BattleEntityData[] _partyEntities = new BattleEntityData[8];
        private BattleEntityData[] _enemyEntities = new BattleEntityData[8];

        private int _entityIndex = 0;

        private byte[] _entityClipboard;

        public BattlePanel()
        {
            InitializeComponent();

            foreach(var tabItem in TabBattle.Items)
                ControlsHelper.SetHeaderFontSize((UIElement)tabItem, 14);

            foreach (var tabItem in TabEntity.Items)
                ControlsHelper.SetHeaderFontSize((UIElement)tabItem, 14);

            _componentsLoaded = true;
        }

        public void Refresh()
        {
            if (!_componentsLoaded) return;

            if (!BattleEntity.CheckBattleState())
            {
                // hide display
                StackBattle.Visibility = Visibility.Collapsed;
                TextEnterBattle.Visibility=Visibility.Visible;
                return;
            }

            StackBattle.Visibility = Visibility.Visible;
            TextEnterBattle.Visibility = Visibility.Collapsed;

            if (TabBattle.SelectedIndex == 0)
                RefreshParty();
            else
                RefreshEnemies();

            RefreshEntity();
        }

        public void RefreshEnemies()
        {
            for (int i = 0; i < 8; i++)
            {
                BattleEntityData readEntity;
                var success = BattleEntity.ReadEntity(EntityType.Enemy, i, out readEntity);
                _enemyEntities[i] = readEntity;

                if (readEntity.guid == 0 || !success)
                {
                    (TabEntity.Items[i] as TabItem).Visibility = Visibility.Collapsed;
                    continue;
                }

                var entityTab = TabEntity.Items[i] as TabItem;
                entityTab.Visibility = Visibility.Visible;
                entityTab.Header = StringConverter.ToString(readEntity.text_name);
            }
        }

        public void RefreshParty()
        {
            for (int i = 0; i < 8; i++)
            {
                BattleEntityData readEntity;
                var success = BattleEntity.ReadEntity(EntityType.Party, i, out readEntity);
                _partyEntities[i] = readEntity;

                if (readEntity.guid == 0 || !success)
                {
                    (TabEntity.Items[i] as TabItem).Visibility = Visibility.Collapsed;
                    continue;
                }

                var entityTab = TabEntity.Items[i] as TabItem;
                entityTab.Visibility = Visibility.Visible;
                entityTab.Header = ((Characters)i).ToString();
            }
        }

        public void RefreshEntity()
        {
            if (!_componentsLoaded) return;
            BattleEntityData entityData;

            var success = BattleEntity.ReadEntity((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                out entityData);

            TextCurrentHP.Text = entityData.hp_current.ToString();
            TextCurrentMP.Text = entityData.mp_current.ToString();
            TextOverdrive.Text = entityData.overdrive_current.ToString();
            TextStrength.Text = entityData.strength.ToString();
            TextDefense.Text = entityData.defense.ToString();
            TextMagic.Text = entityData.magic.ToString();
            TextMagicDefense.Text = entityData.magic_defense.ToString();
            TextAgility.Text = entityData.agility.ToString();
            TextLuck.Text = entityData.luck.ToString();
            TextEvasion.Text = entityData.evasion.ToString();
            TextAccuracy.Text = entityData.accuracy.ToString();

            // Refresh negative status checkboxes
            var statusFlags = BitHelper.GetBitArray(entityData.status_flags_negative);
            for (int i = 0; i < 16; i++)
            {
                var boxName = "CheckFlag" + (i+1).ToString().Trim();
                var box = (CheckBox)FindName(boxName);
                if (box == null) continue;

                box.IsChecked = statusFlags[i];
            }

            TextSilence.Text = entityData.status_turns_silence.ToString();
            TextDarkness.Text = entityData.status_turns_darkness.ToString();
            TextSleep.Text = entityData.status_turns_sleep.ToString();
        }

        private void TabBattle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
            RefreshEntity();
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            var entityOffset = BattleEntity.GetEntityOffset(EntityType.Enemy, 0);
            _entityClipboard = MemoryReader.ReadBytes(entityOffset, (int) BlockLength.BattleEntity, true);
        }

        private void ButtonPaste_Click(object sender, RoutedEventArgs e)
        {
            if (_entityClipboard == null) return;

            var entityOffset = BattleEntity.GetEntityOffset(EntityType.Enemy, 0);
            MemoryReader.WriteBytes(entityOffset, _entityClipboard, true);
        }

        private void TabEntity_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _entityIndex = TabEntity.SelectedIndex;
            RefreshEntity();
        }

        private void TextCurrentHP_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if (!BattleEntity.CheckBattleState())
            {
                Refresh();
                return;
            }

            var senderBox = sender as TextBox;
            try
            {
                switch (senderBox.Name)
                {
                    case "TextCurrentHP":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.hp_current, BitConverter.GetBytes(int.Parse(senderBox.Text)));
                        break;
                    case "TextCurrentMP":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.mp_current, BitConverter.GetBytes(int.Parse(senderBox.Text)));
                        break;
                    case "TextOverdrive":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.overdrive_current, byte.Parse(senderBox.Text));
                        break;
                    case "TextStrength":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.strength, byte.Parse(senderBox.Text));
                        break;
                    case "TextDefense":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.defense, byte.Parse(senderBox.Text));
                        break;
                    case "TextMagic":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.magic, byte.Parse(senderBox.Text));
                        break;
                    case "TextMagicDefense":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.magic_defense, byte.Parse(senderBox.Text));
                        break;
                    case "TextAgility":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.agility, byte.Parse(senderBox.Text));
                        break;
                    case "TextLuck":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.luck, byte.Parse(senderBox.Text));
                        break;
                    case "TextEvasion":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.evasion, byte.Parse(senderBox.Text));
                        break;
                    case "TextAccuracy":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.accuracy, byte.Parse(senderBox.Text));
                        break;
                }
            }
            catch
            {
                Error.Show("The value you entered was invalid.");
            }
        

    }
    }
}

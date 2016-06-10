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
        private bool _canWriteData = false;

        private BattleEntityData[] _partyEntities = new BattleEntityData[8];
        private BattleEntityData[] _enemyEntities = new BattleEntityData[8];

        private int _enemyCount = 0;
        private int _partyCount = 0;

        private bool IsInBattle => BattleEntity.CheckBattleState();

        public BattlePanel()
        {
            InitializeComponent();

            foreach (var tabItem in TabBattle.Items)
                ControlsHelper.SetHeaderFontSize((UIElement) tabItem, 14);

            foreach (var tabItem in TabEntity.Items)
                ControlsHelper.SetHeaderFontSize((UIElement) tabItem, 14);

            _canWriteData = true;
        }

        public void Refresh()
        {
            if (!_canWriteData) return;

            if (!CheckBattleState()) return;
            _canWriteData = false;
            if (TabBattle.SelectedIndex == 0)
                RefreshParty();
            else
                RefreshEnemies();

            RefreshEntity();

            _canWriteData = true;
        }

        private bool CheckBattleState()
        {
            if (IsInBattle)
            {
                StackBattle.Visibility = Visibility.Visible;
                TextEnterBattle.Visibility = Visibility.Collapsed;
            }
            else
            {
                StackBattle.Visibility = Visibility.Collapsed;
                TextEnterBattle.Visibility = Visibility.Visible;
            }
            return IsInBattle;
        }

        public void RefreshEnemies()
        {
            _enemyCount = 0;
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

                _enemyCount++;
            }
        }

        public void RefreshParty()
        {
            _partyCount = 0;
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
                entityTab.Header = ((Character) i).ToString();
                _partyCount++;
            }
        }

        public void RefreshEntity()
        {
            if (TabBattle.SelectedIndex == 0 && TabEntity.SelectedIndex >= _partyCount)
                TabEntity.SelectedIndex = 0;

            if (TabBattle.SelectedIndex == 1 && TabEntity.SelectedIndex >= _enemyCount)
                TabEntity.SelectedIndex = 0;

            BattleEntityData entityData;
            BattleEntity.ReadEntity((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                out entityData);

            // Refresh stats panel
            TextCurrentHP.Text = entityData.hp_current.ToString();
            TextCurrentMP.Text = entityData.mp_current.ToString();
            TextMaxHP.Text = entityData.hp_max.ToString();
            TextMaxMP.Text = entityData.mp_max.ToString();
            TextOverdrive.Text = entityData.overdrive_current.ToString();
            TextOverdriveMax.Text = entityData.overdrive_max.ToString();
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
                var boxName = "CheckFlag" + (i + 1).ToString().Trim();
                var box = (CheckBox) FindName(boxName);
                if (box == null) continue;
                box.IsChecked = statusFlags[i];
            }

            CheckSilence.IsChecked = entityData.status_turns_silence != 0;
            TextSilence.Text = entityData.status_turns_silence.ToString();

            CheckDarkness.IsChecked = entityData.status_turns_darkness != 0;
            TextDarkness.Text = entityData.status_turns_darkness.ToString();

            CheckSleep.IsChecked = entityData.status_turns_sleep != 0;
            TextSleep.Text = entityData.status_turns_sleep.ToString();

            TextDoom.Text = entityData.timer_doom.ToString();

            // Refresh positive status checkboxes
            CheckShell.IsChecked = entityData.status_shell != 0;
            CheckProtect.IsChecked = entityData.status_protect != 0;
            CheckReflect.IsChecked = entityData.status_reflect != 0;
            CheckNulTide.IsChecked = entityData.status_nultide != 0;
            CheckNulBlaze.IsChecked = entityData.status_nulblaze != 0;
            CheckNulShock.IsChecked = entityData.status_nulshock != 0;
            CheckNulFrost.IsChecked = entityData.status_nulfrost != 0;
            CheckRegen.IsChecked = entityData.status_regen != 0;
            CheckHaste.IsChecked = entityData.status_haste != 0;
            CheckSlow.IsChecked = entityData.status_slow != 0;
            CheckUnknown.IsChecked = entityData.status_unknown != 0;

            var posFlags = BitHelper.GetBitArray(entityData.status_flags_positive);
            for (int i = 0; i < 16; i++)
            {
                var boxName = "CheckPositiveFlag" + (i + 1).ToString().Trim();
                var box = (CheckBox)FindName(boxName);
                if (box == null) continue;
                box.IsChecked = posFlags[i];
            }
        }

        private void SetPositiveStatus(EntityType entityType, int entityIndex, EntityDataOffset statusOffset, bool statusState)
        {
            if (!_canWriteData) return;
            BattleEntity.WriteBytes(entityType, entityIndex, statusOffset, statusState ? (byte)0xFF : (byte)0);
        }

        private void TabBattle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TabEntity_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TextBoxStat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if (!CheckBattleState()) return;

            if (!_canWriteData) return;

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
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.mp_current, BitConverter.GetBytes(int.Parse(senderBox.Text)));
                        break;
                    case "TextMaxHP":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.hp_max, BitConverter.GetBytes(int.Parse(senderBox.Text)));
                        break;
                    
                    case "TextMaxMP":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.mp_max, BitConverter.GetBytes(int.Parse(senderBox.Text)));
                        break;
                    case "TextOverdrive":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.overdrive_current, byte.Parse(senderBox.Text));
                        break;
                    case "TextOverdriveMax":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.overdrive_max, byte.Parse(senderBox.Text));
                        break;
                    case "TextStrength":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.strength, byte.Parse(senderBox.Text));
                        break;
                    case "TextDefense":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.defense, byte.Parse(senderBox.Text));
                        break;
                    case "TextMagic":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.magic, byte.Parse(senderBox.Text));
                        break;
                    case "TextMagicDefense":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.magic_defense, byte.Parse(senderBox.Text));
                        break;
                    case "TextAgility":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.agility, byte.Parse(senderBox.Text));
                        break;
                    case "TextLuck":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.luck, byte.Parse(senderBox.Text));
                        break;
                    case "TextEvasion":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.evasion, byte.Parse(senderBox.Text));
                        break;
                    case "TextAccuracy":
                        BattleEntity.WriteBytes((EntityType) TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.accuracy, byte.Parse(senderBox.Text));
                        break;
                    case "TextDoom":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.timer_doom, byte.Parse(TextDoom.Text));
                        break;
                    case "TextSilence":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.status_turns_silence, byte.Parse(TextSilence.Text));
                        break;
                    case "TextDarkness":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.status_turns_darkness, byte.Parse(TextDarkness.Text));
                        break;
                    case "TextSleep":
                        BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex,
                            EntityDataOffset.status_turns_sleep, byte.Parse(TextSleep.Text));
                        break;
                }
            }
            catch
            {
                Error.Show("The value you entered was invalid.");
            }
        }

        private void CheckSleep_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!CheckBattleState()) return;
            if (!_canWriteData) return;

            byte statusTurns = 0;

            if (CheckSleep.IsChecked.Value)
            {
                try
                {
                    statusTurns = byte.Parse(TextSleep.Text);
                }
                catch { }

                if (statusTurns == 0) statusTurns = 3;
            }
            
            BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_turns_sleep, statusTurns);

            Refresh();
        }
        private void CheckDarkness_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!CheckBattleState()) return;
            if (!_canWriteData) return;

            byte statusTurns = 0;
            if (CheckDarkness.IsChecked.Value)
            {
                try
                {
                    statusTurns = byte.Parse(TextDarkness.Text);
                }
                catch
                {
                }
                if (statusTurns == 0) statusTurns = 3;
            }

            

            BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_turns_darkness, statusTurns);

            Refresh();
        }
        private void CheckSilence_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!CheckBattleState()) return;
            if (!_canWriteData) return;

            byte statusTurns = 0;

            if (CheckSilence.IsChecked.Value)
            {
                try
                {
                    statusTurns = byte.Parse(TextSilence.Text);
                }
                catch
                {
                }
                if (statusTurns == 0) statusTurns = 3;
            }

            

            BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_turns_silence, statusTurns);
            
            Refresh();
        }
        private void CheckFlagNegative_Changed(object sender, RoutedEventArgs e)
        {
            if (!_canWriteData) return;
            var name = (sender as CheckBox).Name;
            var index = int.Parse(name.Substring(9)) -1;

            var byteIndex = index/8;
            var bitIndex = index%8;

            BattleEntityData readEntity;
            BattleEntity.ReadEntity((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, out readEntity);

            var negativeFlags = readEntity.status_flags_negative;
            negativeFlags[byteIndex] = BitHelper.ToggleBit(negativeFlags[byteIndex], bitIndex);

            BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_flags_negative, negativeFlags);
        }

        private void CheckShell_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_shell, CheckShell.IsChecked.Value);
        }

        private void CheckProtect_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_protect, CheckProtect.IsChecked.Value);
        }

        private void CheckReflect_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_reflect, CheckReflect.IsChecked.Value);
        }

        private void CheckNulTide_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_nultide, CheckNulTide.IsChecked.Value);
        }

        private void CheckNulBlaze_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_nulblaze, CheckNulBlaze.IsChecked.Value);
        }

        private void CheckNulShock_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_nulshock, CheckNulShock.IsChecked.Value);
        }

        private void CheckNulFrost_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_nulfrost, CheckNulFrost.IsChecked.Value);
        }

        private void CheckRegen_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_regen, CheckRegen.IsChecked.Value);
        }
        
        private void CheckHaste_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_haste, CheckHaste.IsChecked.Value);
        }

        private void CheckSlow_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_slow, CheckSlow.IsChecked.Value);
        }

        private void CheckUnknown_OnChecked(object sender, RoutedEventArgs e)
        {
            SetPositiveStatus((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.status_unknown, CheckUnknown.IsChecked.Value);
        }

        private void CheckPositiveFlag_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_canWriteData) return;
            var name = (sender as CheckBox).Name;
            var index = int.Parse(name.Substring(17)) - 1;

            var byteIndex = index / 8;
            var bitIndex = index % 8;

            BattleEntityData readEntity;
            BattleEntity.ReadEntity((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, out readEntity);

            var positiveFlags = readEntity.status_flags_positive;
            positiveFlags[byteIndex] = BitHelper.ToggleBit(positiveFlags[byteIndex], bitIndex);

            BattleEntity.WriteBytes((EntityType)TabBattle.SelectedIndex, TabEntity.SelectedIndex, EntityDataOffset.positive_status_flags, positiveFlags);
        }
    }
}
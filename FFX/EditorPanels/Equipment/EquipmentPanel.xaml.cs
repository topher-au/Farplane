using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.Equipment
{
    /// <summary>
    /// Interaction logic for EquipmentPanel.xaml
    /// </summary>
    public partial class EquipmentPanel : UserControl
    {
        private const string NameUnknown = "????";
        private const string NameEmpty = "< Empty >";
        private const int TotalSlots = 0xB2;

        private int currentItem = -1;
        private byte[] _allEquipBytes;
        private EquipmentItem _currentItem;


        private int _selectedItem = -1;
        private bool _refreshing = false;

        private readonly BitmapImage[] _icons =
        {
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_0_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_0_1.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_1_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_1_1.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_2_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_2_1.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_3_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_3_1.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_4_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_4_1.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_5_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_5_1.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_6_0.png")),
            new BitmapImage(new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_6_1.png")),
        };


        public EquipmentPanel()
        {
            InitializeComponent();

            // Initialize equipment item view
            for (var charaIndex = 0; charaIndex < 18; charaIndex++)
                ComboEquipmentCharacter.Items.Add((Characters) charaIndex);

            for (var formulaIndex = 0; formulaIndex < DamageFormula.DamageFormulas.Length; formulaIndex++)
                ComboDamageFormula.Items.Add(DamageFormula.DamageFormulas[formulaIndex].Name);
        }

        public void ReadEquipmentBytes()
        {
            _allEquipBytes = MemoryReader.ReadBytes(Offsets.GetOffset(OffsetType.EquipmentBase),
                TotalSlots*(int) BlockLength.EquipmentItem);
        }

        public void Refresh()
        {
            _refreshing = true;

            ListEquipment.Items.Clear();

            ReadEquipmentBytes();

            for (int equipmentSlot = 0; equipmentSlot < TotalSlots; equipmentSlot++)
            {
                var imageIcon = new Image {Width = 24, Height = 24, Name = "ImageIcon"};
                var textName = new TextBlock() {VerticalAlignment = VerticalAlignment.Center, Name = "TextName"};

                var panelItem = new DockPanel() {Children = {imageIcon, textName}, Margin = new Thickness(0)};
                var listItem = new ListViewItem() {Content = panelItem};

                var currentItem = Data.Equipment.ReadItem(equipmentSlot);

                if (currentItem.Character > 6)
                {
                    // Aeon or Seymour, no name available
                    var charaName = ((Characters) currentItem.Character).ToString();
                    textName.Text = $"{NameUnknown} [{charaName}]";
                }
                else if (currentItem.SlotOccupied == 0)
                {
                    // Empty slot
                    textName.Text = NameEmpty;
                }
                else
                {
                    textName.Text = EquipName.EquipNames[currentItem.Character][currentItem.Name];
                    imageIcon.Source = _icons[currentItem.Character*2 + currentItem.Type];
                }

                ListEquipment.Items.Add(listItem);
            }

            if (ListEquipment.SelectedIndex == -1) ListEquipment.SelectedIndex = 0;
            RefreshSelectedItem();

            _refreshing = false;
        }

        private void RefreshSelectedItem()
        {
            _refreshing = true;

            ReadEquipmentBytes();

            if (_selectedItem == -1) _selectedItem = 0;

            _currentItem = Data.Equipment.ReadItem(_selectedItem);

            if ((_selectedItem >= 0x0C && _selectedItem <= 0x21) ||
                _currentItem.SlotOccupied == 0)
                ButtonDeleteItem.IsEnabled = false;
            else
                ButtonDeleteItem.IsEnabled = true;

            var listItem = ListEquipment.Items[_selectedItem] as ListViewItem;
            var imageIcon = (listItem.Content as DockPanel).FindChild<Image>("ImageIcon");
            var listText = (listItem.Content as DockPanel).FindChild<TextBlock>("TextName");

            if (_currentItem.SlotOccupied == 0)
            {
                ContentNoItem.Visibility = Visibility.Visible;
                ContentEditItem.Visibility = Visibility.Collapsed;

                listText.Text = NameEmpty;
                _refreshing = false;
                return;
            }


            if (_currentItem.Character < 7)
            {
                var nameString = EquipName.EquipNames[_currentItem.Character][_currentItem.Name];

                listText.Text = nameString;
                imageIcon.Source = _icons[_currentItem.Character*2 + _currentItem.Type];

                ButtonEquipmentName.Content = nameString;
                GroupEquipmentEditor.Header = nameString;
            }
            else
            {
                var nameString = ((Characters) _currentItem.Character).ToString();

                listText.Text = $"{NameUnknown} [{nameString}]";
                imageIcon.Source = null;

                ButtonEquipmentName.Content = NameUnknown;
                GroupEquipmentEditor.Header = NameUnknown;
            }

            var appearance = EquipAppearance.EquipAppearances.FirstOrDefault(e => e.ID == _currentItem.Appearance);
            if (appearance == null)
                ButtonEquipmentAppearance.Content = NameUnknown;
            else
                ButtonEquipmentAppearance.Content = appearance.Name;

            ComboEquipmentCharacter.SelectedIndex = _currentItem.Character;
            ComboEquipmentType.SelectedIndex = _currentItem.Type;

            var damageFormula = DamageFormula.DamageFormulas.FirstOrDefault(f => f.ID == _currentItem.DamageFormula);

            if (damageFormula == null)
                ComboDamageFormula.SelectedIndex = 0;
            else
                ComboDamageFormula.SelectedIndex = Array.IndexOf(DamageFormula.DamageFormulas, damageFormula);

            TextAttackPower.Text = _currentItem.AttackPower.ToString();
            TextCritChance.Text = _currentItem.Critical.ToString();

            ComboEquipmentSlots.SelectedIndex = _currentItem.AbilityCount;

            for (int slot = 0; slot < 4; slot++)
            {
                var button = (Button) FindName("Ability" + slot.ToString().Trim());

                if (slot >= _currentItem.AbilityCount)
                {
                    button.Visibility = Visibility.Collapsed;
                    continue;
                }
                button.Visibility = Visibility.Visible;
                var ability = AutoAbility.FromID(_currentItem.Abilities[slot]);

                if (ability == null)
                {
                    // no ability in this slot
                    button.Content = NameEmpty;
                    continue;
                }

                button.Content = ability.Name;
            }

            ContentNoItem.Visibility = Visibility.Collapsed;
            ContentEditItem.Visibility = Visibility.Visible;

            _refreshing = false;
        }

        private void SelectedEquipment_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            _selectedItem = ListEquipment.SelectedIndex;
            RefreshSelectedItem();
        }

        private void ComboEquipmentSlots_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            var newSlots = (sender as ComboBox).SelectedIndex;
            for (var i = 0; i < 4; i++)
            {
                var abilityButton = (AbilityPanel.Children[i] as Button);
                if (abilityButton == null) continue;

                if (i >= newSlots)
                {
                    abilityButton.Visibility = Visibility.Collapsed;
                    WriteAbility(_selectedItem, i, 0xFF);
                }
                else
                {
                    abilityButton.Visibility = Visibility.Visible;
                }
            }
            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + (_selectedItem*(int) BlockLength.EquipmentItem);
            MemoryReader.WriteBytes(offset + (int) EquipmentOffset.AbilityCount, new byte[] {(byte) newSlots});

            RefreshSelectedItem();
        }

        private void WriteAbility(int itemSlot, int abilitySlot, ushort abilityId)
        {
            _currentItem.Abilities[abilitySlot] = abilityId;
            Data.Equipment.WriteItem(itemSlot, _currentItem);
        }

        private void AbilityButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var index = int.Parse(button.Name.Substring(7));

            // Generate search list
            var searchList = new List<string>();
            foreach (var ability in AutoAbility.AutoAbilities)
                searchList.Add($"{ability.ID.ToString("X4")} {ability.Name}");

            var searchDialog = new SearchDialog(searchList) {Owner = this.TryFindParent<Window>()};
            var searchComplete = searchDialog.ShowDialog();

            if (!searchComplete.HasValue || !searchComplete.Value) return;

            if (searchDialog.ResultIndex == -1)
            {
                // Write empty slot
                WriteAbility(_selectedItem, index, 0xFF);
            }
            else
            {
                var ability = AutoAbility.AutoAbilities[searchDialog.ResultIndex];
                WriteAbility(_selectedItem, index, (ushort) ability.ID);
            }

            RefreshSelectedItem();
        }

        private void ButtonEquipmentName_OnClick(object sender, RoutedEventArgs e)
        {
            var currentChara =
                (int) _allEquipBytes[_selectedItem*(int) BlockLength.EquipmentItem + (int) EquipmentOffset.Character];

            if (currentChara > 6) currentChara = 0;

            var searchList = new List<string>();
            for (int n = 0; n < EquipName.EquipNames[currentChara].Length; n++)
                searchList.Add($"{n.ToString("X2")} {EquipName.EquipNames[currentChara][n]}");

            var currentName =
                (int) _allEquipBytes[_selectedItem*(int) BlockLength.EquipmentItem + (int) EquipmentOffset.Name];
            var nameString = string.Empty;

            if (currentChara < 7)
            {
                nameString = EquipName.EquipNames[currentChara][currentName];
            }

            var searchDialog = new SearchDialog(searchList, nameString, false);
            var searchComplete = searchDialog.ShowDialog();

            if (!searchComplete.Value) return;
            var searchIndex = searchDialog.ResultIndex;

            MemoryReader.WriteByte(
                Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem*(int) BlockLength.EquipmentItem,
                (byte) searchIndex);

            RefreshSelectedItem();
        }

        private void ComboEquipmentCharacter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem*(int) BlockLength.EquipmentItem +
                         (int) EquipmentOffset.Character;
            MemoryReader.WriteBytes(offset, new byte[] {(byte) ComboEquipmentCharacter.SelectedIndex});

            RefreshSelectedItem();
        }

        private void ComboEquipmentType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem*(int) BlockLength.EquipmentItem +
                         (int) EquipmentOffset.Type;
            MemoryReader.WriteBytes(offset, new byte[] {(byte) ComboEquipmentType.SelectedIndex});

            RefreshSelectedItem();
        }

        private void ButtonEquipmentAppearance_OnClick(object sender, RoutedEventArgs e)
        {
            var currentChara =
                (int) _allEquipBytes[_selectedItem*(int) BlockLength.EquipmentItem + (int) EquipmentOffset.Character];

            if (currentChara > 6) currentChara = 0;

            var searchList = new List<string>();
            for (int n = 0; n < EquipAppearance.EquipAppearances.Length; n++)
                searchList.Add(
                    $"{EquipAppearance.EquipAppearances[n].ID.ToString("X2")} {EquipAppearance.EquipAppearances[n].Name}");

            var currentAppearance = BitConverter.ToUInt16(_allEquipBytes,
                _selectedItem*(int) BlockLength.EquipmentItem + (int) EquipmentOffset.Appearance);

            var searchDialog = new SearchDialog(searchList, currentAppearance.ToString("X4"), false);
            var searchComplete = searchDialog.ShowDialog();

            if (!searchComplete.Value) return;
            var searchIndex = searchDialog.ResultIndex;
            var searchItem = EquipAppearance.EquipAppearances[searchIndex];

            MemoryReader.WriteBytes(
                Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem*(int) BlockLength.EquipmentItem +
                (int) EquipmentOffset.Appearance, BitConverter.GetBytes((ushort) searchItem.ID));
            RefreshSelectedItem();
        }

        private void ButtonDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem >= 0x0C && _selectedItem <= 0x21) return;

            var nameString = string.Empty;

            try
            {
                nameString = EquipName.EquipNames[_currentItem.Character][_currentItem.Name];
            }
            catch
            {
                nameString = "This item";
            }

            var confirm =
                MessageBox.Show(
                    $"{nameString} will be permanently deleted!\n\nAre you sure?",
                    "Confirm item deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm != MessageBoxResult.Yes) return;

            var itemEmpty = new EquipmentItem() {Abilities = new ushort[4]};
            Data.Equipment.WriteItem(_selectedItem, itemEmpty);
            RefreshSelectedItem();
        }

        private void ButtonCreateNew_Click(object sender, RoutedEventArgs e)
        {
            var itemEmpty = new EquipmentItem()
            {
                SlotOccupied = 0x01,
                Appearance = 0x4002,
                Name = 0x11,
                Abilities = new ushort[] {0xFF, 0xFF, 0xFF, 0xFF},
                AbilityCount = 0x04,
                DamageFormula = 0x01,
                AttackPower = 15,
                Critical = 3,
                EquippedBy = 0xFF
            };
            Data.Equipment.WriteItem(_selectedItem, itemEmpty);
            RefreshSelectedItem();
        }

        private void ComboDamageFormula_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            var formula = DamageFormula.DamageFormulas[ComboDamageFormula.SelectedIndex];
            _currentItem.DamageFormula = (byte)formula.ID;
            Data.Equipment.WriteItem(_selectedItem, _currentItem);
            RefreshSelectedItem();
        }

        private void TextAttackPower_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_refreshing || e.Key != Key.Enter) return;

            try
            {
                _currentItem.AttackPower = byte.Parse(TextAttackPower.Text);
                Data.Equipment.WriteItem(_selectedItem, _currentItem);
            }
            catch
            {
                Error.Show("Please enter a number between 0 and 255.");
            }
            
        }

        private void TextCritChance_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_refreshing || e.Key != Key.Enter) return;

            try
            {
                _currentItem.Critical = byte.Parse(TextCritChance.Text);
                Data.Equipment.WriteItem(_selectedItem, _currentItem);
            }
            catch
            {
                Error.Show("Please enter a number between 0 and 255.");
            }
        }
    }
}
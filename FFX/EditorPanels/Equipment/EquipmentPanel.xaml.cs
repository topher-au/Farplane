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
using Farplane.FFX.Values;

namespace Farplane.FFX.EditorPanels.Equipment
{
    /// <summary>
    /// Interaction logic for EquipmentPanel.xaml
    /// </summary>
    public partial class EquipmentPanel : UserControl
    {
        private int currentItem = -1;
        private byte[] _equipmentBytes;
        private const int EquipmentLength = 0x16;
        private const int EquipmentSlots = 0xB2;
        private int _selectedItem = -1;
        private bool _refreshing = false;

        public EquipmentPanel()
        {
            InitializeComponent();

            // Initialize equipment list
            for (var slotItemIndex = 0; slotItemIndex < EquipmentSlots; slotItemIndex++)
            {
                

                var equipmentItem = new ListViewItem
                {
                    Name = "Item" + slotItemIndex,
                    Content = "<NONE>"
                };
                ListEquipment.Items.Add(equipmentItem);
            }

            // Initialize equipment item view
            for (var charaIndex=0; charaIndex < 18; charaIndex++)
                ComboEquipmentCharacter.Items.Add((Characters)charaIndex);
        }

        public void Refresh()
        {
            _refreshing = true;

            _equipmentBytes = MemoryReader.ReadBytes(Offsets.GetOffset(OffsetType.EquipmentBase), EquipmentSlots * EquipmentLength);

            for (int equipmentSlot = 0; equipmentSlot < EquipmentSlots; equipmentSlot++)
            {
                var listItem = (ListViewItem)ListEquipment.Items[equipmentSlot];
                // Copy item data to array
                var itemOffset = equipmentSlot*EquipmentLength;

                var equipmentItem = (ListViewItem)ListEquipment.Items[equipmentSlot];

                // TODO: implement equipment icon

                var nameIndex = _equipmentBytes[itemOffset + (int)EquipmentOffset.Name];
                var itemChara = (Characters)_equipmentBytes[itemOffset + (int)EquipmentOffset.Character];

                var itemType = _equipmentBytes[itemOffset + (int) EquipmentOffset.Type];

                

                if (nameIndex == 0xFF)
                    equipmentItem.Content = "<EMPTY>";
                else if (equipmentSlot >= 0x0C && equipmentSlot <= 0x21) // hide aeon gear
                {
                    listItem.Visibility = Visibility.Collapsed;
                    continue;
                }
                else
                {
                    Image itemIcon = null;
                    string itemText = string.Empty;

                    if ((int) itemChara < 7)
                    {
                        itemIcon = new Image
                        {
                            Source =
                                new BitmapImage(
                                    new Uri("pack://application:,,,/FFX/Resources/MenuIcons/equip_" + (int) itemChara +
                                            "_" + itemType + ".png")),
                            Width = 24,
                            Height = 24
                        };
                        itemText = WeaponName.WeaponNames[(int) itemChara][nameIndex];
                    }
                    else
                    {
                        itemIcon = new Image
                        {
                            Width = 24,
                            Height = 24
                        };
                        itemText = "????";
                    }

                    equipmentItem.Content =
                        new DockPanel()
                        {
                            Margin= new Thickness(0),
                            Children =
                            {
                                itemIcon,
                                new TextBlock()
                                {
                                    Text =itemText,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Margin = new Thickness(2)
                                }
                            }
                        };
                }
                    
                        
            }

            if (ListEquipment.SelectedIndex == -1) ListEquipment.SelectedIndex = 0;
            RefreshSelectedItem();

            _refreshing = false;
        }

        private void RefreshSelectedItem()
        {
            _refreshing = true;
            _equipmentBytes = MemoryReader.ReadBytes(Offsets.GetOffset(OffsetType.EquipmentBase), EquipmentSlots * EquipmentLength);

            if (_selectedItem == -1) _selectedItem = 0;
            var itemOffset = _selectedItem * EquipmentLength;

            // read item data
            var itemName = _equipmentBytes[itemOffset + (int)EquipmentOffset.Name];
            var itemChara = _equipmentBytes[itemOffset + (int)EquipmentOffset.Character];
            var itemType = _equipmentBytes[itemOffset + (int)EquipmentOffset.Type];
            var itemAppearance = BitConverter.ToUInt16(_equipmentBytes,itemOffset + (int)EquipmentOffset.Appearance);

            var itemAbilityCount = _equipmentBytes[itemOffset + (int)EquipmentOffset.AbilityCount];
            var abilities = new ushort[4];
            for (int a = 0; a < 4; a++)
                abilities[a] = BitConverter.ToUInt16(_equipmentBytes, itemOffset + (int)EquipmentOffset.Ability0 + (2 * a));

            // update controls
            var itemNameString = "????";
            if (itemChara < 7)
            {
                itemNameString = WeaponName.WeaponNames[(int)itemChara][itemName];
            }

            var appearance = WeaponAppearances.FromID(itemAppearance);
            var appearanceString = string.Empty;
            if (appearance != null)
                appearanceString = appearance.Name;

            GroupEquipmentEditor.Header = itemNameString;
            ButtonEquipmentName.Content = itemNameString;
            ButtonEquipmentAppearance.Content = appearanceString;
            ComboEquipmentCharacter.SelectedIndex = itemChara;
            ComboEquipmentType.SelectedIndex = itemType;
            ComboEquipmentSlots.SelectedIndex = itemAbilityCount;

            // update abilities
            for (int ability = 0; ability < 4; ability++)
            {
                var abilityButton = (AbilityPanel.Children[ability] as Button);
                if (abilityButton == null) continue;

                if (ability >= itemAbilityCount)
                {
                    abilityButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    abilityButton.Visibility = Visibility.Visible;

                    if (abilities[ability] == 0xFF || abilities[ability] == 0x8000)
                    {
                        // Empty slot
                        abilityButton.Content = string.Empty;
                    }
                    else
                    {
                        var abilityName = AutoAbility.AutoAbilities.First(a => a.ID == abilities[ability]).Name;
                        abilityButton.Content = abilityName;
                    }
                }
            }
            _refreshing = false;
        }

        private void SelectedEquipment_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            var selectedItem = (ListViewItem)e.AddedItems[0];
            var itemIndex = int.Parse(selectedItem.Name.Substring(4));
            _selectedItem = itemIndex;
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
            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + (_selectedItem * EquipmentLength);
            MemoryReader.WriteBytes(offset + (int)EquipmentOffset.AbilityCount, new byte[] {(byte)newSlots});

            RefreshSelectedItem();
        }

        private void WriteAbility(int itemSlot, int abilitySlot, int abilityId)
        {
            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + (itemSlot * EquipmentLength);
            MemoryReader.WriteBytes(offset + (int)EquipmentOffset.Ability0 + (abilitySlot*2),
                BitConverter.GetBytes((ushort) abilityId));
        }

        private void AbilityButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var index = int.Parse(button.Name.Substring(7));

            // Generate search list
            var searchList = new List<string>();
            foreach(var ability in AutoAbility.AutoAbilities)
                searchList.Add($"{ability.ID.ToString("X4")} {ability.Name}");

            var searchDialog = new SearchDialog(searchList);
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
                WriteAbility(_selectedItem, index, ability.ID);
            }

            RefreshSelectedItem();
        }

        private void ButtonEquipmentName_OnClick(object sender, RoutedEventArgs e)
        {
            var currentChara = (int)_equipmentBytes[_selectedItem * EquipmentLength + (int)EquipmentOffset.Character];

            if (currentChara > 6) currentChara = 0;

            var searchList = new List<string>();
            for(int n=0; n<WeaponName.WeaponNames[currentChara].Length; n++)
                searchList.Add($"{n.ToString("X2")} {WeaponName.WeaponNames[currentChara][n]}");

            var currentName = (int)_equipmentBytes[_selectedItem * EquipmentLength + (int)EquipmentOffset.Name];
            var nameString = string.Empty;

            if (currentChara < 7)
            {
                nameString = WeaponName.WeaponNames[currentChara][currentName];
            }

            var searchDialog = new SearchDialog(searchList, nameString, false);
            var searchComplete = searchDialog.ShowDialog();

            if (!searchComplete.Value) return;
            var searchIndex = searchDialog.ResultIndex;

            MemoryReader.WriteBytes(Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem * EquipmentLength, new byte[] {(byte)searchIndex});

            Refresh();
        }

        private void ComboEquipmentCharacter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem*EquipmentLength + (int)EquipmentOffset.Character;
            MemoryReader.WriteBytes(offset, new byte[] {(byte) ComboEquipmentCharacter.SelectedIndex});

            Refresh();
            
        }

        private void ComboEquipmentType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            var offset = Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem * EquipmentLength + (int)EquipmentOffset.Type;
            MemoryReader.WriteBytes(offset, new byte[] { (byte)ComboEquipmentType.SelectedIndex });

            Refresh();
            
        }

        private void ButtonEquipmentAppearance_OnClick(object sender, RoutedEventArgs e)
        {
            var currentChara = (int)_equipmentBytes[_selectedItem * EquipmentLength + (int)EquipmentOffset.Character];

            if (currentChara > 6) currentChara = 0;

            var searchList = new List<string>();
            for (int n = 0; n < WeaponAppearances.Appearances.Length; n++)
                searchList.Add($"{WeaponAppearances.Appearances[n].ID.ToString("X2")} {WeaponAppearances.Appearances[n].Name}");

            var currentAppearance = BitConverter.ToUInt16(_equipmentBytes, _selectedItem * EquipmentLength + (int)EquipmentOffset.Appearance);

            var searchDialog = new SearchDialog(searchList, currentAppearance.ToString("X4"), false);
            var searchComplete = searchDialog.ShowDialog();

            if (!searchComplete.Value) return;
            var searchIndex = searchDialog.ResultIndex;
            var searchItem = WeaponAppearances.Appearances[searchIndex];

            MemoryReader.WriteBytes(Offsets.GetOffset(OffsetType.EquipmentBase) + _selectedItem * EquipmentLength + (int)EquipmentOffset.Appearance, BitConverter.GetBytes((ushort)searchItem.ID));
            Refresh();
            
        }
    }
}

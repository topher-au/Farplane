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
using Farplane.FFX.Values;
using MahApps.Metro;

namespace Farplane.FFX.EditorPanels.Items
{
    /// <summary>
    /// Interaction logic for ItemsPanel.xaml
    /// </summary>
    public partial class ItemsPanel : UserControl
    {
        private readonly int _offsetKeyItem = Offsets.GetOffset(OffsetType.KeyItems);
        private readonly int _offsetAlBhed = Offsets.GetOffset(OffsetType.AlBhed);

        private readonly ButtonGrid _itemButtons = new ButtonGrid(2, 112);
        private readonly ButtonGrid _keyItemButtons = new ButtonGrid(2, KeyItem.KeyItems.Length - 1);

        private static readonly ComboBox ComboItemList = new ComboBox() { ItemsSource = Item.Items.Select(item => item.Name) };
        private static readonly TextBox TextItemCount = new TextBox();
        private static readonly StackPanel PanelEditItem = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            Children =
            {
                ComboItemList,
                TextItemCount
            }
        };

        private bool _refreshing = false;

        private Item[] _currentItems;
        private int _editingItem = -1;

        private bool[] _keyItemState;
        private bool[] _alBhedState;

        private static readonly Tuple<AppTheme, Accent> currentStyle = ThemeManager.DetectAppStyle(Application.Current);
        private readonly Brush _trueKeyItemBrush = new SolidColorBrush((Color)currentStyle.Item1.Resources["BlackColor"]);
        private readonly Brush _falseKeyItemBrush = new SolidColorBrush((Color)currentStyle.Item1.Resources["Gray2"]);

        public ItemsPanel()
        {
            InitializeComponent();
            TabItems.Content = _itemButtons;
            ContentKeyItems.Content = _keyItemButtons;

            _itemButtons.ButtonClicked += ItemButtonsOnButtonClicked;

            ComboItemList.KeyDown += ItemEditor_KeyDown;
            TextItemCount.KeyDown += ItemEditor_KeyDown;

            _keyItemButtons.ButtonClicked += KeyItemButtonsOnButtonClicked;
        }

        private void KeyItemButtonsOnButtonClicked(int buttonIndex)
        {
            var keyItemData = Memory.ReadBytes(_offsetKeyItem, 8);
            var bitIndex = KeyItem.KeyItems[buttonIndex].BitIndex;
            var keyByteIndex = bitIndex / 8;
            var keyBitIndex = bitIndex % 8;

            keyItemData[keyByteIndex] = BitHelper.ToggleBit(keyItemData[keyByteIndex], keyBitIndex);
            Memory.WriteBytes(_offsetKeyItem, keyItemData);
            Refresh();
        }

        private void ItemButtonsOnButtonClicked(int buttonIndex)
        {
            if (_editingItem == buttonIndex) return;

            Refresh();

            var clickedItem = _currentItems[buttonIndex];
            var baseItem = Item.Items.First(item => item.ID == clickedItem.ID);

            var itemIndex = Item.Items.ToList().IndexOf(baseItem);

            ComboItemList.SelectedIndex = itemIndex;
            ComboItemList.KeyDown += ItemEditor_KeyDown;

            TextItemCount.Text = clickedItem.Count.ToString();

            _itemButtons.SetContent(buttonIndex, PanelEditItem);
            _editingItem = buttonIndex;

            TextItemCount.SelectionStart = 0;
            TextItemCount.SelectionLength = TextItemCount.Text.Length;

            TextItemCount.Focus();
        }

        private void ItemEditor_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key != Key.Enter && keyEventArgs.Key != Key.Escape) return;

            switch (keyEventArgs.Key)
            {
                case Key.Enter:
                    var itemIndex = ComboItemList.SelectedIndex;
                    var itemCount = byte.Parse(TextItemCount.Text);
                    if (itemCount == 0) itemIndex = 0;
                    if (itemIndex == 0) itemCount = 0;
                    Item.WriteItem(_editingItem, Item.Items[itemIndex].ID, itemCount);
                    Refresh();
                    break;
                case Key.Escape:
                    Refresh();
                    break;
            }
        }

        public void Refresh()
        {
            _refreshing = true;
            _editingItem = -1;
            _currentItems = Item.ReadItems();

            // Refresh inventory items
            for (int i = 0; i < _currentItems.Length; i++)
            {
                if (_currentItems[i].ID == 0xFF)
                {
                    // Empty slot
                    _itemButtons.SetContent(i, "< EMPTY >");
                }
                else
                {
                    // Show item name and count
                    _itemButtons.SetContent(i, _currentItems[i].Name + " x" + _currentItems[i].Count);
                }
            }

            // Refresh key items and al bhed dictionaries
            var keyItemData = Memory.ReadBytes(_offsetKeyItem, 8);
            var alBhedData = Memory.ReadBytes(_offsetAlBhed, 4);
            _keyItemState = BitHelper.GetBitArray(keyItemData, 58);
            _alBhedState = BitHelper.GetBitArray(alBhedData, 26);

            // Key Items
            for (int i = 0; i < KeyItem.KeyItems.Length - 1; i++)
            {
                if (_keyItemState[KeyItem.KeyItems[i].BitIndex])
                {
                    // Key item owned
                    _keyItemButtons.Buttons[i].Foreground = _trueKeyItemBrush;
                    _keyItemButtons.SetContent(i, $"{KeyItem.KeyItems[i].Name}");
                }
                else
                {
                    // Key item not owned
                    _keyItemButtons.Buttons[i].Foreground = _falseKeyItemBrush;
                    _keyItemButtons.SetContent(i, $"{KeyItem.KeyItems[i].Name}");
                }
            }

            // Al Bhed Dictionaries
            for (int i = 0; i < 26; i++)
            {
                (PanelAlBhed.Children[i] as CheckBox).IsChecked = _alBhedState[i];
            }
            _refreshing = false;
        }

        private void AlBhedDictionary_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (_refreshing) return;
            var checkBox = sender as CheckBox;
            var alBhedData = Memory.ReadBytes(_offsetAlBhed, 4);

            var boxIndex = PanelAlBhed.Children.IndexOf(checkBox);

            var byteIndex = boxIndex/8;
            var bitIndex = boxIndex%8;

            alBhedData[byteIndex] = BitHelper.ToggleBit(alBhedData[byteIndex], bitIndex);
            Memory.WriteBytes(_offsetAlBhed, alBhedData);
            Refresh();
        }
    }
}
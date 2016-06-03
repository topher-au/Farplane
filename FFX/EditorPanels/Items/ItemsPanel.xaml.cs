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

namespace Farplane.FFX.EditorPanels.Items
{
    /// <summary>
    /// Interaction logic for ItemsPanel.xaml
    /// </summary>
    public partial class ItemsPanel : UserControl
    {
        private ButtonGrid _itemButtons = new ButtonGrid(2,112);
        private Item[] _currentItems;
        private int _editingItem = -1;

        private static ComboBox _comboItemList = new ComboBox() {ItemsSource = Item.Items.Select(item => item.Name) };
        private static TextBox _textItemCount = new TextBox();
        private static StackPanel _panelEditItem = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
                Children =
                {
                    _comboItemList,
                    _textItemCount
                }
        };

        public ItemsPanel()
        {
            InitializeComponent();
            TabItems.Content = _itemButtons;
            _itemButtons.ButtonClicked += ItemButtonsOnButtonClicked;
            _comboItemList.KeyDown += ItemEditor_KeyDown;
            _textItemCount.KeyDown += ItemEditor_KeyDown;

            
        }

        private void SaveItem(int buttonIndex, int itemID, byte itemCount)
        {
            Item.WriteItem(buttonIndex, itemID, itemCount);
        }
        
        private void ItemButtonsOnButtonClicked(int buttonIndex)
        {
            if (_editingItem == buttonIndex) return;

            Refresh();

            var clickedItem = _currentItems[buttonIndex];
            var baseItem = Item.Items.First(item => item.ID == clickedItem.ID);

            var itemIndex = Item.Items.ToList().IndexOf(baseItem);

            _comboItemList.SelectedIndex = itemIndex;
            _comboItemList.KeyDown += ItemEditor_KeyDown;

            _textItemCount.Text = clickedItem.Count.ToString();

            _itemButtons.SetContent(buttonIndex, _panelEditItem);
            _editingItem = buttonIndex;

            _textItemCount.SelectionStart = 0;
            _textItemCount.SelectionLength = _textItemCount.Text.Length;

            _textItemCount.Focus();
        }

        private void ItemEditor_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key != Key.Enter && keyEventArgs.Key != Key.Escape) return;

            switch (keyEventArgs.Key)
            {
                case Key.Enter:
                    var itemIndex = _comboItemList.SelectedIndex;
                    var itemCount = byte.Parse(_textItemCount.Text);
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
            _editingItem = -1;
            _currentItems = Item.ReadItems();
            
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
        }
    }
}

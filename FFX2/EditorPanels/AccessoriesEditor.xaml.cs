using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Farplane.FFX2.Values;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for ItemsEditor.xaml
    /// </summary>
    public partial class AccessoriesEditor : UserControl
    {
        private byte[] itemTypes;
        private byte[] itemCounts;
        private Button editingButton = null;
        private int editingItem = -1;

        public AccessoriesEditor()
        {
            InitializeComponent();

            ItemsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            ItemsGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for(int r=0;r<64;r++)
                ItemsGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < 128; i++)
            {
                var row = i/2;
                var col = i%2 == 0 ? 0 : 1;
                var itemButton = new Button()
                {
                    Name = "Accessory" + i
                };
                ItemsGrid.Children.Add(itemButton);
                Grid.SetRow(itemButton,row);
                Grid.SetColumn(itemButton,col);
            }
        }

        public void Refresh()
        {
            itemTypes = Memory.ReadBytes((int)OffsetType.AccessoryType, 0x100);
            itemCounts = Memory.ReadBytes((int)OffsetType.AccessoryCount, 0x80);

            for (int i = 0; i < 128; i++)
            {

                var button = ItemsGrid.Children.OfType<Button>().First(butt => butt.Name == "Accessory" + i);

                SetButtonText((Button)button, i);
                button.Click += (sender, args) =>
                {
                    var itemNum = int.Parse(button.Name.Substring(9));
                    if (itemNum == editingItem && editingButton != null) return;
                    SetButtonBox(button, itemNum);
                    (button.Content as StackPanel).UpdateLayout();
                    (button.Content as StackPanel).Children[1].Focus();
                };
            }
        }

        private void ResetButton()
        {
            if (editingButton != null)
            {
                SetButtonText(editingButton, editingItem);
                editingButton = null;
                editingItem = -1;
            }
        }

        public void SetButtonText(Button button, int itemNum)
        {
            var itemType = itemTypes[itemNum*2];
            var itemCount = itemCounts[itemNum];

            var itemNames = Accessories.GetNames();

            if (itemType >= 128 || itemCount == 0)
                button.Content = "EMPTY";
            else
                button.Content = $"{itemNames[itemType]}  x{itemCount}";
        }

        public void SetButtonBox(Button button, int itemNum)
        {
            ResetButton();

            var textItemCount = new TextBox();
            textItemCount.Text = itemCounts[itemNum].ToString();
            textItemCount.SelectionStart = textItemCount.Text.Length;

            var comboItemType = new ComboBox();
            comboItemType.Padding= new Thickness(0D);
            var accessories = Accessories.GetNames();
            accessories.Insert(0, "None");
            comboItemType.ItemsSource = accessories;
            var selItem = itemTypes[itemNum*2];
            comboItemType.SelectedIndex = selItem > 68 ? 0 : selItem+1;
            comboItemType.Width = 140;

            StackPanel buttonPanel = new StackPanel() {Orientation=Orientation.Horizontal};

            buttonPanel.Children.Add(comboItemType);
            buttonPanel.Children.Add(textItemCount);

            buttonPanel.KeyDown += (o, eventArgs) =>
            {
                switch (eventArgs.Key)
                {
                    case Key.Enter:
                        var newCount = byte.Parse(textItemCount.Text);
                        var newItem = comboItemType.SelectedIndex-1;
                        if(newItem != -1 && newCount > 0)
                            Cheats.WriteAccessory(itemNum, newItem, newCount);
                        else
                            Cheats.WriteAccessory(itemNum, -1, 0);
                        ResetButton();
                        Refresh();
                        break;
                    case Key.Escape:
                        ResetButton();
                        break;
                    default:
                        break;
                }
            };

            button.Content = buttonPanel;

            editingButton = button;
            editingItem = itemNum;
        }

        private void GiveAllItems_Click(object sender, RoutedEventArgs e)
        {
            Cheats.GiveAllAccessories();
            Refresh();
        }
    }
}

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

        public ItemsPanel()
        {
            InitializeComponent();
            TabItems.Content = _itemButtons;
        }

        public void Refresh()
        {
            var items = Item.ReadItems();
            
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].ID == 0xFF)
                {
                    // Empty slot
                    _itemButtons.SetContent(i, "< EMPTY >");
                }
                else
                {
                    // Show item name and count
                    _itemButtons.SetContent(i, items[i].Name + " x" + items[i].Count);
                }
                
            }
        }
    }
}

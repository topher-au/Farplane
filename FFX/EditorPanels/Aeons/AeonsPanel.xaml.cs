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
using Farplane.FFX.Data;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.Aeons
{
    /// <summary>
    /// Interaction logic for AeonsPanel.xaml
    /// </summary>
    public partial class AeonsPanel : UserControl
    {
        public delegate void UpdateTabsDelegate();
        public static event UpdateTabsDelegate UpdateTabsEvent;
        private int _currentAeon = 8;
        AeonStats _aeonStats = new AeonStats();
        AeonAbilities _aeonAbilities = new AeonAbilities();

        public AeonsPanel()
        {
            InitializeComponent();
            foreach(var item in TabAeon.Items)
                ControlsHelper.SetHeaderFontSize((TabItem)item, 14);
            UpdateTabsEvent += () => Refresh();
        }

        public static void UpdateTabs()
        {
            UpdateTabsEvent?.Invoke();
        }

        public void Refresh()
        {
            // Refresh names
            for (int i = 0; i < 10; i++)
            {
                var aeonTab = (TabItem)TabAeon.Items[i];
                aeonTab.Header = AeonName.GetName(i+8);
            }
            _aeonStats.Refresh(_currentAeon);
            _aeonAbilities.Refresh(_currentAeon);
        }

        private void TabAeonTab_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemIndex = TabAeonTab.Items.IndexOf(e.AddedItems[0]);
            Refresh();
            switch (itemIndex)
            {
                case 0:
                    ContentAeon.Content = _aeonStats;
                    break;
                case 1:
                    ContentAeon.Content = _aeonAbilities;
                    break;
            }
        }

        private void TabAeon_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentAeon = TabAeon.Items.IndexOf(e.AddedItems[0]) + 8;
            Refresh();
        }
    }
}

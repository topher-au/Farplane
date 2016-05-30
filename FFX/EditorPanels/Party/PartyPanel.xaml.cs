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
using Farplane.FFX.Values;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.Party
{
    /// <summary>
    /// Interaction logic for PartyPanel.xaml
    /// </summary>
    public partial class PartyPanel : UserControl
    {
        private bool _refreshing = false;
        private int _selectedIndex = -1;

        public PartyPanel()
        {
            InitializeComponent();
            foreach(var tab  in TabPartySelect.Items)
                ControlsHelper.SetHeaderFontSize((TabItem)tab, 14);
            Refresh();
        }

        public void Refresh()
        {
            _refreshing = true;

            if (_selectedIndex == -1)
            {
                TabPartySelect.SelectedIndex = 0;
                _selectedIndex = 0;
            }
            
            PartyEditor.Load((Characters)_selectedIndex);
            PartyEditor.Refresh();

            _refreshing = false;
        }

        private void TabPartySelect_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            _selectedIndex = TabPartySelect.SelectedIndex;
            PartyEditor.Load((Characters)_selectedIndex);
            PartyEditor.Refresh();
        }
    }
}

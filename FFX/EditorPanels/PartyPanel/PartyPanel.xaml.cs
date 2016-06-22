using System.Windows.Controls;
using Farplane.FFX.Values;
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.PartyPanel
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
            
            PartyEditor.Load((Character)_selectedIndex);
            PartyEditor.Refresh();

            _refreshing = false;
        }

        private void TabPartySelect_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            _selectedIndex = TabPartySelect.SelectedIndex;
            PartyEditor.Load((Character)_selectedIndex);
            PartyEditor.Refresh();
        }
    }
}

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

namespace Farplane.FFX2.EditorPanels.Party
{
    /// <summary>
    /// Interaction logic for PartyPanel.xaml
    /// </summary>
    public partial class PartyPanel : UserControl
    {
        private int _selected = 0;
        private bool _refresh;
        private StatsPanel _statsPanel = new StatsPanel();
        private DressphereAbilities _dressphereAbilities = new DressphereAbilities();

        public PartyPanel()
        {
            _refresh = true;

            InitializeComponent();

            _refresh = false;
        }

        public void Refresh()
        {
            _refresh = true;

            _statsPanel.Refresh(_selected);

	        _dressphereAbilities.SelectedIndex = _selected;
            _dressphereAbilities.RefreshAbilities();
			_dressphereAbilities.ReloadDresspheres();

            _refresh = false;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;

	        _selected = TabParty.SelectedIndex;
            Refresh();
        }

        private void TabEditor_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabEditor.SelectedIndex == 0) PartyEditor.Content = _statsPanel;
            else PartyEditor.Content = _dressphereAbilities;
        }
    }
}

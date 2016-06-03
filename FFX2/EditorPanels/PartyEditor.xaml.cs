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

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for PartyEditor.xaml
    /// </summary>
    public partial class PartyEditor : UserControl
    {
        private int mStatsOffset = 0;
        private StatsPanel _statsPanel;
        public PartyEditor(int partyIndex, string title = "")
        {
            InitializeComponent();
            mStatsOffset = (int)OffsetType.PartyStatBase + (0x80 * partyIndex);
            if (partyIndex < 3)
            {
                DressAbilities.baseOffset = (int) OffsetType.AbilityBase + 0x6A0*partyIndex;
                TabDresspheres.Visibility = Visibility.Visible;
            }
            else
            {
                TabDresspheres.Visibility = Visibility.Collapsed;
            }
            
            _statsPanel = new StatsPanel(mStatsOffset);

            StatsContent.Content = _statsPanel;
            GroupPartyEditor.Header = title;
        }

        public void Refresh()
        {
            _statsPanel.Refresh();
        }
    }
}

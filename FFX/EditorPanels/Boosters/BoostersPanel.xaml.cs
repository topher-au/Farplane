using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Farplane.FFX.Values;
using CheckBox = System.Windows.Controls.CheckBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Farplane.FFX.EditorPanels.Boosters
{
    /// <summary>
    /// Interaction logic for BoostersPanel.xaml
    /// </summary>
    public partial class BoostersPanel : UserControl
    {
        public BoostersPanel()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            // No refresh logic for this panel
        }

        private void GiveAllItems_Click(object sender, RoutedEventArgs e)
        {
            Cheats.GiveAllItems();
        }

        private void MaxAllStats_Click(object sender, RoutedEventArgs e)
        {
            Cheats.MaxAllStats();
        }

        private void MaxSphereLevels_Click(object sender, RoutedEventArgs e)
        {
            Cheats.MaxSphereLevels();
        }

        private void LearnAllAbilities_Click(object sender, RoutedEventArgs e)
        {
            Cheats.LearnAllAbilities();
        }
    }
}
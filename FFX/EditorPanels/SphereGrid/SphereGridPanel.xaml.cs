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
using MahApps.Metro.Controls;

namespace Farplane.FFX.EditorPanels.SphereGrid
{
    /// <summary>
    /// Interaction logic for SphereGridPanel.xaml
    /// </summary>
    public partial class SphereGridPanel : UserControl
    {
        public SphereGridPanel()
        {
            InitializeComponent();
            foreach(var tabItem in TabSphereGrid.Items)
                ControlsHelper.SetHeaderFontSize((TabItem)tabItem, 14);
        }

        public void Refresh()
        {
            
        }
    }
}

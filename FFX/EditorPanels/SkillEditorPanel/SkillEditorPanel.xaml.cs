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

namespace Farplane.FFX.EditorPanels.SkillEditorPanel
{
    /// <summary>
    /// Interaction logic for SkillEditorPanel.xaml
    /// </summary>
    public partial class SkillEditorPanel : UserControl
    {
        public SkillEditorPanel()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            CommandEditor.Refresh();
        }
    }
}

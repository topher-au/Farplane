using System.Windows.Controls;

namespace Farplane.FFX.EditorPanels.SphereGridPanel
{
    /// <summary>
    /// Interaction logic for SphereGridPanel.xaml
    /// </summary>
    public partial class SphereGridPanel : UserControl
    {
        private SphereGridEditor _sphereGridEditor = new SphereGridEditor();
        public SphereGridPanel()
        {
            InitializeComponent();
            SphereGridEditor.Content = _sphereGridEditor;
        }

        public void Refresh()
        {
            _sphereGridEditor.Refresh();
        }
    }
}
